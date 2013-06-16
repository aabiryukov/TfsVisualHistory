namespace Sitronics.TfsVisualHistory.VSExtension
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
            this.datesGroupBox = new System.Windows.Forms.GroupBox();
            this.dateToLabel = new System.Windows.Forms.Label();
            this.dateFromLabel = new System.Windows.Forms.Label();
            this.dateToPicker = new System.Windows.Forms.DateTimePicker();
            this.dateFromPicker = new System.Windows.Forms.DateTimePicker();
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
            this.loopPlaybackCheckBox = new System.Windows.Forms.CheckBox();
            this.maxFilesTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.secondsPerDayTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.hideFileNamesCheckBox = new System.Windows.Forms.CheckBox();
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.setResolutionCheckBox = new System.Windows.Forms.CheckBox();
            this.fullScreenCheckBox = new System.Windows.Forms.CheckBox();
            this.resolutionHeightTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.resolutionWidthTextBox = new System.Windows.Forms.TextBox();
            this.hideDirNamesCheckBox = new System.Windows.Forms.CheckBox();
            this.datesGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.okButton.Location = new System.Drawing.Point(392, 317);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(135, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "Start";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(533, 317);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // pathTextBox
            // 
            this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathTextBox.Location = new System.Drawing.Point(47, 27);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(561, 20);
            this.pathTextBox.TabIndex = 4;
            this.pathTextBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Path";
            // 
            // datesGroupBox
            // 
            this.datesGroupBox.Controls.Add(this.dateToLabel);
            this.datesGroupBox.Controls.Add(this.dateFromLabel);
            this.datesGroupBox.Controls.Add(this.dateToPicker);
            this.datesGroupBox.Controls.Add(this.dateFromPicker);
            this.datesGroupBox.Location = new System.Drawing.Point(14, 53);
            this.datesGroupBox.Name = "datesGroupBox";
            this.datesGroupBox.Size = new System.Drawing.Size(308, 53);
            this.datesGroupBox.TabIndex = 5;
            this.datesGroupBox.TabStop = false;
            this.datesGroupBox.Text = "History period";
            // 
            // dateToLabel
            // 
            this.dateToLabel.AutoSize = true;
            this.dateToLabel.Location = new System.Drawing.Point(161, 25);
            this.dateToLabel.Name = "dateToLabel";
            this.dateToLabel.Size = new System.Drawing.Size(23, 13);
            this.dateToLabel.TabIndex = 2;
            this.dateToLabel.Text = "To:";
            // 
            // dateFromLabel
            // 
            this.dateFromLabel.AutoSize = true;
            this.dateFromLabel.Location = new System.Drawing.Point(8, 25);
            this.dateFromLabel.Name = "dateFromLabel";
            this.dateFromLabel.Size = new System.Drawing.Size(33, 13);
            this.dateFromLabel.TabIndex = 0;
            this.dateFromLabel.Text = "From:";
            // 
            // dateToPicker
            // 
            this.dateToPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateToPicker.Location = new System.Drawing.Point(190, 25);
            this.dateToPicker.Name = "dateToPicker";
            this.dateToPicker.Size = new System.Drawing.Size(105, 20);
            this.dateToPicker.TabIndex = 3;
            // 
            // dateFromPicker
            // 
            this.dateFromPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFromPicker.Location = new System.Drawing.Point(47, 24);
            this.dateFromPicker.Name = "dateFromPicker";
            this.dateFromPicker.Size = new System.Drawing.Size(105, 20);
            this.dateFromPicker.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.userExcludeTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.userIncludeTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(328, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 89);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User name filter";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Example: *John*; MyDomain\\*; *robot";
            // 
            // userExcludeTextBox
            // 
            this.userExcludeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.userExcludeTextBox.Location = new System.Drawing.Point(59, 44);
            this.userExcludeTextBox.Name = "userExcludeTextBox";
            this.userExcludeTextBox.Size = new System.Drawing.Size(215, 20);
            this.userExcludeTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Exclude:";
            // 
            // userIncludeTextBox
            // 
            this.userIncludeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.userIncludeTextBox.Location = new System.Drawing.Point(59, 18);
            this.userIncludeTextBox.Name = "userIncludeTextBox";
            this.userIncludeTextBox.Size = new System.Drawing.Size(215, 20);
            this.userIncludeTextBox.TabIndex = 1;
            this.userIncludeTextBox.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Include:";
            // 
            // resetToDefaultButton
            // 
            this.resetToDefaultButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetToDefaultButton.Location = new System.Drawing.Point(15, 317);
            this.resetToDefaultButton.Name = "resetToDefaultButton";
            this.resetToDefaultButton.Size = new System.Drawing.Size(96, 23);
            this.resetToDefaultButton.TabIndex = 9;
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
            this.groupBox2.Location = new System.Drawing.Point(328, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 89);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File type filter";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Example: *.cs;*.txt";
            // 
            // filesExcludeTextBox
            // 
            this.filesExcludeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filesExcludeTextBox.Location = new System.Drawing.Point(59, 44);
            this.filesExcludeTextBox.Name = "filesExcludeTextBox";
            this.filesExcludeTextBox.Size = new System.Drawing.Size(215, 20);
            this.filesExcludeTextBox.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Exclude:";
            // 
            // filesIncludeTextBox
            // 
            this.filesIncludeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filesIncludeTextBox.Location = new System.Drawing.Point(59, 18);
            this.filesIncludeTextBox.Name = "filesIncludeTextBox";
            this.filesIncludeTextBox.Size = new System.Drawing.Size(215, 20);
            this.filesIncludeTextBox.TabIndex = 1;
            this.filesIncludeTextBox.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Include:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.hideDirNamesCheckBox);
            this.groupBox3.Controls.Add(this.loopPlaybackCheckBox);
            this.groupBox3.Controls.Add(this.maxFilesTextBox);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.secondsPerDayTextBox);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.hideFileNamesCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(15, 188);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(308, 123);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Visualization";
            // 
            // loopPlaybackCheckBox
            // 
            this.loopPlaybackCheckBox.AutoSize = true;
            this.loopPlaybackCheckBox.Location = new System.Drawing.Point(13, 100);
            this.loopPlaybackCheckBox.Name = "loopPlaybackCheckBox";
            this.loopPlaybackCheckBox.Size = new System.Drawing.Size(96, 17);
            this.loopPlaybackCheckBox.TabIndex = 6;
            this.loopPlaybackCheckBox.Text = "Loop playback";
            this.loopPlaybackCheckBox.UseVisualStyleBackColor = true;
            // 
            // maxFilesTextBox
            // 
            this.maxFilesTextBox.Location = new System.Drawing.Point(201, 74);
            this.maxFilesTextBox.MaxLength = 7;
            this.maxFilesTextBox.Name = "maxFilesTextBox";
            this.maxFilesTextBox.Size = new System.Drawing.Size(51, 20);
            this.maxFilesTextBox.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(133, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Max number of active files:";
            // 
            // secondsPerDayTextBox
            // 
            this.secondsPerDayTextBox.Location = new System.Drawing.Point(201, 48);
            this.secondsPerDayTextBox.MaxLength = 5;
            this.secondsPerDayTextBox.Name = "secondsPerDayTextBox";
            this.secondsPerDayTextBox.Size = new System.Drawing.Size(51, 20);
            this.secondsPerDayTextBox.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(192, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Seconds per day (1-1000, 0 - real time):";
            // 
            // hideFileNamesCheckBox
            // 
            this.hideFileNamesCheckBox.AutoSize = true;
            this.hideFileNamesCheckBox.Location = new System.Drawing.Point(13, 28);
            this.hideFileNamesCheckBox.Name = "hideFileNamesCheckBox";
            this.hideFileNamesCheckBox.Size = new System.Drawing.Size(98, 17);
            this.hideFileNamesCheckBox.TabIndex = 1;
            this.hideFileNamesCheckBox.Text = "Hide file names";
            this.hideFileNamesCheckBox.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(620, 24);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadSettingsToolStripMenuItem
            // 
            this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
            this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.loadSettingsToolStripMenuItem.Text = "&Load Settings";
            this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(195, 6);
            // 
            // saveSettingsToFileToolStripMenuItem
            // 
            this.saveSettingsToFileToolStripMenuItem.Name = "saveSettingsToFileToolStripMenuItem";
            this.saveSettingsToFileToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.saveSettingsToFileToolStripMenuItem.Text = "&Save Settings As...";
            this.saveSettingsToFileToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToFileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(195, 6);
            // 
            // restoreDefaultToolStripMenuItem
            // 
            this.restoreDefaultToolStripMenuItem.Name = "restoreDefaultToolStripMenuItem";
            this.restoreDefaultToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.restoreDefaultToolStripMenuItem.Text = "Restore Default settings";
            this.restoreDefaultToolStripMenuItem.Click += new System.EventHandler(this.resetToDefaultButton_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.interactiveKeyboardCommandsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // interactiveKeyboardCommandsToolStripMenuItem
            // 
            this.interactiveKeyboardCommandsToolStripMenuItem.Name = "interactiveKeyboardCommandsToolStripMenuItem";
            this.interactiveKeyboardCommandsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.setResolutionCheckBox);
            this.groupBox4.Controls.Add(this.fullScreenCheckBox);
            this.groupBox4.Controls.Add(this.resolutionHeightTextBox);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.resolutionWidthTextBox);
            this.groupBox4.Location = new System.Drawing.Point(14, 112);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(307, 70);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "View Size";
            // 
            // setResolutionCheckBox
            // 
            this.setResolutionCheckBox.AutoSize = true;
            this.setResolutionCheckBox.Location = new System.Drawing.Point(14, 44);
            this.setResolutionCheckBox.Name = "setResolutionCheckBox";
            this.setResolutionCheckBox.Size = new System.Drawing.Size(95, 17);
            this.setResolutionCheckBox.TabIndex = 7;
            this.setResolutionCheckBox.Text = "Set Resolution";
            this.setResolutionCheckBox.UseVisualStyleBackColor = true;
            this.setResolutionCheckBox.CheckedChanged += new System.EventHandler(this.setResolutionCheckBox_CheckedChanged);
            // 
            // fullScreenCheckBox
            // 
            this.fullScreenCheckBox.AutoSize = true;
            this.fullScreenCheckBox.Location = new System.Drawing.Point(14, 19);
            this.fullScreenCheckBox.Name = "fullScreenCheckBox";
            this.fullScreenCheckBox.Size = new System.Drawing.Size(192, 17);
            this.fullScreenCheckBox.TabIndex = 6;
            this.fullScreenCheckBox.Text = "Full Screen mode (toggle Alt+Enter)";
            this.fullScreenCheckBox.UseVisualStyleBackColor = true;
            // 
            // resolutionHeightTextBox
            // 
            this.resolutionHeightTextBox.Enabled = false;
            this.resolutionHeightTextBox.Location = new System.Drawing.Point(190, 42);
            this.resolutionHeightTextBox.MaxLength = 4;
            this.resolutionHeightTextBox.Name = "resolutionHeightTextBox";
            this.resolutionHeightTextBox.Size = new System.Drawing.Size(43, 20);
            this.resolutionHeightTextBox.TabIndex = 4;
            this.resolutionHeightTextBox.Text = "600";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(172, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(12, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "x";
            // 
            // resolutionWidthTextBox
            // 
            this.resolutionWidthTextBox.Enabled = false;
            this.resolutionWidthTextBox.Location = new System.Drawing.Point(119, 42);
            this.resolutionWidthTextBox.MaxLength = 4;
            this.resolutionWidthTextBox.Name = "resolutionWidthTextBox";
            this.resolutionWidthTextBox.Size = new System.Drawing.Size(47, 20);
            this.resolutionWidthTextBox.TabIndex = 2;
            this.resolutionWidthTextBox.Text = "800";
            // 
            // hideDirNamesCheckBox
            // 
            this.hideDirNamesCheckBox.AutoSize = true;
            this.hideDirNamesCheckBox.Location = new System.Drawing.Point(134, 27);
            this.hideDirNamesCheckBox.Name = "hideDirNamesCheckBox";
            this.hideDirNamesCheckBox.Size = new System.Drawing.Size(125, 17);
            this.hideDirNamesCheckBox.TabIndex = 7;
            this.hideDirNamesCheckBox.Text = "Hide directory names";
            this.hideDirNamesCheckBox.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(620, 352);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.resetToDefaultButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.datesGroupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualization Settings";
            this.datesGroupBox.ResumeLayout(false);
            this.datesGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.GroupBox datesGroupBox;
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
        private System.Windows.Forms.CheckBox hideFileNamesCheckBox;
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox resolutionHeightTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox resolutionWidthTextBox;
        private System.Windows.Forms.CheckBox setResolutionCheckBox;
        private System.Windows.Forms.CheckBox fullScreenCheckBox;
        private System.Windows.Forms.CheckBox loopPlaybackCheckBox;
        private System.Windows.Forms.CheckBox hideDirNamesCheckBox;
    }
}