//NVD ACLKeeper
//Copyright © 2021, Nikolay Dudkin

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//GNU General Public License for more details.
//You should have received a copy of the GNU General Public License
//along with this program.If not, see<https://www.gnu.org/licenses/>.

using NVD.SQL;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;

namespace ACLKeeper
{
	internal class CatalogueItem
	{
		private long id;
		private string path;
		private bool monitoring;
		private long refresh_time;
		private long refresh_dow;

		private string path_slashed;
		private DateTime next_refresh;
		private ACL acl = null;
		private FileSystemWatcher watcher = null;

		public string DebugId
		{
			get => path;
		}

		public string DebugNextRefresh
		{
			get => next_refresh.ToString("yyyy-MM-dd HH:mm:ss");
		}

		public bool SubjectForMonitoring
		{
			get => monitoring;
		}

		public void Update(string path, bool monitoring, long refresh_time, long refresh_dow)
		{
			this.path = path.TrimEnd('\\');
			this.path_slashed = path + "\\";
			this.monitoring = monitoring;
			this.refresh_time = refresh_time;
			this.refresh_dow = refresh_dow;

			procrastinate();
		}

		public bool Load(SQLite db, long id)
		{
			try
			{
				this.id = id;
				var tuple = db.GetTuple<string, long, long, long>("SELECT path, monitoring, refresh_time, refresh_dow FROM catalogue WHERE id = @0;", id);

				path = tuple.Item1.TrimEnd('\\');
				path_slashed = path + "\\";

				monitoring = tuple.Item2 != 0;
				refresh_time = tuple.Item3;
				refresh_dow = tuple.Item4;

				procrastinate();
			}
			catch
			{
				return false;
			}

			return true;
		}

		public bool Save(SQLite db)
		{
			try
			{
				if (id > 0 && db.Get<long>("SELECT id FROM catalogue WHERE id = @0;", id) > 0)
				{
					db.Execute("UPDATE catalogue SET path = @1, monitoring = @2, refresh_time = @3, refresh_dow = @4 WHERE id = @0;", id, path, monitoring ? 1 : 0, refresh_time, refresh_dow);
				}
				else
				{
					id = db.ExecuteGetIdentity("INSERT into catalogue (path, monitoring, refresh_time, refresh_dow) VALUES (@0, @1, @2, @3);", path, monitoring ? 1 : 0, refresh_time, refresh_dow);
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		public bool Remove(SQLite db)
		{
			try
			{
				db.Execute("DELETE FROM catalogue WHERE id = @0;", id);
			}
			catch
			{
				return false;
			}

			return true;
		}

		public bool ACLEquals(ACL other)
		{
			//return Volatile.Read<ACL>(ref acl).Equals(other); //In case you are concerned about cache coherency on your target platform
			return acl.Equals(other);
		}

		public bool UpdateACL()
		{
			ACL newacl = new ACL();
			if (!newacl.Load(path))
				return false;

			//Volatile.Write<ACL>(ref acl, newacl); //In case you are concerned about cache coherency on your target platform
			acl = newacl;

			return true;
		}

		public bool EnableMonitoring(FileSystemEventHandler created, RenamedEventHandler renamed)
		{
			try
			{
				watcher = new FileSystemWatcher(path);
				watcher.IncludeSubdirectories = true;
				watcher.Created += created;
				watcher.Renamed += renamed;
				watcher.EnableRaisingEvents = true;

				return true;
			}
			catch
			{
				return false;
			}
		}

		public void DisableMonitoring()
		{
			if (watcher != null)
			{
				watcher.EnableRaisingEvents = false;
				watcher.Dispose();
				watcher = null;
			}
		}

		public bool ProcrastinateAndConfess(long today)
		{
			if (refresh_dow == 0 || next_refresh > DateTime.Now)
				return false;

			procrastinate();

			return (refresh_dow & today) != 0;
		}

		private void procrastinate()
		{
			if (refresh_dow == 0)
				return;

			var (h, m) = Helper.SplitMinutes(refresh_time);

			next_refresh = DateTime.Now.Date.AddHours(h).AddMinutes(m);

			if (next_refresh <= DateTime.Now)
				next_refresh = next_refresh.AddDays(1);
		}

		public bool Enqueue(ConcurrentQueue<PathItem> queue)
		{
			PathItem pathItem = new PathItem(path, this);
			if (queue.Contains(pathItem))
				return false;

			queue.Enqueue(pathItem);

			return true;
		}

		public bool IsRootForPath(string candidate) => candidate.StartsWith(path_slashed, true, System.Globalization.CultureInfo.InvariantCulture);

		public bool IsPath(string candidate) => path.Equals(candidate, StringComparison.InvariantCultureIgnoreCase);

		public bool PathsIntersect(CatalogueItem other) => IsRootForPath(other.path_slashed) || other.IsRootForPath(path_slashed);

		public void Decompose(Action<CatalogueItem, string, bool, long, long> decomposer) => decomposer(this, path, monitoring, refresh_time, refresh_dow);
	}
}
