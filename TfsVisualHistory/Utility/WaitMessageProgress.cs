using System.Globalization;

namespace Sitronics.TfsVisualHistory.Utility
{
	public class WaitMessageProgress
	{
		private WaitMessage _waitMessage;
		private readonly string _formatString;

		/// <summary>
		/// Initializes a new instance of the <see cref="WaitMessageProgress"/> class.
		/// </summary>
		internal WaitMessageProgress(WaitMessage waitMessage, string formatString)
		{
			LastValue = -1;
			_waitMessage = waitMessage;
			_formatString = formatString;
		}

		public int LastValue { get; private set; }

		public void SetValue(int value)
		{
			if (LastValue == value)
				return;

			LastValue = value;

			if (_waitMessage != null)
				_waitMessage.Text = string.Format(CultureInfo.CurrentCulture, _formatString, value);
		}

		public void Done()
		{
			_waitMessage = null;
		}
	}
}
