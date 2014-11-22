using Sitronics.TfsVisualHistory.Common;

namespace Sitronics.TfsVisualHistory
{
	public static class ActivatorVS2015
	{
		public static ITeamExplorerIntegrator CreateTeamExplorerIntegrator()
		{
			return new TeamExplorerIntegrator();
		}
	}
}
