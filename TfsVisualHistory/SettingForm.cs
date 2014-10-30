using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Sitronics.TfsVisualHistory.Utility;

namespace Sitronics.TfsVisualHistory
{
	public partial class SettingForm : Form
	{
		const string DialogCaption = "Visualization Settings";

		internal VisualizationSettings Settings { get; private set; }

		internal static VisualizationSettings DefaultSettigs
		{
			get
			{
				return new VisualizationSettings();
			}
		}

		private static string RecentConfigurationFile
		{
			get { return Path.Combine(Path.GetTempPath(), "TfsVisualHistoryRecentSettings.VHCfg"); }
		}

		public SettingForm()
		{
			InitializeComponent();

			// Loading recent configuration
			try
			{
				Settings = File.Exists(RecentConfigurationFile) ? VisualizationSettings.LoadFromFile(RecentConfigurationFile) : DefaultSettigs;
				SetSettigs(Settings);
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

		private VisualizationSettings GetSettings()
		{
			if (string.IsNullOrWhiteSpace(userIncludeTextBox.Text))
			{
				MessageBox.Show("User name filter 'include' can not be empty.", DialogCaption);
				ActiveControl = userIncludeTextBox;
				return null;
			}

			if (string.IsNullOrWhiteSpace(filesIncludeTextBox.Text))
			{
				MessageBox.Show("File type filter 'include' can not be empty.", DialogCaption);
				ActiveControl = filesIncludeTextBox;
				return null;
			}

			var settings = new VisualizationSettings
				{
					PlayMode = GetPlayMode(),
					DateFrom = dateFromPicker.Value,
					DateTo = dateToPicker.Value,
					LoopPlayback = loopPlaybackCheckBox.Checked,
					UsersFilter = new StringFilter(userIncludeTextBox.Text, userExcludeTextBox.Text),
					FilesFilter = new StringFilter(filesIncludeTextBox.Text, filesExcludeTextBox.Text),
					ViewFileNames = viewFileNamesCheckBox.Checked,
					ViewDirNames = viewDirNamesCheckBox.Checked,
					ViewUserNames = viewUserNamesCheckBox.Checked,
					ViewAvatars = viewAvatarsCheckBox.Checked,
					ViewFilesExtentionMap = viewFilesExtentionMapCheckBox.Checked,
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

					settings.TimeScale = timeScaleMapping[timeScaleComboBox.SelectedIndex];
				}

				if (!TryParseInt(secondsPerDayTextBox.Text, out settings.SecondsPerDay) || settings.SecondsPerDay < 1 || settings.SecondsPerDay > 1000)
				{
					MessageBox.Show("Incorrect value in 'Seconds Per Day' (1-1000).", DialogCaption);
					ActiveControl = secondsPerDayTextBox;
					return null;
				}
			}


			if (!TryParseInt(maxFilesTextBox.Text, out settings.MaxFiles) || settings.MaxFiles < 0 || settings.MaxFiles > 1000000)
			{
				MessageBox.Show("Incorrect value in 'Max Files' (Range: 0-1000000).", DialogCaption);
				ActiveControl = maxFilesTextBox;
				return null;
			}


			settings.FullScreen = fullScreenCheckBox.Checked;
			settings.SetResolution = setResolutionCheckBox.Checked;

			if (setResolutionCheckBox.Checked)
			{
				if (!TryParseInt(resolutionWidthTextBox.Text, out settings.ResolutionWidth) || settings.ResolutionWidth < 100)
				{
					MessageBox.Show("Incorrect value in 'Resolution width' (min: 100).", DialogCaption);
					ActiveControl = resolutionWidthTextBox;
					return null;
				}

				if (!TryParseInt(resolutionHeightTextBox.Text, out settings.ResolutionHeight) || settings.ResolutionHeight < 100)
				{
					MessageBox.Show("Incorrect value in 'Resolution height' (min: 100).", DialogCaption);
					ActiveControl = resolutionHeightTextBox;
					return null;
				}
			}

			if (ViewLogoCheckBox.CheckState == CheckState.Checked && !File.Exists(LogoFileTextBox.Text))
			{
				MessageBox.Show("Logo file path does not exist.", DialogCaption);
				ActiveControl = LogoFileTextBox;
				return null;
			}

			settings.ViewLogo = ViewLogoCheckBox.CheckState;
			settings.LogoFileName = settings.ViewLogo == CheckState.Checked ? LogoFileTextBox.Text : null;
			settings.FileIdleTime = int.Parse(secondsTextBox.Text);

			return settings;
		}

		private VisualizationSettings.PlayModeOption GetPlayMode()
		{
			if (historyRadioButton.Checked)
				return VisualizationSettings.PlayModeOption.History;

			if (liveStreamRadioButton.Checked)
				return VisualizationSettings.PlayModeOption.Live;

			if (liveWithHistoryRadioButton.Checked)
				return VisualizationSettings.PlayModeOption.HistoryThenLive;

			throw new Exception();
		}

		internal void SetSettigs(VisualizationSettings settings)
		{
			historyRadioButton.Checked = settings.PlayMode == VisualizationSettings.PlayModeOption.History;
			liveStreamRadioButton.Checked = settings.PlayMode == VisualizationSettings.PlayModeOption.Live;
			liveWithHistoryRadioButton.Checked = settings.PlayMode == VisualizationSettings.PlayModeOption.HistoryThenLive;

			dateFromPicker.Value = settings.DateFrom;
			dateToPicker.Value = settings.DateTo;

			userIncludeTextBox.Text = settings.UsersFilter.IncludeMask;
			userExcludeTextBox.Text = settings.UsersFilter.ExcludeMask;

			filesIncludeTextBox.Text = settings.FilesFilter.IncludeMask;
			filesExcludeTextBox.Text = settings.FilesFilter.ExcludeMask;

			viewFilesExtentionMapCheckBox.Checked = settings.ViewFilesExtentionMap;

			viewFileNamesCheckBox.Checked = settings.ViewFileNames;
			viewDirNamesCheckBox.Checked = settings.ViewDirNames;
			viewUserNamesCheckBox.Checked = settings.ViewUserNames;
			viewAvatarsCheckBox.Checked = settings.ViewAvatars;

			timeScaleComboBox.SelectedIndex = (int)settings.TimeScale;
			secondsPerDayTextBox.Text = settings.SecondsPerDay.ToString(CultureInfo.CurrentCulture);
			loopPlaybackCheckBox.Checked = settings.LoopPlayback;

			fullScreenCheckBox.Checked = settings.FullScreen;
			setResolutionCheckBox.Checked = settings.SetResolution;
			resolutionWidthTextBox.Text = settings.ResolutionWidth.ToString(CultureInfo.CurrentCulture);
			resolutionHeightTextBox.Text = settings.ResolutionHeight.ToString(CultureInfo.CurrentCulture);

			maxFilesTextBox.Text = settings.MaxFiles.ToString(CultureInfo.CurrentCulture);

			ViewLogoCheckBox.CheckState = settings.ViewLogo;
			LogoFileTextBox.Text = settings.LogoFileName;

			secondsTextBox.Text = settings.FileIdleTime.ToString(CultureInfo.InvariantCulture);
		}

		private void ShowError(Exception ex)
		{
			MessageBox.Show(this, ex.Message, DialogCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			//			if(!ValidateSettingsUI()) return;

			Settings = GetSettings();
			if (Settings != null)
			{
				try
				{
					Settings.SaveToFile(RecentConfigurationFile);
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
			var settigs = GetSettings();
			if (settigs == null) return;

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				settigs.SaveToFile(saveFileDialog1.FileName);
			}
		}

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
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

		private void setResolutionCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			resolutionHeightTextBox.Enabled = setResolutionCheckBox.Checked;
			resolutionWidthTextBox.Enabled = setResolutionCheckBox.Checked;
		}

		private void historyRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			historySettingsGroupBox.Enabled = (historyRadioButton.Checked || liveWithHistoryRadioButton.Checked);

			if (!retainIndefinitelyCheckBox.Checked)
			{
				if (historyRadioButton.Checked)
				{
					secondsTextBox.Text = "60";
				}
				else
				{
					secondsTextBox.Text = "28800";
				}
			}
		}

		private void ViewLogoCheckBox_CheckStateChanged(object sender, EventArgs e)
		{
			LogoFileTextBox.Enabled = ViewLogoCheckBox.CheckState == CheckState.Checked;
			selectLogoFileButton.Enabled = LogoFileTextBox.Enabled;
		}

		private void selectLogoFileButton_Click(object sender, EventArgs e)
		{
			using (var fileDialog = new OpenFileDialog())
			{
				if (string.IsNullOrEmpty(LogoFileTextBox.Text))
				{
					fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
				}
				else
				{
					fileDialog.FileName = LogoFileTextBox.Text;
					fileDialog.InitialDirectory = Path.GetDirectoryName(fileDialog.FileName);
				}

				fileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp";
				fileDialog.Title = "Select Logo image file";

				if (fileDialog.ShowDialog(this) == DialogResult.OK)
				{
					LogoFileTextBox.Text = fileDialog.FileName;
				}
			}
		}

		private void retainIndefinitelyCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			secondsTextBox.Enabled = !retainIndefinitelyCheckBox.Checked;

			if (retainIndefinitelyCheckBox.Checked)
			{
				secondsTextBox.Text = "0";
			}
			else
			{
				if (historyRadioButton.Checked)
				{
					secondsTextBox.Text = "60";
				}
				else
				{
					secondsTextBox.Text = "28800";
				}
			}
		}

		private void unlimitedCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (unlimitedCheckBox.Checked)
			{
				maxFilesTextBox.Text = "0";
			}
			else
			{
				maxFilesTextBox.Text = "10000";
			}

			maxFilesTextBox.Enabled = !unlimitedCheckBox.Checked;
		}
	}
}
