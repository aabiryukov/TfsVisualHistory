using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Sitronics.TfsVisualHistory.VSExtension.Utility;

namespace Sitronics.TfsVisualHistory.VSExtension
{
    /// <summary>
    /// TFS Version Control live changes reader
    /// </summary>
    internal class VersionControlLogReader : TextReader
    {
        private readonly TfsTeamProjectCollection m_tpc;
        private VersionControlServer m_vcs;
        private readonly string m_serverPath;
        private readonly Queue<string> m_latestChanges = new Queue<string>();
        private int m_latestChangesetId;
        private readonly ChangesetConverter m_changesetConverter;

        // Time simulation
        private int m_lastReadLineTicks;
        // How mutch a wait before get next line
        private int m_waitMilliseconds;

        public VersionControlLogReader(
            Uri sourceControlUrl,
            string serverPath,
            StringFilter usersFilter, 
            StringFilter filesFilter)
        {
            m_changesetConverter = new ChangesetConverter(usersFilter, filesFilter);
            m_serverPath = serverPath;

	        m_tpc = new TfsTeamProjectCollection(sourceControlUrl);
        }

        public void Connect()
        {
            m_tpc.EnsureAuthenticated();
            m_vcs = m_tpc.GetService<VersionControlServer>();
        }

        private Changeset GetLatestChangeset(bool includeChanges = true)
        {
            var queryParams = new QueryHistoryParameters(m_serverPath, RecursionType.Full)
                {
                    MaxResults = 1,
                    IncludeChanges = includeChanges,
                    IncludeDownloadInfo = false,
                    SortAscending = false
                };

            return m_vcs.QueryHistory(queryParams).FirstOrDefault();
        }

        private int GetLatestChangesetId()
        {
            var latestChaneset = GetLatestChangeset(false);
            return latestChaneset != null ? latestChaneset.ChangesetId : 0;
        }

        private IEnumerable<Changeset> FindLatestChanges(int latestChangesetId)
        {
            // Before query a new changesets we need to check the new latest changeset to avoid exception "Changeset does not exist"
            if (GetLatestChangesetId() <= latestChangesetId)
                return null;

            var queryParams = new QueryHistoryParameters(m_serverPath, RecursionType.Full)
            {
                MaxResults = 10,
                IncludeChanges = true,
                IncludeDownloadInfo = false,
                SortAscending = true,
                VersionStart = new ChangesetVersionSpec(latestChangesetId+1),
                VersionEnd = VersionSpec.Latest
            };

            return m_vcs.QueryHistory(queryParams);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_tpc.Dispose();
            }
            base.Dispose(disposing);
        }

        public override string ReadLine()
        {
            const int ChangesetTimeMilliseconds = 3000;

            if (m_vcs == null)
                Connect();

            if (m_latestChangesetId == 0)
            {
                // First run. Searching latest changeset
                var latestChangeset = GetLatestChangeset();
                if (latestChangeset == null)
                    return null; // No any chanesets found!

                foreach (var line in m_changesetConverter.GetLogLines(latestChangeset))
                {
                    m_latestChanges.Enqueue(line);
                }

                // How mutch a wait before get next line
                m_waitMilliseconds = m_latestChanges.Count > 0 ? ChangesetTimeMilliseconds / m_latestChanges.Count : 0;

                m_latestChangesetId = latestChangeset.ChangesetId;
            }
            else
            {
               if (m_latestChanges.Count == 0)
               {
                   // Getting new changes after latest previous changeset Id
                   var foundChanges = FindLatestChanges(m_latestChangesetId);
                   if (foundChanges != null)
                   {
                       foreach (var changeset in foundChanges)
                       {
                           foreach (var line in m_changesetConverter.GetLogLines(changeset))
                           {
                               m_latestChanges.Enqueue(line);
                           }

                           m_latestChangesetId = changeset.ChangesetId;
                       }
                   }

                   // How mutch a wait before get next line
                   m_waitMilliseconds = m_latestChanges.Count > 0 ? ChangesetTimeMilliseconds / m_latestChanges.Count : 0;
               }
            }

            if (m_latestChanges.Count == 0)
                return null;

            // Simulate changes time to avoid super fast
            var sleepTime = m_waitMilliseconds - (Environment.TickCount - m_lastReadLineTicks);
            if (sleepTime > 0 && sleepTime <= m_waitMilliseconds)
            {
                System.Diagnostics.Debug.WriteLine("WAIT: " + sleepTime);
                System.Threading.Thread.Sleep(sleepTime);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("WAIT: none");
            }
            m_lastReadLineTicks = Environment.TickCount;

            return m_latestChanges.Dequeue();
        }
    }
}
