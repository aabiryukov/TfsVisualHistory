using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Sitronics.TfsVisualHistory.Common;
using Sitronics.TfsVisualHistory.VSExtension.Helpers;
using Sitronics.VisualStudioSelector;

namespace Sitronics.TfsVisualHistory.VSExtension
{
    internal class ViewSourceControlHistoryCommand
    {
        private Package _package;

        private ITeamExplorerIntegrator _teamexpIntegrator;
        private readonly VisualStudioVersion _vsVersion;

        private ViewSourceControlHistoryCommand(Package package)
        {
            _package = package;

#if DEBUG
            MessageHelper.ShowMessage("UserRegistryRoot=" + _package.UserRegistryRoot);
#endif

            // Detect Visual Studio version
            {
                var vsRegistryRoot = _package.UserRegistryRoot.ToString();

                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if (vsRegistryRoot.Contains(@"\12"))
                {
                    _vsVersion = VisualStudioVersion.VS2013;
                }
                else
                if (vsRegistryRoot.Contains(@"\14"))
                {
                    _vsVersion = VisualStudioVersion.VS2015;
                }
                else
                if (vsRegistryRoot.Contains(@"\15"))
                {
                    _vsVersion = VisualStudioVersion.VS2017;
                }
                else
                if (vsRegistryRoot.Contains(@"\16"))
                {
                    _vsVersion = VisualStudioVersion.VS2019;
                }
                else
                {
                    throw new ApplicationException($"Unknown version of VisualStudioVersion: {vsRegistryRoot}");
                }
            }

            // Add our command handlers for menu (commands must exist in the .vsct file)
            if (ServiceProvider.GetService(typeof(IMenuCommandService)) is OleMenuCommandService menuCommandService)
            {
                // Create the command for the menu item.
                var menuCommandID = new CommandID(GuidList.GuidTfsVisualHistoryVSExtensionCmdSet, (int)PkgCmdIDList.CmdSitronicsVisualizeHistory);
                var menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
                /*
                                menuItem.BeforeQueryStatus += (sender, evt) =>
                                {
                                    // I don't need this next line since this is a lambda.
                                    // But I just wanted to show that sender is the OleMenuCommand.
                                    var item = (OleMenuCommand)sender;

                                    // var service = (EnvDTE.DTE)this.GetService(typeof(EnvDTE.DTE));
                                    item.Enabled = false;//TeamExplorerIntegrator.IsSingleSelected;
                                };
                */
                menuCommandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static ViewSourceControlHistoryCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider => _package;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFrom")]
        public ITeamExplorerIntegrator TeamExplorerIntegrator
        {
            get
            {
                if (_teamexpIntegrator != null) return _teamexpIntegrator;

                var vsExtensibility = ServiceProvider.GetService(typeof(EnvDTE.IVsExtensibility)) as EnvDTE.IVsExtensibility;
                if(vsExtensibility == null)
                    throw new InvalidOperationException("Internal error: Interface EnvDTE.IVsExtensibility is not available (vsExtensibility is null)");

                _teamexpIntegrator = VSSelector.CreateTeamExplorerIntegrator(_vsVersion);
                _teamexpIntegrator.Initialize(vsExtensibility);

                return _teamexpIntegrator;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new ViewSourceControlHistoryCommand(package);
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
        {
//            ThreadHelper.ThrowIfNotOnUIThread();

            var uiShell = (IVsUIShell)ServiceProvider.GetService(typeof(SVsUIShell));

            if (uiShell == null)
            {
                MessageHelper.ShowMessage("Internal error: uiShell is null");
                return;
            }

            try
            {
                uiShell.SetWaitCursor();

                switch ((uint)((MenuCommand)sender).CommandID.ID)
                {
                    case PkgCmdIDList.CmdSitronicsVisualizeHistory:

                        if (TeamExplorerIntegrator != null)
                            TeamExplorerIntegrator.ViewHistory();
                        break;
                }
            }
            catch (Exception ex)
            {
                 MessageHelper.ShowShellErrorMessage(uiShell, ex);
            }
        }
    }
}
