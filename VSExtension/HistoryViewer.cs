using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;

namespace Sitronics.TfsVisualHistory.VSExtension
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

        private void ExecViewHistory(Uri tfsCollectionUri, string sourceControlFolder)
        {
            // gource start arguments
            string arguments;

            if (m_settigs.PlayMode == VisualizationSettings.PlayModeOption.History)
            {
                var logFile = Path.Combine(Path.GetTempPath(), "TfsHistoryLog.tmp.txt");

                bool hasLines;

                using (var waitMessage = new Utility.WaitMessage("Connecting to Team Foundation Server...", OnCancelByUser))
                {
                    var progress = waitMessage.CreateProgress<int>("Loading history ({0}% done) ...");

                    hasLines =
                        TfsLogWriter.CreateGourceLogFile(
                            logFile,
                            tfsCollectionUri,
                            sourceControlFolder,
                            m_settigs,
                            ref m_canceled,
                            progress.SetValue
                            );

                    progress.Done();
                }

                if (!hasLines)
                {
                    if (!m_canceled)
                    {
                        MessageBox.Show("History items not found.", "TFS History Visualization");
                    }
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
            }
            else
            {
                // PlayMode: Live

                arguments = " --realtime --log-format custom -";

                arguments += " --file-idle-time 28800"; // 8 hours
            }

            var baseDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? "unknown";
            if (baseDirectory.Contains("Test"))
            {
                baseDirectory += @"\..\..\..\VSExtension";
            }

            var gourcePath = Path.Combine(baseDirectory, @"Gource\Gource.exe");
            var dataPath = Path.Combine(baseDirectory, @"Data");
            var title = "History of " + sourceControlFolder;

            // ******************************************************
            // Configuring Gource command line
            // ******************************************************

            arguments +=
                string.Format(CultureInfo.InvariantCulture,
                    " --highlight-users"
//                    + " --auto-skip-seconds 1"
//                    + " --user-image-dir Avatars"
//                    + " --background-colour FFFFFF"
                    + " --logo \"{0}\""
                    + " --title \"{1}\"",
                    Path.Combine(dataPath, "Logo.png"), title
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

            if (m_settigs.ViewFilesExtentionMap)
            {
                arguments += " --key";
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

            var si = new ProcessStartInfo(gourcePath, arguments)
                {
                    WindowStyle = ProcessWindowStyle.Maximized,
                    UseShellExecute = true
                };

            if (m_settigs.PlayMode == VisualizationSettings.PlayModeOption.History)
            {
                Process.Start(si);
            }
            else
            {
                using (var sr = new VersionControlLogReader(tfsCollectionUri, sourceControlFolder, m_settigs))
                // using (StreamWriter sw = new StreamWriter())
                {
                    using (new Utility.WaitMessage("Connecting to Team Foundation Server..."))
                    {
                        sr.Connect();
                    }

                    si.RedirectStandardInput = true;
                    si.UseShellExecute = false;
                    var process = Process.Start(si);

                    while (!process.HasExited)
                    {
                        var line = sr.ReadLine();
                        if (line == null)
                        {
                            // Waiting for second to avoid frequent server calls 
                            System.Threading.Thread.Sleep(1000);
                        }
                        else
                        {
                            process.StandardInput.WriteLine(line);
                        }
                    }
                }
            }
        }
    }
}
