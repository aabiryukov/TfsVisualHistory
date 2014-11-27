using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.VersionControl.Client;
using System.Linq;

namespace Sitronics.TfsVisualHistory
{
	internal static class TfsLogWriter
	{
		public enum GourceLogResult
		{
			Success,
			NoItemsOnHistoryStep,
			NoItemsOnFilterStep
		}

		public static bool CreateGourceLogFile(
	        string outputFile,
			string outputAvatarsDir,
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

		        HashSet<string> commiters = null;

		        if (!string.IsNullOrEmpty(outputAvatarsDir))
		        {
			        commiters = new HashSet<string>();
		        }

	            var result = CreateGourceLogFile(
	                outputFile,
					commiters,
	                vcs,
	                serverPath,
	                settings,
	                ref cancel,
	                progressReporter);

		        if (result && commiters != null)
		        {
					var identityService = tpc.GetService<IIdentityManagementService2>();
					DownloadAvatars(identityService, outputAvatarsDir, commiters);
		        }

		        return result;
	        }
	    }


//		private static readonly HashSet<string> st_commiterImageCache = new HashSet<string>();

		private static void DownloadAvatars(IIdentityManagementService2 identityService, string outputAvatarsDir, IEnumerable<string> commiters)
		{
			var invalidFileChars = new []{'\\', ',', '/', '<', '>', '?', '|', ':', '*'};

			var searchCommiters1 = new string[1];
			foreach (var commiter in commiters)
			{
				if(string.IsNullOrEmpty(commiter) || commiter.IndexOfAny(invalidFileChars) >= 0)
					continue;

				var imagePath = Path.Combine(outputAvatarsDir, commiter + ".png");
				if (File.Exists(imagePath))
				{
					if ((DateTime.Now - File.GetLastWriteTime(imagePath)).TotalHours < 24)
						continue;

					// File is expired
					File.Delete(imagePath);
				}

				searchCommiters1[0] = commiter;
				var identities = identityService.ReadIdentities(IdentitySearchFactor.DisplayName,
					searchCommiters1, MembershipQuery.Expanded, ReadIdentityOptions.ExtendedProperties);

				if (identities == null || !identities.Any()) 
					continue;

				foreach (var identity in identities[0])
				{
					object imageData;
					if (!identity.TryGetProperty("Microsoft.TeamFoundation.Identity.Image.Data", out imageData))
						continue;

					var imageBytes = imageData as byte[];
					if (imageBytes == null)
						continue;

					//			var imageFormat = GetImageFormat(imageBytes);
					//			Trace.WriteLine(imageFormat);
					File.WriteAllBytes(imagePath, imageBytes);
					
					break;
				}
			}
		}

		private static bool CreateGourceLogFile(
			string outputFile,
			HashSet<string> outputCommiters,
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

	        var changesetConverter = new ChangesetConverter(settings.UsersFilter, settings.FilesFilter);

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

				var hasLines = false;

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

	                var usefulChangeset = false;
					foreach (var line in changesetConverter.GetLogLines(changeset))
					{
						usefulChangeset = true;
                        writer.WriteLine(line);
                    }

	                if (usefulChangeset)
	                {
		                hasLines = true;
		                if (outputCommiters != null)
			                outputCommiters.Add(changeset.OwnerDisplayName);
	                }
                }

				return hasLines;
			}
		}
	}
}
