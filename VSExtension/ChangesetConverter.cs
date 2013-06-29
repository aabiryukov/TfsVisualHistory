using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.TeamFoundation.VersionControl.Client;
using Sitronics.TfsVisualHistory.VSExtension.Utility;

namespace Sitronics.TfsVisualHistory.VSExtension
{
    /// <summary>
    /// Convert TFS changesets to gource format.
    /// </summary>
    internal class ChangesetConverter
    {
		private static DateTime st_unixTimeZero = new DateTime(1970, 1, 1);
		
		private readonly Wildcard m_includeUsersWildcard;
        private readonly Wildcard m_excludeUsersWildcard;
        private readonly Wildcard m_includeFilesWildcard;
        private readonly Wildcard m_excludeFilesWildcard;

        public ChangesetConverter(VisualizationSettings settings)
        {
            m_includeUsersWildcard = new Wildcard(settings.IncludeUsers, RegexOptions.IgnoreCase);
            m_excludeUsersWildcard = new Wildcard(settings.ExcludeUsers, RegexOptions.IgnoreCase);

            m_includeFilesWildcard = new Wildcard(settings.IncludeFiles, RegexOptions.IgnoreCase);
            m_excludeFilesWildcard = new Wildcard(settings.ExcludeFiles, RegexOptions.IgnoreCase);
        }

        private static bool FilterByString(string text, Wildcard include, Wildcard exclude)
        {
            if (include.IsEmpty || !include.IsMatch(text))
                return false;

            if (!exclude.IsEmpty && exclude.IsMatch(text))
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

        public IEnumerable<string> GetLogLines(Changeset changeset)
        {
            // Filter changeset Owner
            if (!FilterByString(changeset.Owner ?? "", m_includeUsersWildcard, m_excludeUsersWildcard))
                yield break;

            foreach (var change in changeset.Changes)
            {

                //                        if (change.Item.ItemType == ItemType.File)
                {
                    // Filter files
                    if (!FilterByString(change.Item.ServerItem, m_includeFilesWildcard, m_excludeFilesWildcard))
                        continue;
                }

                var changeTypeCode = GetChangeType(change.ChangeType);
                if (changeTypeCode == null)
                    continue;

				double unixTime = (int)(changeset.CreationDate - st_unixTimeZero.ToLocalTime()).TotalSeconds;

                var userName = FormatCommitterName(changeset.Committer);
                var fileName = FormatFileName(change.Item.ServerItem);

                yield return 
                    string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}|{3}",
                        unixTime,
                        userName,
                        changeTypeCode,
                        fileName);
            }
        }
    }
}
