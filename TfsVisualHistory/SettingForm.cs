using System;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using Sitronics.TfsVisualHistory.Utility;

namespace Sitronics.TfsVisualHistory
{
    public partial class SettingForm : Form
    {
        const string DialogCaption = "Visualization Settings";

        internal VisualizationSettings Settigs { get; private set; }

        internal static VisualizationSettings DefaultSettigs
        {
            get 
            {
                return new VisualizationSettings();
            }
        }

        public SettingForm()
        {
            InitializeComponent();

			toolTip1.SetToolTip(selectAvatarsDirButton, AvatarsDirectoryHelp);
			toolTip1.SetToolTip(avatarsDirLabel, AvatarsDirectoryHelp);

            // Loading recent configuration
            try
            {
                Settigs = File.Exists(RecentConfigurationFile) ? VisualizationSettings.LoadFromFile(RecentConfigurationFile) : DefaultSettigs;
                SetSettigs(Settigs);
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

        private static bool TryParseInt(string text, out int value)
        {
            return int.TryParse(text, NumberStyles.Integer, CultureInfo.CurrentCulture, out value);
        }

        private VisualizationSettings GetSettigs()
        {
            var settigs = new VisualizationSettings
                {
                    PlayMode = historyRadioButton.Checked
                                           ? VisualizationSettings.PlayModeOption.History
                                           : VisualizationSettings.PlayModeOption.Live,
                    DateFrom = dateFromPicker.Value,
                    DateTo = dateToPicker.Value,
                    LoopPlayback = loopPlaybackCheckBox.Checked,
                    UsersFilter = new StringFilter(userIncludeTextBox.Text, userExcludeTextBox.Text),
                    FilesFilter = new StringFilter(filesIncludeTextBox.Text, filesExcludeTextBox.Text), 
                    ViewFileNames = viewFileNamesCheckBox.Checked,
                    ViewDirNames = viewDirNamesCheckBox.Checked,
                    ViewUserNames = viewUserNamesCheckBox.Checked,
                    ViewFilesExtentionMap = viewFilesExtentionMapCheckBox.Checked,
					AvatarsDirectory = avatarsDirTextBox.Text
                };


            // History settings
            if (historyRadioButton.Checked)
            {
                if (timeScaleComboBox.SelectedIndex >= 0)
                {
                    var timeScaleMapping = new[]
                        {
                            VisualizationSettings.TimeScaleOption.None,
                            VisualizationSettings.TimeScaleOption.Slow8,
                            VisualizationSettings.TimeScaleOption.Slow4,
                            VisualizationSettings.TimeScaleOption.Slow2,
                            VisualizationSettings.TimeScaleOption.Fast2,
                            VisualizationSettings.TimeScaleOption.Fast3,
                            VisualizationSettings.TimeScaleOption.Fast4
                        };

                    settigs.TimeScale = timeScaleMapping[timeScaleComboBox.SelectedIndex];
                }

                if (!TryParseInt(secondsPerDayTextBox.Text, out settigs.SecondsPerDay) || settigs.SecondsPerDay < 1 || settigs.SecondsPerDay > 1000)
                {
                    MessageBox.Show("Incorrect value in 'Seconds Per Day' (1-1000).", DialogCaption);
                    ActiveControl = secondsPerDayTextBox;
                    return null;
                }
            }


            if (!TryParseInt(maxFilesTextBox.Text, out settigs.MaxFiles) || settigs.MaxFiles < 1 || settigs.MaxFiles > 1000000)
            {
                MessageBox.Show("Incorrect value in 'Max Files' (Range: 1-1000000).", DialogCaption);
                ActiveControl = maxFilesTextBox;
                return null;
            }


            settigs.FullScreen = fullScreenCheckBox.Checked;
            settigs.SetResolution = setResolutionCheckBox.Checked;

            if (setResolutionCheckBox.Checked)
            {
                if (!TryParseInt(resolutionWidthTextBox.Text, out settigs.ResolutionWidth) || settigs.ResolutionWidth < 100)
                {
                    MessageBox.Show("Incorrect value in 'Resolution width' (min: 100).", DialogCaption);
                    ActiveControl = resolutionWidthTextBox;
                    return null;
                }

                if (!TryParseInt(resolutionHeightTextBox.Text, out settigs.ResolutionHeight) || settigs.ResolutionHeight < 100)
                {
                    MessageBox.Show("Incorrect value in 'Resolution height' (min: 100).", DialogCaption);
                    ActiveControl = resolutionHeightTextBox;
                    return null;
                }
            }

            return settigs;
        }

        internal void SetSettigs(VisualizationSettings settigs)
        {
            historyRadioButton.Checked = settigs.PlayMode == VisualizationSettings.PlayModeOption.History;
            liveStreamRadioButton.Checked = settigs.PlayMode == VisualizationSettings.PlayModeOption.Live;

            dateFromPicker.Value = settigs.DateFrom;
            dateToPicker.Value = settigs.DateTo;

            userIncludeTextBox.Text = settigs.UsersFilter.IncludeMask;
            userExcludeTextBox.Text = settigs.UsersFilter.ExcludeMask;

            filesIncludeTextBox.Text = settigs.FilesFilter.IncludeMask;
            filesExcludeTextBox.Text = settigs.FilesFilter.ExcludeMask;

            viewFilesExtentionMapCheckBox.Checked = settigs.ViewFilesExtentionMap;

            viewFileNamesCheckBox.Checked = settigs.ViewFileNames;
            viewDirNamesCheckBox.Checked = settigs.ViewDirNames;
            viewUserNamesCheckBox.Checked = settigs.ViewUserNames;
            
            timeScaleComboBox.SelectedIndex = (int)settigs.TimeScale;
            secondsPerDayTextBox.Text = settigs.SecondsPerDay.ToString(CultureInfo.CurrentCulture);
            loopPlaybackCheckBox.Checked = settigs.LoopPlayback;

            fullScreenCheckBox.Checked = settigs.FullScreen;
            setResolutionCheckBox.Checked = settigs.SetResolution;
            resolutionWidthTextBox.Text = settigs.ResolutionWidth.ToString(CultureInfo.CurrentCulture);
            resolutionHeightTextBox.Text = settigs.ResolutionHeight.ToString(CultureInfo.CurrentCulture);

            maxFilesTextBox.Text = settigs.MaxFiles.ToString(CultureInfo.CurrentCulture);

	        avatarsDirTextBox.Text = settigs.AvatarsDirectory;
        }

        private static string RecentConfigurationFile
        {
            get { return Path.Combine(Path.GetTempPath(), "TfsVisualHistoryRecentSettings.VHCfg"); }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
			if(!ValidateSettingsUI())
				return;

            Settigs = GetSettigs();
            if (Settigs != null)
            {
                try
                {
                    Settigs.SaveToFile(RecentConfigurationFile);
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }

                DialogResult = DialogResult.OK;
            }
        }

	    private void ShowValidationError(Control focusControl, string message)
	    {
			MessageBox.Show(this, message, "Settings validation error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			ActiveControl = focusControl;
		}
	    private void ShowValidationError(Control focusControl, string messageFmt, params object[] args)
	    {
		    ShowValidationError(focusControl, string.Format(CultureInfo.CurrentCulture, messageFmt, args));
	    }

// ReSharper disable once InconsistentNaming
	    private bool ValidateSettingsUI()
	    {
		    if (!string.IsNullOrEmpty(avatarsDirTextBox.Text) && !Directory.Exists(avatarsDirTextBox.Text))
		    {
				ShowValidationError(avatarsDirTextBox, "Avatars directory '{0}' does not exist.", avatarsDirTextBox.Text);
			    return false;
		    }

		    return true;
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

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                settigs.SaveToFile(saveFileDialog1.FileName);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
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

        private void setResolutionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            resolutionHeightTextBox.Enabled = setResolutionCheckBox.Checked;
            resolutionWidthTextBox.Enabled = setResolutionCheckBox.Checked;
        }

        private void historyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            historySettingsGroupBox.Enabled = historyRadioButton.Checked;
        }

	    private const string AvatarsDirectoryHelp =
		    "Directory containing .jpg or .png images of users (eg 'Full Name.png') to use as avatars.";

		private void selectAvatarsDirButton_Click(object sender, EventArgs e)
		{
			using (var folderBrowser = new FolderBrowserDialog())
			{
				folderBrowser.SelectedPath = avatarsDirTextBox.Text;
				folderBrowser.Description = "Select directory with avatars.\n" + AvatarsDirectoryHelp;

				if (folderBrowser.ShowDialog() == DialogResult.OK)
				{
					avatarsDirTextBox.Text = folderBrowser.SelectedPath;
				}
			}
		}
    }
}
