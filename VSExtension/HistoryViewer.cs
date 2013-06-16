using System;
//using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;

namespace Sitronics.TfsVisualHistory.VSExtension
{
    public class HistoryViewer
    {
//        private Uri m_tfsCollectionUri;
//        string m_sourceControlFolder;
        private readonly VisualizationSettings m_settigs;
        private bool m_canceled;

        private HistoryViewer(VisualizationSettings settigs)
        {
            m_settigs = settigs;
        }

        public static void ViewHistory(Uri tfsCollectionUri, string sourceControlFolder)
        {
            var settingForm = new SettingForm();
            settingForm.SetSourcePath(sourceControlFolder);

            if (settingForm.ShowDialog() != DialogResult.OK) return;

            var historyViewer = new HistoryViewer(settingForm.Settigs);
            historyViewer.ExecViewHistory(tfsCollectionUri, sourceControlFolder);
        }

        private void OnCancelByUser(object sender, EventArgs e)
        {
            m_canceled = true;
        }

        private void ExecViewHistory(Uri tfsCollectionUri, string sourceControlFolder)
        {
            var logFile = Path.Combine(Path.GetTempPath(), "TfsHistoryLog.tmp.txt");

            bool hasLines;
            using (var waitMessage = new Installer.UI.WaitMessage("Connecting to Team Foundation Server...", OnCancelByUser))
            {
                hasLines = 
                    TfsLogWriter.CreateGourceLogFile(
                        logFile,
                        tfsCollectionUri,
                        sourceControlFolder,
                        m_settigs,
                        ref m_canceled,
                        x => 
                        {
                            waitMessage.Text = "Loading history (" + x.ToString() + "% done) ...";
                        });
            }

            if (!hasLines)
            {
                if (!m_canceled)
                {
                    MessageBox.Show("History items not found.", "TFS History Visualization");
                }
                return;
            }

            var baseDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? "unknown";
            if (baseDirectory.Contains("Test"))
            {
                baseDirectory += @"\..\..\..\VSExtension";
            }

            var gourcePath = Path.Combine(baseDirectory, @"Gource\Gource.exe");
            var dataPath = Path.Combine(baseDirectory, @"Data");
            var title = "History of " + sourceControlFolder;

            StartGource(gourcePath, dataPath, logFile, title);
        }


        private void StartGource(
            string gourcePath,
            string dataPath,
            string logFilePath,
            string title)
        {
            var arguments =
                string.Format(CultureInfo.InvariantCulture,
                    " \"{0}\" "
                    + " --highlight-users"
//                    + " --auto-skip-seconds 1"
//                    + " --user-image-dir Avatars"
                    + " --logo \"{1}\""
                    + " --title \"{2}\"",
                    logFilePath, Path.Combine(dataPath, "Logo.png"), title
                );

            if (m_settigs.FullScreen)
            {
                arguments += " --fullscreen";

                // By default gource not using full area of screen width ( It's a bug. Must be fixed in gource 0.41).
                // Fixing fullscreen resolution to real full screen.
                if (!m_settigs.SetResolution)
                {
                    var screenBounds = Screen.PrimaryScreen.Bounds;
                    arguments += string.Format(CultureInfo.InvariantCulture, " --viewport {0}x{1}", screenBounds.Width, screenBounds.Height);
                }
            }

            if (m_settigs.SetResolution)
            {
                arguments += string.Format(CultureInfo.InvariantCulture, " --viewport {0}x{1}", m_settigs.ResolutionWidth, m_settigs.ResolutionHeight);
            }

            if (m_settigs.SecondsPerDay == 0)
            {
                arguments += " --realtime";
            }
            else
            {
                arguments += " --seconds-per-day " + m_settigs.SecondsPerDay.ToString(CultureInfo.InvariantCulture);
            }

            if (m_settigs.TimeScale != VisualizationSettings.TimeScaleOption.None)
            {
                string optionValue = null;
                switch (m_settigs.TimeScale)
                {
                    case VisualizationSettings.TimeScaleOption.Slow8:
                        optionValue = "0.125";
                        break;
                    case VisualizationSettings.TimeScaleOption.Slow4:
                        optionValue = "0.25";
                        break;
                    case VisualizationSettings.TimeScaleOption.Slow2:
                        optionValue = "0.5";
                        break;
                    case VisualizationSettings.TimeScaleOption.Fast2:
                        optionValue = "2";
                        break;
                    case VisualizationSettings.TimeScaleOption.Fast3:
                        optionValue = "3";
                        break;
                    case VisualizationSettings.TimeScaleOption.Fast4:
                        optionValue = "4";
                        break;
                }

                if (optionValue != null)
                    arguments += " --time-scale " + optionValue;
            }

            // Process "--hide" option
            {
                var hideItems = string.Empty;
                if (m_settigs.HideDirNames)
                {
                    hideItems = "dirnames";
                }
                if (m_settigs.HideFileNames)
                {
                    if (hideItems.Length > 0) hideItems += ",";
                    hideItems += "filenames";
                }
                if (m_settigs.HideUserNames)
                {
                    if (hideItems.Length > 0) hideItems += ",";
                    hideItems += "usernames";
                }

                if (hideItems.Length > 0)
                    arguments += " --hide " + hideItems;
            }

            if (m_settigs.LoopPlayback)
            {
                arguments += " --loop";
            }

            arguments += " --max-files " + m_settigs.MaxFiles.ToString(CultureInfo.InvariantCulture);

            var si = new ProcessStartInfo(gourcePath, arguments)
                {
                    WindowStyle = ProcessWindowStyle.Maximized,
                    UseShellExecute = true
                };
            Process.Start(si);
        }
    }
}
