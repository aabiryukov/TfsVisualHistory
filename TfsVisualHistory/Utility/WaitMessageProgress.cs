using System.Globalization;

namespace Sitronics.TfsVisualHistory.Utility
{
    public class WaitMessageProgress<T>
    {
        private WaitMessage m_waitMessage;
        private readonly string m_formatString;

        internal WaitMessageProgress(WaitMessage waitMessage, string formatString)
        {
            m_waitMessage = waitMessage;
            m_formatString = formatString;
        }

        public void SetValue(T value)
        {
            if (m_waitMessage != null)
                m_waitMessage.Text = string.Format(CultureInfo.CurrentCulture, m_formatString, value);
        }
//                m_waitMessage.Text = "Loading history (" + value.ToString(CultureInfo.CurrentCulture) + "% done) ...";

        public void Done() { m_waitMessage = null; }
    }
}
