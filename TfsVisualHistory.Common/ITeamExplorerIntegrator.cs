using System;
using EnvDTE;

namespace Sitronics.TfsVisualHistory.Common
{
	public interface ITeamExplorerIntegrator
	{
		Uri TeamProjectCollectionUri { get; }

		string TeamProjectName { get; set; }

		string CurrentSourceControlFolder { get; }

		void Initialize(IVsExtensibility extensibility);

		void SetSourceControlExplorerDirty(string serverPath);

		void RefreshSourceControlExplorer();

		void ViewHistory();
	}
}
