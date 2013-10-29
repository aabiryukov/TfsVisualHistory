using System;
using Sitronics.TfsVisualHistory;
using Sitronics.TfsVisualHistory.Common;

namespace Sitronics.VisualStudioSelector
{
	public enum VisualStudioVersion
	{
		Unknown,
		VS2012,
		VS2013
	}

    public static class VSSelector
    {
	    public static ITeamExplorerIntegrator CreateTeamExplorerIntegrator(VisualStudioVersion vsVersion)
	    {
		    switch (vsVersion)
		    {
				case VisualStudioVersion.VS2012:
				    return ActivatorVS2012.CreateTeamExplorerIntegrator();
				case VisualStudioVersion.VS2013:
					return ActivatorVS2013.CreateTeamExplorerIntegrator();
			}

			throw new ApplicationException("Unknown version of VisualStudioVersion");
	    }
    }
}
