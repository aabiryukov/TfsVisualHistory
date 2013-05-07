using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Sitronics.TfsVisualHistory.VSExtension
{
    public partial class SettingForm : Form
    {
        const string DialogCaption = "Visualization Settings";

        private VisualizationSettings m_settigs;

        public VisualizationSettings Settigs
        {
            get { return m_settigs; }
        }

        public static VisualizationSettings DefaultSettigs
        {
            get 
            {
                return new VisualizationSettings();
            }
        }

        public SettingForm()
        {
            InitializeComponent();

            // Loading recent configuration
            try
            {
                if (File.Exists(RecentConfigurationFile))
                {
                    m_settigs = VisualizationSettings.LoadFromFile(RecentConfigurationFile);
                }
                else m_settigs = DefaultSettigs;

                SetSettigs(m_settigs);
            }
            catch (Exception ex)
            {
                ShowError(ex);
                SetSettigs(DefaultSettigs);
            }
        }

        public void SetSourcePath(string sourcePath)
        {
            pathTextBox.Text = sourcePath;
        }

        private void resetToDefaultButton_Click(object sender, EventArgs e)
        {
            SetSettigs(DefaultSettigs);
        }

        private VisualizationSettings GetSettigs()
        {
            var settigs = new VisualizationSettings();

            settigs.DateFrom = dateFromPicker.Value;
            settigs.DateTo = dateToPicker.Value;

            settigs.IncludeUsers = userIncludeTextBox.Text;
            settigs.ExcludeUsers = userExcludeTextBox.Text;

            settigs.IncludeFiles = filesIncludeTextBox.Text;
            settigs.ExcludeFiles = filesExcludeTextBox.Text;

            settigs.HideFileNames = hideFileNamesCheckBox.Checked;

            settigs.FullScreen = fullScreenCheckBox.Checked;

            if (!int.TryParse(secondsPerDayTextBox.Text, out settigs.SecondsPerDay) || settigs.SecondsPerDay < 0 || settigs.SecondsPerDay > 1000)
            {
                MessageBox.Show("Incorrect value in 'Seconds Per Day'.", DialogCaption);
                ActiveControl = secondsPerDayTextBox;
                return null;
            }

            if (!int.TryParse(maxFilesTextBox.Text, out settigs.MaxFiles) || settigs.MaxFiles < 1 || settigs.MaxFiles > 1000000)
            {
                MessageBox.Show("Incorrect value in 'Max Files' (Range: 1-1000000).", DialogCaption);
                ActiveControl = maxFilesTextBox;
                return null;
            }

            return settigs;
        }

        public void SetSettigs(VisualizationSettings settigs)
        {
            dateFromPicker.Value = settigs.DateFrom;
            dateToPicker.Value = settigs.DateTo;

            userIncludeTextBox.Text = settigs.IncludeUsers;
            userExcludeTextBox.Text = settigs.ExcludeUsers;

            filesIncludeTextBox.Text = settigs.IncludeFiles;
            filesExcludeTextBox.Text = settigs.ExcludeFiles;

            hideFileNamesCheckBox.Checked = settigs.HideFileNames;
            secondsPerDayTextBox.Text = settigs.SecondsPerDay.ToString();

            fullScreenCheckBox.Checked = settigs.FullScreen;
            maxFilesTextBox.Text = settigs.MaxFiles.ToString();
        }

        private static string RecentConfigurationFile
        {
            get { return Path.Combine(Path.GetTempPath(), "TfsVisualHistoryRecentSettings.VHCfg"); }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            m_settigs = GetSettigs();
            if (m_settigs != null)
            {
                try
                {
                    m_settigs.SaveToFile(RecentConfigurationFile);
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }

                DialogResult = DialogResult.OK;
            }
        }

        private void interactiveKeyboardCommandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string Help =
"\"Gource\" window interactive keyboard commands:\n" +
"\n" +
"    (V)   Toggle camera mode\n" +
//"    (C)   Displays Gource logo\n" +
"    (K)   Toggle file extension key.\n" +
"    (M)   Toggle mouse visibility\n" +
"    (N)   Jump forward in time to next log entry.\n" +
"    (S)   Randomize colours.\n" +
"    (+-)  Adjust simulation speed.\n" +
"    (<>)  Adjust time scale.\n" +
"    (TAB) Cycle through visible users\n" +
"\n" +
"    (Alt+Enter) Toggle Fullscreen\n" +
"\n" +
"    (ESC) Quit\n" +
"\n" +
"\n" +
"Gource is external tool embedded to this extension.\n" +
"Gource - software version control visualization\n" +
"Copyright (C) 2009 Andrew Caudwell <acaudwell@gmail.com>\n" +
"Project site: http://code.google.com/p/gource/\n";

            MessageBox.Show(this, Help, DialogCaption);
        }

        private void saveSettingsToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settigs = GetSettigs();
            if (settigs == null) return;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                settigs.SaveToFile(saveFileDialog1.FileName);
            }
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    var settings = VisualizationSettings.LoadFromFile(openFileDialog1.FileName);
                    SetSettigs(settings);
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
            }
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(this, ex.Message, DialogCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
