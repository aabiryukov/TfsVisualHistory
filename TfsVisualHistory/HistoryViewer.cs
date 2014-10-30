using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sitronics.TfsVisualHistory.Utility;

namespace Sitronics.TfsVisualHistory
{
	public class HistoryViewer
	{
		public static void ViewHistory(Uri tfsCollectionUri, string sourceControlFolder)
		{
			using (var settingForm = new SettingForm())
			{
				settingForm.SetSourcePath(sourceControlFolder);

				if (settingForm.ShowDialog() != DialogResult.OK) 
					return;

				var pathProvider = new PathProvider();
				var visualizationSettings = settingForm.Settings;
				var argumentGenerator = new ArgumentGenerator(visualizationSettings, pathProvider);

				var historyViewerManager = new HistoryViewerManager(
					visualizationSettings, 
					pathProvider,
					argumentGenerator);

				historyViewerManager.ExecViewHistory(tfsCollectionUri, sourceControlFolder);
			}
		}

	}
}
