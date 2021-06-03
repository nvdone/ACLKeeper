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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ACLKeeper
{
	internal class Catalogue
	{
		private readonly List<CatalogueItem> items = new List<CatalogueItem>();
		private readonly List<CatalogueItem> removed_items = new List<CatalogueItem>();

		public void Add(CatalogueItem item)
		{
			items.Add(item);
		}

		public void Remove(CatalogueItem item)
		{
			items.Remove(item);
			removed_items.Add(item);
		}

		public bool Load(SQLite db)
		{
			items.Clear();
			removed_items.Clear();

			foreach (long itemId in db.GetList<long>("SELECT id FROM catalogue;"))
			{
				CatalogueItem item = new CatalogueItem();
				if (!item.Load(db, itemId))
					return false;
				items.Add(item);
			}

			return true;
		}

		public bool Save(SQLite db)
		{
			foreach(CatalogueItem item in removed_items)
			{
				if (!item.Remove(db))
					return false;
			}

			removed_items.Clear();

			foreach(CatalogueItem item in items)
			{
				if (!item.Save(db))
					return false;
			}

			return true;
		}

		public List<CatalogueItem> GetItemsForRefresh()
		{
			long today = 1 << (int)DateTime.Now.DayOfWeek;
			return items.Where(item => item.ProcrastinateAndConfess(today)).ToList();
		}

		public bool ItemCanBeAdded(CatalogueItem candidate)
		{
			foreach(CatalogueItem item in items)
			{
				if ((item != candidate) && item.PathsIntersect(candidate))
					return false;
			}

			return true;
		}

		public CatalogueItem GetRootItem(string path)
		{
			foreach (CatalogueItem item in items)
			{
				if (item.IsRootForPath(path))
					return item;
			}

			foreach (CatalogueItem item in items)
			{
				if (item.IsPath(path))
					return item;
			}

			return null;
		}

		public bool UpdateACLs()
		{
			foreach (CatalogueItem item in items)
			{
				if (!item.UpdateACL())
					return false;
			}

			return true;
		}

		public void EnableMonitoring(Log log, FileSystemEventHandler created, RenamedEventHandler renamed)
		{
			foreach(CatalogueItem item in items)
			{
				if (item.SubjectForMonitoring)
				{
					if (item.EnableMonitoring(created, renamed))
						log.Add(Log.LOGLEVEL.DEBUG, "Enabled monitoring for " + item.DebugId);
					else
						log.Add(Log.LOGLEVEL.ERROR, "Failed to enable monitoring for " + item.DebugId);
				}
			}
		}

		public void DisableMonitoring(Log log)
		{
			foreach (CatalogueItem item in items)
			{
				if (item.SubjectForMonitoring)
				{
					item.DisableMonitoring();
					log.Add(Log.LOGLEVEL.DEBUG, "Disabled monitoring for " + item.DebugId);
				}
			}
		}

		public void Decompose(Action<CatalogueItem, string, bool, long, long> decomposer) => items.ForEach(item => item.Decompose(decomposer));
	}
}
