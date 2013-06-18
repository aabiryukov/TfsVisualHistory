using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Sitronics.TfsVisualHistory.VSExtension.Utility;
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

            var includeUsersWildcard = new Wildcard(settings.IncludeUsers, RegexOptions.IgnoreCase);
            var excludeUsersWildcard = new Wildcard(settings.ExcludeUsers, RegexOptions.IgnoreCase);

            var includeFilesWildcard = new Wildcard(settings.IncludeFiles, RegexOptions.IgnoreCase);
            var excludeFilesWildcard = new Wildcard(settings.ExcludeFiles, RegexOptions.IgnoreCase);

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

                    if (!FilterByUser(changeset, includeUsersWildcard, excludeUsersWildcard))
						continue;

					foreach (var change in changeset.Changes)
					{
//                        if (change.Item.ItemType == ItemType.File)
                        {
                            if (!FilterByFileType(change.Item.ServerItem, includeFilesWildcard, excludeFilesWildcard))
                                continue;
                        }

						var changeTypeCode = GetChangeType(change.ChangeType);
						if (changeTypeCode == null)
							continue;

						double unixTime = (int)(changeset.CreationDate - new DateTime(1970, 1, 1)).TotalSeconds;

						var userName = FormatCommitterName(changeset.Committer);
                        var fileName = FormatFileName(change.Item.ServerItem);

						writer.WriteLine("{0}|{1}|{2}|{3}",
							unixTime,
							userName,
							changeTypeCode,
                            fileName);
					}
				}
			}

            return true;
		}

        private static string FormatFileName(string fileName)
        {
            // Преобразуем расширение к нижнему регистру, чтобы размер букв не влиял на визуализацию типов файлов.
            return ConvertFileExtensionToLowerCase(fileName);
        }


        private static string ConvertFileExtensionToLowerCase(string fileName)
        {
            var slashIndex = Math.Max(fileName.LastIndexOf('/'), fileName.LastIndexOf('\\'));
            var pointIndex = fileName.LastIndexOf('.');
            if (pointIndex < slashIndex || pointIndex < 1 || pointIndex >= fileName.Length - 1)
                return fileName;

            return fileName.Substring(0, pointIndex + 1) + fileName.Substring(pointIndex + 1, fileName.Length - (pointIndex + 1)).ToLowerInvariant();
        }

        private static bool FilterByFileType(string fileName, Wildcard includeWildcard, Wildcard excludeWildcard)
		{
            if (includeWildcard.IsEmpty || !includeWildcard.IsMatch(fileName))
                return false;

            if (!excludeWildcard.IsEmpty && excludeWildcard.IsMatch(fileName))
                return false;

            return true;
		}

        private static bool FilterByUser(Changeset changeset, Wildcard includeWildcard, Wildcard excludeWildcard)
		{
            if (includeWildcard.IsEmpty || !includeWildcard.IsMatch(changeset.Owner))
                return false;

            if (!excludeWildcard.IsEmpty && excludeWildcard.IsMatch(changeset.Owner))
                return false;

			return true;
		}

		private static string GetChangeType(ChangeType changeType)
		{
			if ((changeType & ChangeType.Edit) == ChangeType.Edit)
				return "M";

			if ((changeType & ChangeType.Rename) == ChangeType.Rename)
				return "M";

			if ((changeType & ChangeType.Add) == ChangeType.Add)
				return "A";

			if ((changeType & ChangeType.Merge) == ChangeType.Merge)
				return "A";

			if ((changeType & ChangeType.Delete) == ChangeType.Delete)
				return "D";

			return null;
		}

		private static string FormatCommitterName(string initialName)
		{
            return initialName;
/*
			int ind = initialName.LastIndexOf('\\');
			if (ind == -1)
				return initialName;

			return initialName.Substring(ind + 1);
 */ 
		}
	}
}
