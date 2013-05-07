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
        Uri TPCollectionUri { get; }

        string TPName { get; set; }

        string CurrentSourceControlFolder { get; }

        void SetSourceControlExplorerDirty(string p);

        void RefreshSourceControlExplorer();
    }

    public class TeamExplorerIntegrator : ITeamExplorerIntegrator
    {
        private ITeamFoundationContextManager m_tfsContextManager;
        private readonly DTE2 m_applicationObject;
        ////private AddIn _addInInstance; 

        private readonly VersionControlExt m_srcCtrlExplorer;
        private TeamFoundationServerExt m_tfsExt;
        private TfsTeamProjectCollection m_tfsTpCollection;

        private readonly List<string> m_dirtyPath;
 
        /// <summary>Initializes a new instance of the TeamExplorerIntegrator class, The constructor for the Add-in object. Place your initialization code within this method.</summary> 
        public TeamExplorerIntegrator(EnvDTE.IVsExtensibility extensibility, ITeamFoundationContextManager te)
        {
            this.m_dirtyPath = new List<string>();
            this.m_tfsContextManager = te;

            // get IDE Globals object and DTE from that
            EnvDTE80.DTE2 dte2 = extensibility.GetGlobalsObject(null).DTE as EnvDTE80.DTE2;
            this.m_applicationObject = dte2;

            Debug.Assert(dte2 != null, "No DTE2");

            var tfsExt = (TeamFoundationServerExt)dte2.Application.GetObject("Microsoft.VisualStudio.TeamFoundation.TeamFoundationServerExt");
            this.m_srcCtrlExplorer = (VersionControlExt)dte2.Application.GetObject("Microsoft.VisualStudio.TeamFoundation.VersionControl.VersionControlExt");

            this.DoConnect(tfsExt);
        } 

        public string TPName { get; set; }

        public TfsTeamProjectCollection TPCollection
        {
            get
            {
                return this.m_tfsTpCollection;
            }
        }

        public Uri TPCollectionUri
        {
            get
            {
                return this.m_tfsTpCollection.Uri;
            }
        }

        public bool IsSingleSelected
        {
            get { return this.m_srcCtrlExplorer.Explorer.SelectedItems.Length == 1; }
        }

        public string CurrentSourceControlFolder
        {
            get 
            {
//                VersionControlExplorerItem itm = this.srcCtrlExplorer.Explorer.SelectedItems.First(i => i.IsFolder == true);
                VersionControlExplorerItem itm = this.m_srcCtrlExplorer.Explorer.SelectedItems.First();
                if (itm == null)
                {
                    return this.m_srcCtrlExplorer.Explorer.CurrentFolderItem.SourceServerPath; 
                }
                else
                {
                    return itm.SourceServerPath;
                }
            }
        }
        
        /// <summary>Implements the ExecuteCommand method.</summary>         
        /// <param name='command'>The command to be executed.</param> 
        /// <param name='parameter'>String that are the parameter to the command to execute.</param>               
        public void ExecuteCommands(string command, string parameter)
        {
            this.m_applicationObject.ExecuteCommand(command, parameter);
        }

        public void DoConnect(TeamFoundationServerExt tfsEx)
        {
            try
            {
                this.m_tfsExt = tfsEx;              
 
                if (null != this.m_tfsExt)
                {
                    this.m_tfsExt.ProjectContextChanged += new EventHandler(this.TfsExt_ProjectContextChanged);

                    if (null != this.m_tfsExt.ActiveProjectContext)
                    {
                        // Run the event handler without the event actually having fired, so we pick up the initial state. 
                        this.TfsExt_ProjectContextChanged(null, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("***** MATTIAS **** " + ex.Message);
                ////MessageBox.Show(ex.Message); 
            }
        }

        public void RefreshSourceControlExplorer()
        {
            foreach (string s in this.m_dirtyPath)
            {
                try
                {
                    this.m_srcCtrlExplorer.Explorer.Workspace.Get(
                        new GetRequest(s, RecursionType.OneLevel, VersionSpec.Latest),
                        GetOptions.Preview);
                }
                catch
                {
                }
            }

            this.m_srcCtrlExplorer.Explorer.Workspace.Refresh();
        }

        public void SetSourceControlExplorerDirty(string serverpath)
        {
            this.m_dirtyPath.Add(serverpath);
        }

        /// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary> 
        /// <param name='disconnectMode'>Describes how the Add-in is being unloaded.</param> 
        /// <param name='custom'>Array of parameters that are host application specific.</param> 
        /// <seealso class='IDTExtensibility2' /> 
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom) 
        { 
            // Unhook the ProjectContextChanged event handler. 
            if (null != this.m_tfsExt) 
            { 
                this.m_tfsExt.ProjectContextChanged -= new EventHandler(this.TfsExt_ProjectContextChanged); 
                this.m_tfsExt = null; 
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
            if (null != this.m_tfsExt.ActiveProjectContext && 
                !string.IsNullOrEmpty(this.m_tfsExt.ActiveProjectContext.DomainUri)) 
            { 
                this.SwitchToTfs(TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(this.m_tfsExt.ActiveProjectContext.DomainUri)));
                this.TPName = this.m_tfsExt.ActiveProjectContext.ProjectName;
            } 
            else 
            { 
                this.SwitchToTfs(null); 
            }           
        } 

        private void SwitchToTfs(TfsTeamProjectCollection tfs) 
        { 
            if (object.ReferenceEquals(this.m_tfsTpCollection, tfs)) 
            { 
                // No work to do; could be a team project switch only 
                return; 
            } 

            this.m_tfsTpCollection = tfs;           
        }
    } 
}