namespace Sitronics.TfsVisualHistory
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.pathTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.historySettingsGroupBox = new System.Windows.Forms.GroupBox();
			this.dateToLabel = new System.Windows.Forms.Label();
			this.dateFromLabel = new System.Windows.Forms.Label();
			this.timeScaleComboBox = new System.Windows.Forms.ComboBox();
			this.loopPlaybackCheckBox = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.dateToPicker = new System.Windows.Forms.DateTimePicker();
			this.dateFromPicker = new System.Windows.Forms.DateTimePicker();
			this.secondsPerDayTextBox = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.userExcludeTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.userIncludeTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.resetToDefaultButton = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.filesExcludeTextBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.filesIncludeTextBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.selectLogoFileButton = new System.Windows.Forms.Button();
			this.LogoFileTextBox = new System.Windows.Forms.TextBox();
			this.ViewLogoCheckBox = new System.Windows.Forms.CheckBox();
			this.viewAvatarsCheckBox = new System.Windows.Forms.CheckBox();
			this.viewFilesExtentionMapCheckBox = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.viewUserNamesCheckBox = new System.Windows.Forms.CheckBox();
			this.viewDirNamesCheckBox = new System.Windows.Forms.CheckBox();
			this.maxFilesTextBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.viewFileNamesCheckBox = new System.Windows.Forms.CheckBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.saveSettingsToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.restoreDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.interactiveKeyboardCommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.liveStreamRadioButton = new System.Windows.Forms.RadioButton();
			this.historyRadioButton = new System.Windows.Forms.RadioButton();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageGeneral = new System.Windows.Forms.TabPage();
			this.tabPageVisualization = new System.Windows.Forms.TabPage();
			this.tabPageFilters = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.setResolutionCheckBox = new System.Windows.Forms.CheckBox();
			this.fullScreenCheckBox = new System.Windows.Forms.CheckBox();
			this.resolutionHeightTextBox = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.resolutionWidthTextBox = new System.Windows.Forms.TextBox();
			this.historySettingsGroupBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageGeneral.SuspendLayout();
			this.tabPageVisualization.SuspendLayout();
			this.tabPageFilters.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.okButton.Location = new System.Drawing.Point(173, 460);
			this.okButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(180, 40);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "Start";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(361, 460);
			this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(100, 40);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// pathTextBox
			// 
			this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pathTextBox.Location = new System.Drawing.Point(57, 41);
			this.pathTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pathTextBox.Name = "pathTextBox";
			this.pathTextBox.ReadOnly = true;
			this.pathTextBox.Size = new System.Drawing.Size(407, 22);
			this.pathTextBox.TabIndex = 4;
			this.pathTextBox.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 41);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "Path";
			// 
			// historySettingsGroupBox
			// 
			this.historySettingsGroupBox.Controls.Add(this.dateToLabel);
			this.historySettingsGroupBox.Controls.Add(this.dateFromLabel);
			this.historySettingsGroupBox.Controls.Add(this.timeScaleComboBox);
			this.historySettingsGroupBox.Controls.Add(this.loopPlaybackCheckBox);
			this.historySettingsGroupBox.Controls.Add(this.label11);
			this.historySettingsGroupBox.Controls.Add(this.dateToPicker);
			this.historySettingsGroupBox.Controls.Add(this.dateFromPicker);
			this.historySettingsGroupBox.Controls.Add(this.secondsPerDayTextBox);
			this.historySettingsGroupBox.Controls.Add(this.label8);
			this.historySettingsGroupBox.Location = new System.Drawing.Point(14, 131);
			this.historySettingsGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.historySettingsGroupBox.Name = "historySettingsGroupBox";
			this.historySettingsGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.historySettingsGroupBox.Size = new System.Drawing.Size(418, 185);
			this.historySettingsGroupBox.TabIndex = 6;
			this.historySettingsGroupBox.TabStop = false;
			this.historySettingsGroupBox.Text = "History setting";
			// 
			// dateToLabel
			// 
			this.dateToLabel.AutoSize = true;
			this.dateToLabel.Location = new System.Drawing.Point(216, 39);
			this.dateToLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.dateToLabel.Name = "dateToLabel";
			this.dateToLabel.Size = new System.Drawing.Size(29, 17);
			this.dateToLabel.TabIndex = 2;
			this.dateToLabel.Text = "To:";
			// 
			// dateFromLabel
			// 
			this.dateFromLabel.AutoSize = true;
			this.dateFromLabel.Location = new System.Drawing.Point(12, 39);
			this.dateFromLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.dateFromLabel.Name = "dateFromLabel";
			this.dateFromLabel.Size = new System.Drawing.Size(44, 17);
			this.dateFromLabel.TabIndex = 0;
			this.dateFromLabel.Text = "From:";
			// 
			// timeScaleComboBox
			// 
			this.timeScaleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.timeScaleComboBox.FormattingEnabled = true;
			this.timeScaleComboBox.Items.AddRange(new object[] {
            "<None>",
            "Slow x1/8",
            "Slow x1/4",
            "Slow x1/2",
            "Fast x2",
            "Fast x3",
            "Fast x4"});
			this.timeScaleComboBox.Location = new System.Drawing.Point(193, 120);
			this.timeScaleComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.timeScaleComboBox.Name = "timeScaleComboBox";
			this.timeScaleComboBox.Size = new System.Drawing.Size(108, 24);
			this.timeScaleComboBox.TabIndex = 7;
			// 
			// loopPlaybackCheckBox
			// 
			this.loopPlaybackCheckBox.AutoSize = true;
			this.loopPlaybackCheckBox.Location = new System.Drawing.Point(15, 156);
			this.loopPlaybackCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.loopPlaybackCheckBox.Name = "loopPlaybackCheckBox";
			this.loopPlaybackCheckBox.Size = new System.Drawing.Size(122, 21);
			this.loopPlaybackCheckBox.TabIndex = 10;
			this.loopPlaybackCheckBox.Text = "Loop playback";
			this.loopPlaybackCheckBox.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(10, 121);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(80, 17);
			this.label11.TabIndex = 6;
			this.label11.Text = "Time scale:";
			// 
			// dateToPicker
			// 
			this.dateToPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateToPicker.Location = new System.Drawing.Point(255, 36);
			this.dateToPicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dateToPicker.Name = "dateToPicker";
			this.dateToPicker.Size = new System.Drawing.Size(139, 22);
			this.dateToPicker.TabIndex = 3;
			// 
			// dateFromPicker
			// 
			this.dateFromPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateFromPicker.Location = new System.Drawing.Point(64, 35);
			this.dateFromPicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dateFromPicker.Name = "dateFromPicker";
			this.dateFromPicker.Size = new System.Drawing.Size(139, 22);
			this.dateFromPicker.TabIndex = 1;
			// 
			// secondsPerDayTextBox
			// 
			this.secondsPerDayTextBox.Location = new System.Drawing.Point(193, 90);
			this.secondsPerDayTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.secondsPerDayTextBox.MaxLength = 5;
			this.secondsPerDayTextBox.Name = "secondsPerDayTextBox";
			this.secondsPerDayTextBox.Size = new System.Drawing.Size(67, 22);
			this.secondsPerDayTextBox.TabIndex = 5;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(9, 93);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(178, 17);
			this.label8.TabIndex = 4;
			this.label8.Text = "Seconds per day (1-1000):";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.userExcludeTextBox);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.userIncludeTextBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(14, 22);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox1.Size = new System.Drawing.Size(418, 132);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "User name filter";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(81, 94);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(193, 17);
			this.label4.TabIndex = 4;
			this.label4.Text = "Example: John*; *Eva; *robot*";
			// 
			// userExcludeTextBox
			// 
			this.userExcludeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.userExcludeTextBox.Location = new System.Drawing.Point(84, 66);
			this.userExcludeTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.userExcludeTextBox.Name = "userExcludeTextBox";
			this.userExcludeTextBox.Size = new System.Drawing.Size(325, 22);
			this.userExcludeTextBox.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 70);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 17);
			this.label3.TabIndex = 2;
			this.label3.Text = "Exclude:";
			// 
			// userIncludeTextBox
			// 
			this.userIncludeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.userIncludeTextBox.Location = new System.Drawing.Point(84, 34);
			this.userIncludeTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.userIncludeTextBox.Name = "userIncludeTextBox";
			this.userIncludeTextBox.Size = new System.Drawing.Size(325, 22);
			this.userIncludeTextBox.TabIndex = 1;
			this.userIncludeTextBox.Text = "*";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 38);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "Include:";
			// 
			// resetToDefaultButton
			// 
			this.resetToDefaultButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.resetToDefaultButton.Location = new System.Drawing.Point(12, 460);
			this.resetToDefaultButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.resetToDefaultButton.Name = "resetToDefaultButton";
			this.resetToDefaultButton.Size = new System.Drawing.Size(128, 40);
			this.resetToDefaultButton.TabIndex = 14;
			this.resetToDefaultButton.Text = "Restore &Defaults";
			this.resetToDefaultButton.UseVisualStyleBackColor = true;
			this.resetToDefaultButton.Click += new System.EventHandler(this.resetToDefaultButton_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.filesExcludeTextBox);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.filesIncludeTextBox);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Location = new System.Drawing.Point(14, 184);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox2.Size = new System.Drawing.Size(418, 132);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "File type filter";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(81, 96);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(119, 17);
			this.label5.TabIndex = 4;
			this.label5.Text = "Example: *.cs;*.txt";
			// 
			// filesExcludeTextBox
			// 
			this.filesExcludeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.filesExcludeTextBox.Location = new System.Drawing.Point(84, 68);
			this.filesExcludeTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.filesExcludeTextBox.Name = "filesExcludeTextBox";
			this.filesExcludeTextBox.Size = new System.Drawing.Size(325, 22);
			this.filesExcludeTextBox.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(11, 72);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(61, 17);
			this.label6.TabIndex = 2;
			this.label6.Text = "Exclude:";
			// 
			// filesIncludeTextBox
			// 
			this.filesIncludeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.filesIncludeTextBox.Location = new System.Drawing.Point(84, 36);
			this.filesIncludeTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.filesIncludeTextBox.Name = "filesIncludeTextBox";
			this.filesIncludeTextBox.Size = new System.Drawing.Size(325, 22);
			this.filesIncludeTextBox.TabIndex = 1;
			this.filesIncludeTextBox.Text = "*";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(11, 40);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(57, 17);
			this.label7.TabIndex = 0;
			this.label7.Text = "Include:";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.selectLogoFileButton);
			this.groupBox3.Controls.Add(this.LogoFileTextBox);
			this.groupBox3.Controls.Add(this.ViewLogoCheckBox);
			this.groupBox3.Controls.Add(this.viewAvatarsCheckBox);
			this.groupBox3.Controls.Add(this.viewFilesExtentionMapCheckBox);
			this.groupBox3.Controls.Add(this.label12);
			this.groupBox3.Controls.Add(this.viewUserNamesCheckBox);
			this.groupBox3.Controls.Add(this.viewDirNamesCheckBox);
			this.groupBox3.Controls.Add(this.maxFilesTextBox);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.viewFileNamesCheckBox);
			this.groupBox3.Location = new System.Drawing.Point(14, 22);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox3.Size = new System.Drawing.Size(418, 161);
			this.groupBox3.TabIndex = 7;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Visualization";
			// 
			// selectLogoFileButton
			// 
			this.selectLogoFileButton.Location = new System.Drawing.Point(370, 122);
			this.selectLogoFileButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.selectLogoFileButton.Name = "selectLogoFileButton";
			this.selectLogoFileButton.Size = new System.Drawing.Size(40, 27);
			this.selectLogoFileButton.TabIndex = 10;
			this.selectLogoFileButton.Text = "...";
			this.selectLogoFileButton.UseVisualStyleBackColor = true;
			this.selectLogoFileButton.Click += new System.EventHandler(this.selectLogoFileButton_Click);
			// 
			// LogoFileTextBox
			// 
			this.LogoFileTextBox.Location = new System.Drawing.Point(105, 126);
			this.LogoFileTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.LogoFileTextBox.Name = "LogoFileTextBox";
			this.LogoFileTextBox.Size = new System.Drawing.Size(257, 22);
			this.LogoFileTextBox.TabIndex = 9;
			// 
			// ViewLogoCheckBox
			// 
			this.ViewLogoCheckBox.AutoSize = true;
			this.ViewLogoCheckBox.Location = new System.Drawing.Point(12, 128);
			this.ViewLogoCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ViewLogoCheckBox.Name = "ViewLogoCheckBox";
			this.ViewLogoCheckBox.Size = new System.Drawing.Size(88, 21);
			this.ViewLogoCheckBox.TabIndex = 8;
			this.ViewLogoCheckBox.Text = "Logo file:";
			this.ViewLogoCheckBox.ThreeState = true;
			this.ViewLogoCheckBox.UseVisualStyleBackColor = true;
			this.ViewLogoCheckBox.CheckStateChanged += new System.EventHandler(this.ViewLogoCheckBox_CheckStateChanged);
			// 
			// viewAvatarsCheckBox
			// 
			this.viewAvatarsCheckBox.AutoSize = true;
			this.viewAvatarsCheckBox.Location = new System.Drawing.Point(320, 31);
			this.viewAvatarsCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.viewAvatarsCheckBox.Name = "viewAvatarsCheckBox";
			this.viewAvatarsCheckBox.Size = new System.Drawing.Size(78, 21);
			this.viewAvatarsCheckBox.TabIndex = 4;
			this.viewAvatarsCheckBox.Text = "Avatars";
			this.viewAvatarsCheckBox.UseVisualStyleBackColor = true;
			// 
			// viewFilesExtentionMapCheckBox
			// 
			this.viewFilesExtentionMapCheckBox.AutoSize = true;
			this.viewFilesExtentionMapCheckBox.Location = new System.Drawing.Point(13, 96);
			this.viewFilesExtentionMapCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.viewFilesExtentionMapCheckBox.Name = "viewFilesExtentionMapCheckBox";
			this.viewFilesExtentionMapCheckBox.Size = new System.Drawing.Size(183, 21);
			this.viewFilesExtentionMapCheckBox.TabIndex = 7;
			this.viewFilesExtentionMapCheckBox.Text = "View files extension map";
			this.viewFilesExtentionMapCheckBox.UseVisualStyleBackColor = true;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(9, 32);
			this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(41, 17);
			this.label12.TabIndex = 0;
			this.label12.Text = "View:";
			// 
			// viewUserNamesCheckBox
			// 
			this.viewUserNamesCheckBox.AutoSize = true;
			this.viewUserNamesCheckBox.Location = new System.Drawing.Point(241, 31);
			this.viewUserNamesCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.viewUserNamesCheckBox.Name = "viewUserNamesCheckBox";
			this.viewUserNamesCheckBox.Size = new System.Drawing.Size(67, 21);
			this.viewUserNamesCheckBox.TabIndex = 3;
			this.viewUserNamesCheckBox.Text = "Users";
			this.viewUserNamesCheckBox.UseVisualStyleBackColor = true;
			// 
			// viewDirNamesCheckBox
			// 
			this.viewDirNamesCheckBox.AutoSize = true;
			this.viewDirNamesCheckBox.Location = new System.Drawing.Point(132, 31);
			this.viewDirNamesCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.viewDirNamesCheckBox.Name = "viewDirNamesCheckBox";
			this.viewDirNamesCheckBox.Size = new System.Drawing.Size(98, 21);
			this.viewDirNamesCheckBox.TabIndex = 2;
			this.viewDirNamesCheckBox.Text = "Directories";
			this.viewDirNamesCheckBox.UseVisualStyleBackColor = true;
			// 
			// maxFilesTextBox
			// 
			this.maxFilesTextBox.Location = new System.Drawing.Point(199, 64);
			this.maxFilesTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.maxFilesTextBox.MaxLength = 7;
			this.maxFilesTextBox.Name = "maxFilesTextBox";
			this.maxFilesTextBox.Size = new System.Drawing.Size(67, 22);
			this.maxFilesTextBox.TabIndex = 6;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(9, 68);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(175, 17);
			this.label9.TabIndex = 5;
			this.label9.Text = "Max number of active files:";
			// 
			// viewFileNamesCheckBox
			// 
			this.viewFileNamesCheckBox.AutoSize = true;
			this.viewFileNamesCheckBox.Location = new System.Drawing.Point(61, 31);
			this.viewFileNamesCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.viewFileNamesCheckBox.Name = "viewFileNamesCheckBox";
			this.viewFileNamesCheckBox.Size = new System.Drawing.Size(59, 21);
			this.viewFileNamesCheckBox.TabIndex = 1;
			this.viewFileNamesCheckBox.Text = "Files";
			this.viewFileNamesCheckBox.UseVisualStyleBackColor = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(477, 28);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSettingsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.saveSettingsToFileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.restoreDefaultToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// loadSettingsToolStripMenuItem
			// 
			this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
			this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
			this.loadSettingsToolStripMenuItem.Text = "&Load Settings";
			this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(233, 6);
			// 
			// saveSettingsToFileToolStripMenuItem
			// 
			this.saveSettingsToFileToolStripMenuItem.Name = "saveSettingsToFileToolStripMenuItem";
			this.saveSettingsToFileToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
			this.saveSettingsToFileToolStripMenuItem.Text = "&Save Settings As...";
			this.saveSettingsToFileToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToFileToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(233, 6);
			// 
			// restoreDefaultToolStripMenuItem
			// 
			this.restoreDefaultToolStripMenuItem.Name = "restoreDefaultToolStripMenuItem";
			this.restoreDefaultToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
			this.restoreDefaultToolStripMenuItem.Text = "Restore Default settings";
			this.restoreDefaultToolStripMenuItem.Click += new System.EventHandler(this.resetToDefaultButton_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.interactiveKeyboardCommandsToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// interactiveKeyboardCommandsToolStripMenuItem
			// 
			this.interactiveKeyboardCommandsToolStripMenuItem.Name = "interactiveKeyboardCommandsToolStripMenuItem";
			this.interactiveKeyboardCommandsToolStripMenuItem.Size = new System.Drawing.Size(290, 24);
			this.interactiveKeyboardCommandsToolStripMenuItem.Text = "Interactive keyboard commands";
			this.interactiveKeyboardCommandsToolStripMenuItem.Click += new System.EventHandler(this.interactiveKeyboardCommandsToolStripMenuItem_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "VHCfg";
			this.saveFileDialog1.FileName = "VisualHistorySettings1.VHCfg";
			this.saveFileDialog1.Filter = "Setting|*.VHCfg";
			this.saveFileDialog1.Title = "Save Visualization Settings";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.DefaultExt = "VHCfg";
			this.openFileDialog1.FileName = "VisualHistorySettings1.VHCfg";
			this.openFileDialog1.Filter = "Setting|*.VHCfg";
			this.openFileDialog1.Title = "Open Visualization Settings";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.liveStreamRadioButton);
			this.groupBox5.Controls.Add(this.historyRadioButton);
			this.groupBox5.Location = new System.Drawing.Point(14, 22);
			this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox5.Size = new System.Drawing.Size(418, 89);
			this.groupBox5.TabIndex = 5;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Play Mode";
			// 
			// liveStreamRadioButton
			// 
			this.liveStreamRadioButton.AutoSize = true;
			this.liveStreamRadioButton.Location = new System.Drawing.Point(199, 41);
			this.liveStreamRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.liveStreamRadioButton.Name = "liveStreamRadioButton";
			this.liveStreamRadioButton.Size = new System.Drawing.Size(183, 21);
			this.liveStreamRadioButton.TabIndex = 1;
			this.liveStreamRadioButton.Text = "Live Changes (real time)";
			this.liveStreamRadioButton.UseVisualStyleBackColor = true;
			this.liveStreamRadioButton.CheckedChanged += new System.EventHandler(this.historyRadioButton_CheckedChanged);
			// 
			// historyRadioButton
			// 
			this.historyRadioButton.AutoSize = true;
			this.historyRadioButton.Checked = true;
			this.historyRadioButton.Location = new System.Drawing.Point(29, 41);
			this.historyRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.historyRadioButton.Name = "historyRadioButton";
			this.historyRadioButton.Size = new System.Drawing.Size(133, 21);
			this.historyRadioButton.TabIndex = 0;
			this.historyRadioButton.TabStop = true;
			this.historyRadioButton.Text = "Visualize History";
			this.historyRadioButton.UseVisualStyleBackColor = true;
			this.historyRadioButton.CheckedChanged += new System.EventHandler(this.historyRadioButton_CheckedChanged);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageGeneral);
			this.tabControl1.Controls.Add(this.tabPageVisualization);
			this.tabControl1.Controls.Add(this.tabPageFilters);
			this.tabControl1.Location = new System.Drawing.Point(12, 74);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Padding = new System.Drawing.Point(16, 6);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(454, 373);
			this.tabControl1.TabIndex = 15;
			// 
			// tabPageGeneral
			// 
			this.tabPageGeneral.Controls.Add(this.groupBox5);
			this.tabPageGeneral.Controls.Add(this.historySettingsGroupBox);
			this.tabPageGeneral.Location = new System.Drawing.Point(4, 31);
			this.tabPageGeneral.Name = "tabPageGeneral";
			this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(10, 18, 10, 18);
			this.tabPageGeneral.Size = new System.Drawing.Size(446, 338);
			this.tabPageGeneral.TabIndex = 0;
			this.tabPageGeneral.Text = "General";
			this.tabPageGeneral.UseVisualStyleBackColor = true;
			// 
			// tabPageVisualization
			// 
			this.tabPageVisualization.Controls.Add(this.groupBox4);
			this.tabPageVisualization.Controls.Add(this.groupBox3);
			this.tabPageVisualization.Location = new System.Drawing.Point(4, 31);
			this.tabPageVisualization.Name = "tabPageVisualization";
			this.tabPageVisualization.Padding = new System.Windows.Forms.Padding(10, 18, 10, 18);
			this.tabPageVisualization.Size = new System.Drawing.Size(446, 338);
			this.tabPageVisualization.TabIndex = 1;
			this.tabPageVisualization.Text = "Visualization";
			this.tabPageVisualization.UseVisualStyleBackColor = true;
			// 
			// tabPageFilters
			// 
			this.tabPageFilters.Controls.Add(this.groupBox1);
			this.tabPageFilters.Controls.Add(this.groupBox2);
			this.tabPageFilters.Location = new System.Drawing.Point(4, 31);
			this.tabPageFilters.Name = "tabPageFilters";
			this.tabPageFilters.Padding = new System.Windows.Forms.Padding(10, 18, 10, 18);
			this.tabPageFilters.Size = new System.Drawing.Size(446, 338);
			this.tabPageFilters.TabIndex = 2;
			this.tabPageFilters.Text = "Filters";
			this.tabPageFilters.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.setResolutionCheckBox);
			this.groupBox4.Controls.Add(this.fullScreenCheckBox);
			this.groupBox4.Controls.Add(this.resolutionHeightTextBox);
			this.groupBox4.Controls.Add(this.label10);
			this.groupBox4.Controls.Add(this.resolutionWidthTextBox);
			this.groupBox4.Location = new System.Drawing.Point(14, 211);
			this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox4.Size = new System.Drawing.Size(420, 105);
			this.groupBox4.TabIndex = 14;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "View Size";
			// 
			// setResolutionCheckBox
			// 
			this.setResolutionCheckBox.AutoSize = true;
			this.setResolutionCheckBox.Location = new System.Drawing.Point(19, 62);
			this.setResolutionCheckBox.Margin = new System.Windows.Forms.Padding(4);
			this.setResolutionCheckBox.Name = "setResolutionCheckBox";
			this.setResolutionCheckBox.Size = new System.Drawing.Size(122, 21);
			this.setResolutionCheckBox.TabIndex = 1;
			this.setResolutionCheckBox.Text = "Set Resolution";
			this.setResolutionCheckBox.UseVisualStyleBackColor = true;
			this.setResolutionCheckBox.CheckedChanged += new System.EventHandler(this.setResolutionCheckBox_CheckedChanged);
			// 
			// fullScreenCheckBox
			// 
			this.fullScreenCheckBox.AutoSize = true;
			this.fullScreenCheckBox.Location = new System.Drawing.Point(19, 31);
			this.fullScreenCheckBox.Margin = new System.Windows.Forms.Padding(4);
			this.fullScreenCheckBox.Name = "fullScreenCheckBox";
			this.fullScreenCheckBox.Size = new System.Drawing.Size(255, 21);
			this.fullScreenCheckBox.TabIndex = 0;
			this.fullScreenCheckBox.Text = "Full Screen mode (toggle Alt+Enter)";
			this.fullScreenCheckBox.UseVisualStyleBackColor = true;
			// 
			// resolutionHeightTextBox
			// 
			this.resolutionHeightTextBox.Enabled = false;
			this.resolutionHeightTextBox.Location = new System.Drawing.Point(253, 60);
			this.resolutionHeightTextBox.Margin = new System.Windows.Forms.Padding(4);
			this.resolutionHeightTextBox.MaxLength = 4;
			this.resolutionHeightTextBox.Name = "resolutionHeightTextBox";
			this.resolutionHeightTextBox.Size = new System.Drawing.Size(56, 22);
			this.resolutionHeightTextBox.TabIndex = 3;
			this.resolutionHeightTextBox.Text = "600";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(229, 63);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(14, 17);
			this.label10.TabIndex = 3;
			this.label10.Text = "x";
			// 
			// resolutionWidthTextBox
			// 
			this.resolutionWidthTextBox.Enabled = false;
			this.resolutionWidthTextBox.Location = new System.Drawing.Point(159, 60);
			this.resolutionWidthTextBox.Margin = new System.Windows.Forms.Padding(4);
			this.resolutionWidthTextBox.MaxLength = 4;
			this.resolutionWidthTextBox.Name = "resolutionWidthTextBox";
			this.resolutionWidthTextBox.Size = new System.Drawing.Size(61, 22);
			this.resolutionWidthTextBox.TabIndex = 2;
			this.resolutionWidthTextBox.Text = "800";
			// 
			// SettingForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(477, 515);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.resetToDefaultButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pathTextBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Visualization Settings";
			this.historySettingsGroupBox.ResumeLayout(false);
			this.historySettingsGroupBox.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPageGeneral.ResumeLayout(false);
			this.tabPageVisualization.ResumeLayout(false);
			this.tabPageFilters.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox historySettingsGroupBox;
        private System.Windows.Forms.Label dateToLabel;
        private System.Windows.Forms.Label dateFromLabel;
        private System.Windows.Forms.DateTimePicker dateToPicker;
        private System.Windows.Forms.DateTimePicker dateFromPicker;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox userExcludeTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userIncludeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button resetToDefaultButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox filesExcludeTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox filesIncludeTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox viewFileNamesCheckBox;
        private System.Windows.Forms.TextBox secondsPerDayTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox maxFilesTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem restoreDefaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interactiveKeyboardCommandsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.CheckBox loopPlaybackCheckBox;
        private System.Windows.Forms.CheckBox viewDirNamesCheckBox;
        private System.Windows.Forms.ComboBox timeScaleComboBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox viewUserNamesCheckBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton liveStreamRadioButton;
        private System.Windows.Forms.RadioButton historyRadioButton;
		private System.Windows.Forms.CheckBox viewFilesExtentionMapCheckBox;
		private System.Windows.Forms.CheckBox viewAvatarsCheckBox;
		private System.Windows.Forms.TextBox LogoFileTextBox;
		private System.Windows.Forms.CheckBox ViewLogoCheckBox;
		private System.Windows.Forms.Button selectLogoFileButton;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageGeneral;
		private System.Windows.Forms.TabPage tabPageVisualization;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox setResolutionCheckBox;
		private System.Windows.Forms.CheckBox fullScreenCheckBox;
		private System.Windows.Forms.TextBox resolutionHeightTextBox;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox resolutionWidthTextBox;
		private System.Windows.Forms.TabPage tabPageFilters;
    }
}