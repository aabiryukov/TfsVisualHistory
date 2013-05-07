namespace TfsVisualHistoryTest
{
    partial class MainForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tfsUriTextBox = new System.Windows.Forms.TextBox();
            this.sourceControlFolderTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(373, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "TFS Uri";
            // 
            // tfsUriTextBox
            // 
            this.tfsUriTextBox.Location = new System.Drawing.Point(73, 12);
            this.tfsUriTextBox.Name = "tfsUriTextBox";
            this.tfsUriTextBox.Size = new System.Drawing.Size(427, 20);
            this.tfsUriTextBox.TabIndex = 2;
            this.tfsUriTextBox.Text = "https://tfs.xbss.nvision-group.com/sts";
            // 
            // sourceControlFolderTextBox
            // 
            this.sourceControlFolderTextBox.Location = new System.Drawing.Point(73, 38);
            this.sourceControlFolderTextBox.Name = "sourceControlFolderTextBox";
            this.sourceControlFolderTextBox.Size = new System.Drawing.Size(427, 20);
            this.sourceControlFolderTextBox.TabIndex = 4;
            this.sourceControlFolderTextBox.Text = "$/FORIS_Mobile/TeamWork/_Public";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "SC Folder";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 112);
            this.Controls.Add(this.sourceControlFolderTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tfsUriTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tfsUriTextBox;
        private System.Windows.Forms.TextBox sourceControlFolderTextBox;
        private System.Windows.Forms.Label label2;
    }
}

