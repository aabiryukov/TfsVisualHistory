using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitronics.TfsVisualHistory.VSExtension.Helpers
{
    internal static class MessageHelper
    {
        private const string MessageTitle = "TfsSourceControlVisualization";

        public static void ShowShellErrorMessage(IVsUIShell uiShell, string message)
        {
            if (uiShell == null) throw new ArgumentNullException(nameof(uiShell));

            var clsid = Guid.Empty;

            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(uiShell.ShowMessageBox(
                       0,
                       ref clsid,
                       MessageTitle,
                       message,
                       string.Empty,
                       0,
                       OLEMSGBUTTON.OLEMSGBUTTON_OK,
                       OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
                       OLEMSGICON.OLEMSGICON_CRITICAL,
                       0,        // false
                       out int result));
        }

        public static void ShowShellErrorMessage(IVsUIShell uiShell, Exception ex)
        {
            ShowShellErrorMessage(uiShell, string.Format(CultureInfo.CurrentCulture, "{0}\n\nDetails:\n{1}", ex.Message, ex));
        }

        public static void ShowMessage(string message)
        {
            System.Windows.Forms.MessageBox.Show(message, MessageTitle);
        }
    }
}
