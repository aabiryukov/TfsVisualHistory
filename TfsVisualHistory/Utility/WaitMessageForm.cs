using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sitronics.TfsVisualHistory.Utility
{
    internal partial class WaitMessageForm : Form
    {
		#region Private Member Variables

		internal bool CanClose;
        EventHandler m_onCancel;
	
		#endregion
		
    	public string Message
    	{
			get { return labelWaitMessage.Text; }
			set { labelWaitMessage.Text = value; }
    	}

        public WaitMessageForm()
        {
            InitializeComponent();
        }

        public WaitMessageForm(EventHandler onCancel)
            : this()
        {
            m_onCancel = onCancel;
        }        

        private void WaitMessage_Paint(object sender, PaintEventArgs e)
        {
			using (var borderPen = new Pen(SystemColors.ControlDark, 1f))
			{
				var rect = ClientRectangle;
				rect.Width--;
				rect.Height--;
				e.Graphics.DrawRectangle(borderPen, rect);
			}
        }

		#region Private Methods

		private void WaitMessageForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose)
				e.Cancel = true;
		}
		#endregion

        private void cancelButton_Click(object sender, EventArgs e)
        {
            cancelButton.Text = "Canceling...";
            cancelButton.Enabled = false;

            if (m_onCancel != null)
            {
                m_onCancel(sender, e);
            }
        }
	}
}