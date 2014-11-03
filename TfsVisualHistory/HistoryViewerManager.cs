using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sitronics.TfsVisualHistory.Utility;

namespace Sitronics.TfsVisualHistory
{
	public class HistoryViewerManager
	{
		private readonly VisualizationSettings _visualizationSettings;
		private readonly PathProvider _pathProvider;
		private readonly ArgumentGenerator _argumentGenerator;
		private bool _canceled;

		/// <summary>
		/// Initializes a new instance of the <see cref="HistoryViewerManager"/> class.
		/// </summary>
		internal HistoryViewerManager(VisualizationSettings visualizationSettings, PathProvider pathProvider, ArgumentGenerator argumentGenerator)
		{
			if (visualizationSettings == null) throw new ArgumentNullException("visualizationSettings");
			if (pathProvider == null) throw new ArgumentNullException("pathProvider");
			if (argumentGenerator == null) throw new ArgumentNullException("argumentGenerator");

			_visualizationSettings = visualizationSettings;
			_pathProvider = pathProvider;
			_argumentGenerator = argumentGenerator;
		}

		internal void ExecViewHistory(Uri tfsCollectionUri, string sourceControlFolder)
		{
			if (_visualizationSettings.PlayMode == PlayMode.History)
			{
				bool historyFound;
				bool hasLines;

				using (var waitMessage = new WaitMessage("Connecting to Team Foundation Server...", delegate { _canceled = true; }))
				{
					var progress = waitMessage.CreateProgress("Loading history ({0}% done) ...");

					hasLines = TfsLogWriter.CreateGourceLogFile(
						_pathProvider.LogFilePath,
						_pathProvider.AvatarsDirectoryPath,
						tfsCollectionUri,
						sourceControlFolder,
						_visualizationSettings,
						ref _canceled,
						progress.SetValue);

					historyFound = progress.LastValue > 0;
					progress.Done();
				}

				if (_canceled)
					return;

				if (!hasLines)
				{
					MessageBox.Show(
						historyFound
							? "No items found.\nCheck your filters: 'User name' and 'File type'."
							: "No items found.\nTry to change period of the history (From/To dates).",
						"TFS History Visualization");
					return;
				}
			}

			var arguments = _argumentGenerator.Generate(sourceControlFolder);

			var si = new ProcessStartInfo(_pathProvider.GourceExecutableFilePath, arguments)
			{
				WindowStyle = ProcessWindowStyle.Maximized,
				UseShellExecute = _visualizationSettings.PlayMode == PlayMode.History,
				RedirectStandardInput = _visualizationSettings.PlayMode != PlayMode.History
			};

			switch (_visualizationSettings.PlayMode)
			{
				case PlayMode.History:
					{
						Process.Start(si);
					}
					break;
				case PlayMode.Live:
				case PlayMode.HistoryThenLive:
					{
						VersionControlLogReader logReader;

						using (new WaitMessage("Connecting to Team Foundation Server..."))
						{
							logReader = new VersionControlLogReader(
								tfsCollectionUri,
								sourceControlFolder,
								_visualizationSettings.UsersFilter,
								_visualizationSettings.FilesFilter,
								_visualizationSettings.PlayMode == PlayMode.HistoryThenLive);
						}

						Task.Factory.StartNew(() => RunLiveChangesMonitor(logReader, si));
					}
					break;
			}
		}

		private static void RunLiveChangesMonitor(VersionControlLogReader reader, ProcessStartInfo startInfo)
		{
			var process = Process.Start(startInfo);

			// ReSharper disable once PossibleNullReferenceException
			while (!process.HasExited)
			{
				var line = reader.ReadLine();
				Debug.WriteLine("LOG: " + (line ?? "<NULL>"));
				if (line == null)
				{
					// Waiting for second to avoid frequent server calls 
					Thread.Sleep(1000);
				}
				else
				{
					process.StandardInput.WriteLine(line);
				}
			}
		}
	}
}
