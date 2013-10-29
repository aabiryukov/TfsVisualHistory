using Sitronics.TfsVisualHistory.Common;

namespace Sitronics.TfsVisualHistory
{
	public static class ActivatorVS2013
	{
		public static ITeamExplorerIntegrator CreateTeamExplorerIntegrator()
		{
			return new TeamExplorerIntegrator();
		}
	}
}
