using System;
using System.Windows.Forms;
using Microsoft.Win32;
using Sitronics.TfsVisualHistory.VSExtension;

namespace TfsVisualHistoryTest
{
    public partial class MainForm : Form
    {
        private const string RegistyPathTfsName     = @"Software\Sitronics\TfsVisualHistory\TfsUrl";
        private const string RegistyPathTfsFolder   = @"Software\Sitronics\TfsVisualHistory\TfsFolder";
 
        public MainForm()
        {
            InitializeComponent();

            // Load recent settigns
            tfsUriTextBox.Text = (string)Registry.CurrentUser.GetValue(RegistyPathTfsName, "https://tfs.xxxx.com/tfs");
            sourceControlFolderTextBox.Text = (string)Registry.CurrentUser.GetValue(RegistyPathTfsFolder, "$/FORIS_Mobile/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Save recent settigns
            Registry.CurrentUser.SetValue(RegistyPathTfsName, tfsUriTextBox.Text);
            Registry.CurrentUser.SetValue(RegistyPathTfsFolder, sourceControlFolderTextBox.Text);

            HistoryViewer.ViewHistory(new Uri(tfsUriTextBox.Text), sourceControlFolderTextBox.Text);
        }
    }
}
