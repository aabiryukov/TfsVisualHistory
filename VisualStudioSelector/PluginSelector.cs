using System;
using Sitronics.TfsVisualHistory;
using Sitronics.TfsVisualHistory.Common;

namespace Sitronics.VisualStudioSelector
{
	public enum VisualStudioVersion
	{
		Unknown,
		VS2012,
		VS2013,
		VS2015,
        VS2017
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
				case VisualStudioVersion.VS2015:
					return ActivatorVS2015.CreateTeamExplorerIntegrator();
                case VisualStudioVersion.VS2017:
                    return ActivatorVS2017.CreateTeamExplorerIntegrator();
            }

            throw new ApplicationException("Unknown version of VisualStudioVersion");
	    }
    }
}
