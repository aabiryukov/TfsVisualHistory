using Sitronics.TfsVisualHistory.Common;

namespace Sitronics.TfsVisualHistory
{
	public static class ActivatorVS2019
	{
		public static ITeamExplorerIntegrator CreateTeamExplorerIntegrator()
		{
			return new TeamExplorerIntegrator();
		}
	}
}
