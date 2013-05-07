using System;
using System.Windows.Forms;
using Sitronics.TfsVisualHistory.VSExtension;

namespace TfsVisualHistoryTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HistoryViewer.ViewHistory(new Uri(tfsUriTextBox.Text), sourceControlFolderTextBox.Text);
        }
    }
}
