using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Sitronics.TfsVisualHistory.Utility;

namespace Sitronics.TfsVisualHistory
{
	/// <summary>
	/// TFS Version Control live changes reader
	/// </summary>
	internal class VersionControlLogReader : TextReader
	{
		private const int MaxChangesetsToGet = 10;

		private readonly TfsTeamProjectCollection _tfsTeamProjectCollection;
		private readonly VersionControlServer _versionControlServer;
		private readonly string _sourceControlPath;
		private readonly bool _readAllHistory;
		private readonly Queue<string> _logLinesQueue;
		private readonly ChangesetConverter _changesetConverter;
		private readonly int _latestChangesetIdAtStartup;

		private int _previousChangesetId;
		// Time simulation
		private int _lastReadLineTicks;

		// How mutch a wait before get next line
		private int _waitMilliseconds;

		private bool IsFirstRun
		{
			get { return _previousChangesetId == 0; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VersionControlLogReader"/> class.
		/// </summary>
		public VersionControlLogReader(
			Uri sourceControlUrl,
			string sourceControlPath,
			StringFilter usersFilter,
			StringFilter filesFilter,
			bool readAllHistory)
		{
			_changesetConverter = new ChangesetConverter(usersFilter, filesFilter);
			_sourceControlPath = sourceControlPath;
			_readAllHistory = readAllHistory;

			_tfsTeamProjectCollection = new TfsTeamProjectCollection(sourceControlUrl);
			_tfsTeamProjectCollection.EnsureAuthenticated();
			_versionControlServer = _tfsTeamProjectCollection.GetService<VersionControlServer>();

			_logLinesQueue = new Queue<string>();
			_latestChangesetIdAtStartup = GetLatestChangesetId();
		}

		public override string ReadLine()
		{
			if (IsFirstRun)
			{
				Changeset changeset;

				if (_readAllHistory)
				{
					changeset = GetFirstChangeset(true);
				}
				else
				{
					changeset = GetLatestChangeset(true);
				}

				Enqueue(changeset);
			}
			else if (IsQueueEmpty)
			{
				var changesets = GetChangesetsAfter(_previousChangesetId);

				Enqueue(changesets);
			}

			if (IsQueueEmpty)
				return null;

			// Simulate changes time to avoid super fast when in real time mode
			if (_latestChangesetIdAtStartup < _previousChangesetId)
			{
				Sleep();
			}

			return _logLinesQueue.Dequeue();
		}

		private void Sleep()
		{
			var sleepTime = _waitMilliseconds - (Environment.TickCount - _lastReadLineTicks);
			if (0 < sleepTime && sleepTime <= _waitMilliseconds)
			{
				Thread.Sleep(sleepTime);
			}

			_lastReadLineTicks = Environment.TickCount;
		}

		private bool IsQueueEmpty
		{
			get { return _logLinesQueue.Count == 0; }
		}

		private void Enqueue(Changeset latestChangeset)
		{
			if (latestChangeset == null)
				return;

			foreach (var logLine in _changesetConverter.GetLogLines(latestChangeset))
			{
				_logLinesQueue.Enqueue(logLine);
			}

			_previousChangesetId = latestChangeset.ChangesetId;

			// How mutch a wait before get next line
			const int changesetTimeMilliseconds = 3000;

			_waitMilliseconds = _logLinesQueue.Count > 0 ? changesetTimeMilliseconds / _logLinesQueue.Count : 0;
		}

		private void Enqueue(IEnumerable<Changeset> changesets)
		{
			if (changesets == null)
				return;

			foreach (var changeset in changesets)
			{
				Enqueue(changeset);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_tfsTeamProjectCollection.Dispose();
			}

			base.Dispose(disposing);
		}

		private IEnumerable<Changeset> GetChangesetsAfter(int changesetId)
		{
			// Before query a new changesets we need to check the new latest changeset to avoid exception "Changeset does not exist"
			if (GetLatestChangesetId() <= changesetId)
				return null;

			var queryParams = new QueryHistoryParameters(_sourceControlPath, RecursionType.Full)
			{
				MaxResults = MaxChangesetsToGet,
				IncludeChanges = true,
				IncludeDownloadInfo = false,
				SortAscending = true,
				VersionStart = new ChangesetVersionSpec(changesetId + 1),
				VersionEnd = VersionSpec.Latest
			};

			return _versionControlServer.QueryHistory(queryParams);
		}

		private int GetLatestChangesetId()
		{
			var latestChangeset = GetLatestChangeset(false);
			return latestChangeset != null ? latestChangeset.ChangesetId : 0;
		}

		private Changeset GetLatestChangeset(bool includeChanges)
		{
			var queryParams = new QueryHistoryParameters(_sourceControlPath, RecursionType.Full)
			{
				MaxResults = 1,
				IncludeChanges = includeChanges,
				IncludeDownloadInfo = false,
				SortAscending = false
			};

			return _versionControlServer.QueryHistory(queryParams).FirstOrDefault();
		}

		private Changeset GetFirstChangeset(bool includeChanges)
		{
			var queryParams = new QueryHistoryParameters(_sourceControlPath, RecursionType.Full)
			{
				MaxResults = 1,
				IncludeChanges = includeChanges,
				IncludeDownloadInfo = false,
				SortAscending = true
			};

			return _versionControlServer.QueryHistory(queryParams).FirstOrDefault();
		}
	}
}
