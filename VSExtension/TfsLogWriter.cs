using System;
using System.IO;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System.Linq;

namespace Sitronics.TfsVisualHistory.VSExtension
{
	internal static class TfsLogWriter
	{
	    public static bool CreateGourceLogFile(
	        string outputFile,
	        Uri sourceControlUrl,
	        string serverPath,
	        VisualizationSettings settings,
	        ref bool cancel,
	        Action<int> progressReporter)
	    {
            //			var credentials = new UICredentialsProvider();

	        using (var tpc = new TfsTeamProjectCollection(sourceControlUrl))
	        {
	            tpc.EnsureAuthenticated();
	            if (cancel) return false;

	            var vcs = tpc.GetService<VersionControlServer>();

	            return CreateGourceLogFile(
	                outputFile,
	                vcs,
	                serverPath,
	                settings,
	                ref cancel,
	                progressReporter);
	        }
	    }

	    private static bool CreateGourceLogFile(
			string outputFile,
            VersionControlServer vcs,
			string serverPath,
			VisualizationSettings settings,
            ref bool cancel,
			Action<int> progressReporter)
		{
//			int latestChangesetId = vcs.GetLatestChangesetId();
            if (cancel) return false;

            var versionFrom = new DateVersionSpec(settings.DateFrom);
            var versionTo = new DateVersionSpec(settings.DateTo);

            int latestChangesetId;
            // Getting latest changeset ID for current search criteria
		    {
                var latestChanges = vcs.QueryHistory(
                    serverPath,
                    VersionSpec.Latest,
                    0,
                    RecursionType.Full,
                    null, //any user
                    versionFrom, // from first changeset
                    versionTo, // to last changeset
                    1,
                    false, // with changes
                    false,
                    false,
                    false); // sorted

		        var latestChangeset = latestChanges.Cast<Changeset>().FirstOrDefault();
                if (latestChangeset == null)
                {
                    // History not found
                    return false;
                }
                latestChangesetId = latestChangeset.ChangesetId;
                if (cancel) return false;
            }

		    var firstChangesetId = 0;

	        var changesetConverter = new ChangesetConverter(settings);

			using (var writer = new StreamWriter(outputFile))
			{
				var csList = vcs.QueryHistory(
					serverPath,
					VersionSpec.Latest,
					0,
					RecursionType.Full,
					null, //any user
                    versionFrom, // from first changeset
                    versionTo, // to last changeset
					int.MaxValue,
					true, // with changes
					false,
					false,
					true); // sorted

                foreach (var changeset in csList.Cast<Changeset>())
                {
                    if (cancel) return false;

                    if (firstChangesetId == 0) firstChangesetId = changeset.ChangesetId;

                    if (progressReporter != null)
                    {
                        var progressValue = changeset.ChangesetId - firstChangesetId;
                        var progressTotal = latestChangesetId - firstChangesetId;

                        progressReporter(progressTotal > 0 ? progressValue * 100 / progressTotal : 100);
                    }

                    foreach (var line in changesetConverter.GetLogLines(changeset))
                    {
                        writer.WriteLine(line);
                    }
				}
			}

            return true;
		}
	}
}
