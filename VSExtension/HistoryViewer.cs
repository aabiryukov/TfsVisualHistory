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
       //             + " --fullscreen"
//                    + " --viewport " + System.Windows.Forms.SystemInformation.VirtualScreen.Width + "x" + System.Windows.Forms.SystemInformation.VirtualScreen.Height
                    + " --highlight-users"
                    + " --user-image-dir Avatars"
                    + " --logo \"{1}\""
                    + " --title \"{2}\"",
                    logFilePath, Path.Combine(dataPath, "Logo.png"), title
                );

            if (m_settigs.FullScreen)
            {
                arguments += " --fullscreen";
            }

            if (m_settigs.SecondsPerDay == 0)
            {
                arguments += " --realtime";
            }
            else
            {
                arguments += " --seconds-per-day " + m_settigs.SecondsPerDay.ToString(CultureInfo.InvariantCulture);
            }

            if (m_settigs.HideFileNames)
            {
                arguments += " --hide filenames";
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
