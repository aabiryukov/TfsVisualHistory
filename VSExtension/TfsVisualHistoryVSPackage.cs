using Sitronics.TfsVisualHistory.Common;
using Sitronics.VisualStudioSelector;

namespace Sitronics.TfsVisualHistory.VSExtension
{
    using System;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
  
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// <para></para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// <para></para>
    /// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    /// a package.
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true)]

    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "2.0", IconResourceID = 400)]

    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.GuidTfsVisualHistoryVSExtensionPkgString)]
    public sealed class TfsVisualHistoryVSExtensionPackage : Package
    {
		private ITeamExplorerIntegrator m_teamexpIntegrator;
		private VisualStudioVersion m_vsVersion = VisualStudioVersion.Unknown;

        /// <summary>
        /// Initializes a new instance of the TfsVisualHistoryVSExtensionPackage class.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public TfsVisualHistoryVSExtensionPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }
/*
		private static IEnumerable<Type> GetTypesSafely(Assembly assembly)
		{
			try
			{
				return assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				return ex.Types.Where(x => x != null);
			}
		}
*/
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFrom")]
		public ITeamExplorerIntegrator TeamExplorerIntegrator
        {
            get
            {
	            if (m_teamexpIntegrator != null) return m_teamexpIntegrator;

/*
	            var pluginPath = Path.Combine(
		            Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) ?? "unknown",
		            "Sitronics.TfsVisualHistory.VS2012.dll");
	            var assembly = Assembly.LoadFrom(pluginPath);

	            foreach (var type in GetTypesSafely(assembly))
	            {
		            if (type.IsAbstract || !type.IsClass || !type.IsPublic) continue;
		            if (!type.IsSubclassOf(typeof(ITeamExplorerIntegrator))) continue;

		            m_teamexpIntegrator = (ITeamExplorerIntegrator)Activator.CreateInstance(type);
		            m_teamexpIntegrator.Initialize(
			            GetService(typeof(EnvDTE.IVsExtensibility)) as EnvDTE.IVsExtensibility
			            //                        ,(ITeamFoundationContextManager)GetService(typeof(ITeamFoundationContextManager))
			            );
		            break;
	            }

	            if (m_teamexpIntegrator == null)
	            {
		            throw new ApplicationException("Interface ITeamExplorerIntegrator not found!");
	            }
*/
				//typeof(Package).Assembly.Location
				m_teamexpIntegrator = VSSelector.CreateTeamExplorerIntegrator(m_vsVersion);
				m_teamexpIntegrator.Initialize(
					GetService(typeof(EnvDTE.IVsExtensibility)) as EnvDTE.IVsExtensibility
					//                        ,(ITeamFoundationContextManager)GetService(typeof(ITeamFoundationContextManager))
					);

	            return m_teamexpIntegrator;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

#if DEBUG
	        System.Windows.Forms.MessageBox.Show("UserRegistryRoot=" + UserRegistryRoot);
#endif

			if (UserRegistryRoot.ToString().Contains(@"\11"))
	        {
		        m_vsVersion = VisualStudioVersion.VS2012;
	        }
	        else
	        {
				m_vsVersion = VisualStudioVersion.VS2013;
			}

	        // Add our command handlers for menu (commands must exist in the .vsct file)
            var mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the menu item.
                var menuCommandID = new CommandID(GuidList.GuidTfsVisualHistoryVSExtensionCmdSet, (int)PkgCmdIDList.CmdidSitronicsMotionTooling);
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
                mcs.AddCommand(menuItem);
            }
        }
        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            var uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
             
            try
            {
                uiShell.SetWaitCursor();
             
                switch ((uint)((MenuCommand)sender).CommandID.ID)
                {
                    case PkgCmdIDList.CmdidSitronicsMotionTooling:
                        // Views.SelectBranchPlan dlg = new Views.SelectBranchPlan(TeamExplorerIntegrator);
                        // WindowHelper.ShowModal(dlg);
                        // System.Windows.Forms.MessageBox.Show("CurrentSourceControlFolder=" + (TeamExplorerIntegrator.CurrentSourceControlFolder ?? "null"));
                        // System.Windows.Forms.MessageBox.Show("TeamProjectCollectionUri=" + (TeamExplorerIntegrator.TeamProjectCollectionUri != null ? TeamExplorerIntegrator.TeamProjectCollectionUri.ToString() : "null"));

						if (TeamExplorerIntegrator != null)
						{
							TeamExplorerIntegrator.ViewHistory();
                        }
                        break;
                }                
            }
            catch (Exception ex)
            {
                var clsid = Guid.Empty;
                int result;
                Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(uiShell.ShowMessageBox(
                           0,
                           ref clsid,
                           "TfsSourceControlVisualization",
                           string.Format(CultureInfo.CurrentCulture, "{0}\n\nDetails:\n{1}", ex.Message, ex.ToString()),
                           string.Empty,
                           0,
                           OLEMSGBUTTON.OLEMSGBUTTON_OK,
                           OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
                           OLEMSGICON.OLEMSGICON_CRITICAL,
                           0,        // false
                           out result));
            }
        }
    }
}
