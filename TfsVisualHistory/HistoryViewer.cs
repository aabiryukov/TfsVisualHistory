using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;
using Sitronics.TfsVisualHistory.Utility;

namespace Sitronics.TfsVisualHistory
{
    public class HistoryViewer
    {
        private readonly VisualizationSettings m_settigs;
        private bool m_canceled;

        private HistoryViewer(VisualizationSettings settigs)
        {
            m_settigs = settigs;
        }

        public static void ViewHistory(Uri tfsCollectionUri, string sourceControlFolder)
        {
            using (var settingForm = new SettingForm())
            {
                settingForm.SetSourcePath(sourceControlFolder);

                if (settingForm.ShowDialog() != DialogResult.OK) return;

                var historyViewer = new HistoryViewer(settingForm.Settigs);
                historyViewer.ExecViewHistory(tfsCollectionUri, sourceControlFolder);
            }
        }

        private void OnCancelByUser(object sender, EventArgs e)
        {
            m_canceled = true;
        }

        private static string ConvertToString(VisualizationSettings.TimeScaleOption timeScale)
        {
            switch (timeScale)
            {
                case VisualizationSettings.TimeScaleOption.Slow8: return "0.125";
                case VisualizationSettings.TimeScaleOption.Slow4: return "0.25";
                case VisualizationSettings.TimeScaleOption.Slow2: return "0.5";
                case VisualizationSettings.TimeScaleOption.Fast2: return "2";
                case VisualizationSettings.TimeScaleOption.Fast3: return "3";
                case VisualizationSettings.TimeScaleOption.Fast4: return "4";
            }

            return null;
        }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private void ExecViewHistory(Uri tfsCollectionUri, string sourceControlFolder)
        {
            // gource start arguments
            string arguments;
            string title;
			string avatarsDirectory = null;

            if (m_settigs.PlayMode == VisualizationSettings.PlayModeOption.History)
            {
                title = "History of " + sourceControlFolder;
				var logFile = Path.Combine(FileUtils.GetTempPath(), "TfsHistoryLog.tmp.txt");
	            
	            if (m_settigs.ViewAvatars)
	            {
		            avatarsDirectory = Path.Combine(FileUtils.GetTempPath(), "TfsHistoryLog.tmp.Avatars");
					if (!Directory.Exists(avatarsDirectory))
		            {
			            Directory.CreateDirectory(avatarsDirectory);
		            }
	            }

				bool historyFound;
				bool hasLines;

                using (var waitMessage = new WaitMessage("Connecting to Team Foundation Server...", OnCancelByUser))
                {
                    var progress = waitMessage.CreateProgress("Loading history ({0}% done) ...");

                    hasLines =
                        TfsLogWriter.CreateGourceLogFile(
                            logFile,
							avatarsDirectory,
                            tfsCollectionUri,
                            sourceControlFolder,
                            m_settigs,
                            ref m_canceled,
                            progress.SetValue
                            );

	                historyFound = progress.LastValue > 0;
                    progress.Done();
                }

                if (m_canceled)
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

	            arguments = string.Format(CultureInfo.InvariantCulture, " \"{0}\" ", logFile);

                // Setting other history settings

                arguments += " --seconds-per-day " + m_settigs.SecondsPerDay.ToString(CultureInfo.InvariantCulture);

                if (m_settigs.TimeScale != VisualizationSettings.TimeScaleOption.None)
                {
                    var optionValue = ConvertToString(m_settigs.TimeScale);
                    if (optionValue != null)
                        arguments += " --time-scale " + optionValue;
                }

                if (m_settigs.LoopPlayback)
                {
                    arguments += " --loop";
                }

				arguments += " --file-idle-time 60"; // 60 is default in gource 0.40 and older. Since 0.41 default 0.
            }
            else
            {
                // PlayMode: Live
                title = "Live changes of " + sourceControlFolder;

                arguments = " --realtime --log-format custom -";
                arguments += " --file-idle-time 28800"; // 8 hours (work day)
            }

            var baseDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ??
                                "unknown";


            if (baseDirectory.Contains("Test"))
            {
                baseDirectory += @"\..\..\..\VSExtension";
            }

#if DEBUG
			// baseDirectory = @"C:\Temp\aaaa\уи³пс\";
#endif
            var gourcePath = Path.Combine(baseDirectory, @"Gource\Gource.exe");
            var dataPath = Path.Combine(baseDirectory, @"Data");

            // ******************************************************
            // Configuring Gource command line
            // ******************************************************

            arguments +=
                string.Format(CultureInfo.InvariantCulture, " --highlight-users --title \"{0}\"", title);

			if (m_settigs.ViewLogo != CheckState.Unchecked)
			{
				var logoFile = m_settigs.ViewLogo == CheckState.Indeterminate
					? Path.Combine(dataPath, "Logo.png")
					: m_settigs.LogoFileName;

				// fix gource unicode path problems
				logoFile = FileUtils.GetShortPath(logoFile);

				arguments += string.Format(CultureInfo.InvariantCulture, " --logo \"{0}\"", logoFile);
			}

            if (m_settigs.FullScreen)
            {
                arguments += " --fullscreen";

				// By default gource not using full area of screen width ( It's a bug. Must be fixed in gource 0.41).
				// Fixing fullscreen resolution to real full screen.
				if (!m_settigs.SetResolution)
				{
					var screenBounds = Screen.PrimaryScreen.Bounds;
					arguments += string.Format(CultureInfo.InvariantCulture, " --viewport {0}x{1}", screenBounds.Width,
											   screenBounds.Height);
				}
			}

            if (m_settigs.SetResolution)
            {
                arguments += string.Format(CultureInfo.InvariantCulture, " --viewport {0}x{1}",
                                           m_settigs.ResolutionWidth, m_settigs.ResolutionHeight);
            }

            if (m_settigs.ViewFilesExtentionMap)
            {
                arguments += " --key";
            }

			if (!string.IsNullOrEmpty(avatarsDirectory))
			{
				arguments += string.Format(CultureInfo.InvariantCulture, " --user-image-dir \"{0}\"", avatarsDirectory);
			}

            // Process "--hide" option
            {
                var hideItems = string.Empty;
                if (!m_settigs.ViewDirNames)
                {
                    hideItems = "dirnames";
                }
                if (!m_settigs.ViewFileNames)
                {
                    if (hideItems.Length > 0) hideItems += ",";
                    hideItems += "filenames";
                }
                if (!m_settigs.ViewUserNames)
                {
                    if (hideItems.Length > 0) hideItems += ",";
                    hideItems += "usernames";
                }

                if (hideItems.Length > 0)
                    arguments += " --hide " + hideItems;
            }

            arguments += " --max-files " + m_settigs.MaxFiles.ToString(CultureInfo.InvariantCulture);

			if (SystemInformation.TerminalServerSession)
			{
				arguments += " --disable-bloom";
			}

			if (m_settigs.PlayMode == VisualizationSettings.PlayModeOption.History)
            {
                var si = new ProcessStartInfo(gourcePath, arguments)
                    {
                        WindowStyle = ProcessWindowStyle.Maximized,
 //                       UseShellExecute = true
					};

                Process.Start(si);
            }
            else
            {
                var logReader = new VersionControlLogReader(tfsCollectionUri, sourceControlFolder, m_settigs.UsersFilter,
                                                     m_settigs.FilesFilter);
                using (new WaitMessage("Connecting to Team Foundation Server..."))
                {
                    logReader.Connect();
                }
                var si = new ProcessStartInfo(gourcePath, arguments)
                    {
                        WindowStyle = ProcessWindowStyle.Maximized,
                        RedirectStandardInput = true,
                        UseShellExecute = false
                    };

                System.Threading.Tasks.Task.Factory.StartNew(() => RunLiveChangesMonitor(logReader, si));
            }
        }

        private static void RunLiveChangesMonitor(VersionControlLogReader reader, ProcessStartInfo gourceStartInfo)
        {
            var process = Process.Start(gourceStartInfo);

// ReSharper disable once PossibleNullReferenceException
            while (!process.HasExited)
            {
                var line = reader.ReadLine();
                Debug.WriteLine("LOG: " + (line ?? "<NULL>"));
                if (line == null)
                {
                    // Waiting for second to avoid frequent server calls 
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    process.StandardInput.WriteLine(line);
                    // System.Threading.Thread.Sleep(100);
                }
            }
        }
    }
}
