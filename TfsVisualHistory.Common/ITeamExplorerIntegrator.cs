using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitronics.TfsVisualHistory.Common
{
	public interface ITeamExplorerIntegrator
	{
		void Initialize(EnvDTE.IVsExtensibility extensibility);

		Uri TeamProjectCollectionUri { get; }

		string TeamProjectName { get; set; }

		string CurrentSourceControlFolder { get; }

		void SetSourceControlExplorerDirty(string serverPath);

		void RefreshSourceControlExplorer();
		void ViewHistory();
	}
}
