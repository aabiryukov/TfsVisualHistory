using System;

namespace Sitronics.TfsVisualHistory.Common
{
    public interface ITeamExplorerIntegrator
	{
		void Initialize(EnvDTE.IVsExtensibility extensibility);

		Uri TeamProjectCollectionUri { get; }

		string TeamProjectName { get; set; }

        string GetCurrentSourceControlFolder();

		void SetSourceControlExplorerDirty(string serverPath);

		void RefreshSourceControlExplorer();
		void ViewHistory();
	}
}
