#region File Header

// This file released under the Modified BSD License (http://www.opensource.org/licenses/bsd-license.php)
// Project hosted at: http://code.google.com/p/lvknet
//
// $Id: WaitMessage.cs 182 2007-01-04 10:24:24Z lassevk $
// $LastChangedRevision: 182 $
// $LastChangedDate: 2007-01-04 13:24:24 +0300 (Чт, 04 янв 2007) $
// $LastChangedBy: lassevk $

#endregion

#region Using

using System;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace Sitronics.TfsVisualHistory.Utility
{
	/// <summary>
	/// This class encapsulates a form that can be used from lengthy processing
	/// when the UI wouldn't normally be updated, or from a thread that wants
	/// to show progress.
	/// </summary>
	public class WaitMessage : IDisposable
	{
		#region Private Member Variables

		private readonly Object m_formLock = new Object();
		private WaitMessageForm m_form;
		private string m_text;
		private Thread m_thread;
		private bool m_verifyAbort = true;
		private bool m_disposed;
		private volatile bool m_closing;
		private readonly Cursor m_saveCursor;
	    readonly EventHandler m_onCancel;

		#endregion

		#region Construction & Destruction
/*
		/// <summary>
		/// Constructs a new <see cref="WaitMessage"/> object.
		/// </summary>
		public WaitMessage()
		{
			// Do nothing here
		}
*/

	    /// <summary>
	    /// Constructs a new <see cref="WaitMessage"/> object with the specified setup.
	    /// </summary>
	    /// <param name="showNow">
	    /// Wether to show the busy form right away.
	    /// </param>
	    /// <param name="message">
	    /// The text to show on the form.
	    /// </param>
	    /// <param name="onCancel">Event handler for cancel the message</param>
	    protected WaitMessage(bool showNow, string message, EventHandler onCancel)
		{
            Text = message ?? "Please wait... completing request...";
			AllowAbort = false;
			VerifyAbort = true;
            m_onCancel = onCancel;

			m_saveCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

			if (showNow)
				Show();
		}

        public WaitMessage(string message, EventHandler onCancel)
            : this(true, message, onCancel)
		{
		}

        public WaitMessage(string message)
            : this(true, message, null)
        {
        }

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets wether the user can abort the processing from the form.
		/// </summary>
		public bool AllowAbort { get; set; }

//        public bool Canceled { get; private set; }

		/// <summary>
		/// Gets or sets wether to verify that the user wanted to abort or not, defaults to <c>true</c>.
		/// </summary>
		public bool VerifyAbort
		{
			get { return m_verifyAbort; }

			set
			{
				m_verifyAbort = value;
			}
		}

		/// <summary>
		/// Gets or sets the text to display on the form.
		/// </summary>
		public string Text
		{
			get { return m_text; }

			set
			{
                if (m_text != value)
                {
                    m_text = (value == null) ? string.Empty : value.Trim();
                    if (m_form != null)
                    {
                        m_form.Invoke(new Procedure(() => { m_form.Message = m_text; }));
                    }
                }
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Shows the form.
		/// </summary>
		public void Show()
		{
			if (m_thread == null)
			{
				m_thread = new Thread(FormThreadMethod)
				          	{
								Name = "WaitMessage Thread", 
								IsBackground = Thread.CurrentThread.IsBackground
				          	};
				m_thread.Start();
			}
		}

        private delegate void Procedure();

		/// <summary>
		/// Closes the form.
		/// </summary>
		private void Close()
		{
			lock (m_formLock)
			{
				m_closing = true;

				if (m_form != null)
				{
					m_form.BeginInvoke(new Procedure(CloseForm));
					m_thread = null;
				}
			}
		}

		#endregion

		#region Private Methods

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		private void FormThreadMethod()
		{
			// Wait 1 sec before show window
			for(var i = 0; i < 10; ++i)
			{
				Thread.Sleep(100);
				if (m_closing)
					return;
			}

			lock (m_formLock)
			{
				if (m_closing)
					return;

                m_form = new WaitMessageForm(m_onCancel)
				{
					Message = m_text,
					TopLevel = true
				};

				if (!m_form.IsHandleCreated)
				{
					// This call forces creation of the control's handle.
					var handle = m_form.Handle;
					System.Diagnostics.Trace.WriteLine("WaitMessageForm.Handle=" + handle);
				}
			}

			try
			{
				Application.Run(m_form);
			}
			finally
			{
				lock (m_formLock)
				{
					m_form.Dispose();
					m_form = null;
				}
			}
		}

		private void CloseForm()
		{
			m_form.CanClose = true;
			m_form.Close();
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Disposes of the form, by closing it.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		// The bulk of the clean-up code is implemented in Dispose(bool)
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "m_form")]
		protected virtual void Dispose(bool disposing)
		{
			if (!m_disposed)
			{
				m_disposed = true;

				if (disposing)
				{
					Close();
				}

				Cursor.Current = m_saveCursor;
			}
		}

		#endregion

        public WaitMessageProgress CreateProgress(string format)
	    {
            return new WaitMessageProgress(this, format);
	    }
	}
}