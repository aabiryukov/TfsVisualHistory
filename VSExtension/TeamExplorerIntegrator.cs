namespace Sitronics.TfsVisualHistory.VSExtension
{
    using System;    
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using EnvDTE80;
    using Extensibility;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.VersionControl.Client;
    using Microsoft.VisualStudio.TeamFoundation;
    using Microsoft.VisualStudio.TeamFoundation.VersionControl;

    public interface ITeamExplorerIntegrator
    {
        Uri TeamProjectCollectionUri { get; }

        string TeamProjectName { get; set; }

        string CurrentSourceControlFolder { get; }

        void SetSourceControlExplorerDirty(string serverPath);

        void RefreshSourceControlExplorer();
    }

    public class TeamExplorerIntegrator : ITeamExplorerIntegrator
    {
//        private ITeamFoundationContextManager m_tfsContextManager;
        private readonly DTE2 m_applicationObject;

        private readonly VersionControlExt m_srcCtrlExplorer;
        private TeamFoundationServerExt m_tfsExt;

        private readonly List<string> m_dirtyPath;
 
        /// <summary>Initializes a new instance of the TeamExplorerIntegrator class, The constructor for the Add-in object. Place your initialization code within this method.</summary> 
        public TeamExplorerIntegrator(EnvDTE.IVsExtensibility extensibility, ITeamFoundationContextManager te)
        {
            if (extensibility == null)
                throw new ArgumentNullException("extensibility");

            m_dirtyPath = new List<string>();
//            m_tfsContextManager = te;

            // get IDE Globals object and DTE from that
            var dte2 = extensibility.GetGlobalsObject(null).DTE as DTE2;
            m_applicationObject = dte2;

            if (dte2 == null)
                throw new ApplicationException("No DTE2");

            var tfsExt = (TeamFoundationServerExt)dte2.Application.GetObject("Microsoft.VisualStudio.TeamFoundation.TeamFoundationServerExt");
            m_srcCtrlExplorer = (VersionControlExt)dte2.Application.GetObject("Microsoft.VisualStudio.TeamFoundation.VersionControl.VersionControlExt");

            DoConnect(tfsExt);
        } 

        public string TeamProjectName { get; set; }

        public TfsTeamProjectCollection TeamProjectCollection { get; private set; }

        public Uri TeamProjectCollectionUri { get { return TeamProjectCollection.Uri; } }

        public bool IsSingleSelected
        {
            get { return m_srcCtrlExplorer.Explorer.SelectedItems.Length == 1; }
        }

        public string CurrentSourceControlFolder
        {
            get 
            {
                var itm = m_srcCtrlExplorer.Explorer.SelectedItems.First();
                return itm == null 
                    ? m_srcCtrlExplorer.Explorer.CurrentFolderItem.SourceServerPath 
                    : itm.SourceServerPath;
            }
        }
        
        /// <summary>Implements the ExecuteCommand method.</summary>         
        /// <param name='command'>The command to be executed.</param> 
        /// <param name='parameter'>String that are the parameter to the command to execute.</param>               
        public void ExecuteCommands(string command, string parameter)
        {
            m_applicationObject.ExecuteCommand(command, parameter);
        }

        public void DoConnect(TeamFoundationServerExt tfsEx)
        {
            try
            {
                m_tfsExt = tfsEx;              
 
                if (null != m_tfsExt)
                {
                    m_tfsExt.ProjectContextChanged += TfsExt_ProjectContextChanged;

                    if (null != m_tfsExt.ActiveProjectContext)
                    {
                        // Run the event handler without the event actually having fired, so we pick up the initial state. 
                        TfsExt_ProjectContextChanged(null, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("[TfsVisualHistory][DoConnect] " + ex.Message);
                ////MessageBox.Show(ex.Message); 
            }
        }

        public void RefreshSourceControlExplorer()
        {
            foreach (string s in m_dirtyPath)
            {
                try
                {
                    m_srcCtrlExplorer.Explorer.Workspace.Get(
                        new GetRequest(s, RecursionType.OneLevel, VersionSpec.Latest),
                        GetOptions.Preview);
                }
                catch(Exception exception)
                {
                    Trace.TraceError("[TfsVisualHistory][Refresh] " + exception.Message);
                }
            }

            m_srcCtrlExplorer.Explorer.Workspace.Refresh();
        }

        public void SetSourceControlExplorerDirty(string serverPath)
        {
            m_dirtyPath.Add(serverPath);
        }

        /// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary> 
        /// <param name='disconnectMode'>Describes how the Add-in is being unloaded.</param> 
        /// <param name='custom'>Array of parameters that are host application specific.</param> 
        /// <seealso class='IDTExtensibility2' /> 
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom) 
        { 
            // Unhook the ProjectContextChanged event handler. 
            if (null != m_tfsExt) 
            { 
                m_tfsExt.ProjectContextChanged -= TfsExt_ProjectContextChanged; 
                m_tfsExt = null; 
            } 
        } 

        /// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary> 
        /// <param name='custom'>Array of parameters that are host application specific.</param> 
        /// <seealso class='IDTExtensibility2' />        
        public void OnAddInsUpdate(ref Array custom) 
        { 
        } 

        /// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary> 
        /// <param name='custom'>Array of parameters that are host application specific.</param> 
        /// <seealso class='IDTExtensibility2' /> 
        public void OnStartupComplete(ref Array custom) 
        { 
        } 

        /// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary> 
        /// <param name='custom'>Array of parameters that are host application specific.</param> 
        /// <seealso class='IDTExtensibility2' /> 
        public void OnBeginShutdown(ref Array custom) 
        { 
        } 

        /// <summary> 
        /// Raised by the TFS Visual Studio integration package when the active project context changes. 
        /// </summary> 
        /// <param name="sender">The object that is sending the event</param> 
        /// <param name="e">The arguments of the event</param> 
        private void TfsExt_ProjectContextChanged(object sender, EventArgs e) 
        { 
            if (null != m_tfsExt.ActiveProjectContext && 
                !string.IsNullOrEmpty(m_tfsExt.ActiveProjectContext.DomainUri)) 
            { 
                SwitchToTfs(TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(m_tfsExt.ActiveProjectContext.DomainUri)));
                TeamProjectName = m_tfsExt.ActiveProjectContext.ProjectName;
            } 
            else 
            { 
                SwitchToTfs(null); 
            }           
        } 

        private void SwitchToTfs(TfsTeamProjectCollection tfs) 
        { 
            if (ReferenceEquals(TeamProjectCollection, tfs)) 
            { 
                // No work to do; could be a team project switch only 
                return; 
            } 

            TeamProjectCollection = tfs;           
        }
    } 
}