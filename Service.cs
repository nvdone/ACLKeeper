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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;

namespace ACLKeeper
{
	partial class Service : ServiceBase
	{
		long loop = 0;

		private readonly string dbPath;
		private readonly string exeName;
		
		private readonly ManualResetEvent stopEvent;
		private Thread refreshThread = null;
		private Thread checkThread = null;
		private Thread fixThread = null;

		private Log log = null;
		private Catalogue catalogue = null;

		private readonly ConcurrentQueue<PathItem> checkQueue = new ConcurrentQueue<PathItem>();
		private readonly ConcurrentQueue<PathItem> fixQueue = new ConcurrentQueue<PathItem>();

		public Service(string dbPath, string exeName)
		{
			this.dbPath = dbPath;
			this.exeName = exeName;

			stopEvent = new ManualResetEvent(false);

			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			startLoops();
		}

		protected override void OnStop()
		{
			stopLoops();
		}

		private void startLoops()
		{
			SQLite db = null;
			long loglevel = 0;
			long bypassacl = 0;

			try
			{
				db = new SQLite("Data Source = \"" + dbPath + "\"");

				long.TryParse(db.Get<string>("SELECT value FROM settings WHERE name = 'loop';"), out loop);
				long.TryParse(db.Get<string>("SELECT value FROM settings WHERE name = 'loglevel';"), out loglevel);
				long.TryParse(db.Get<string>("SELECT value FROM settings WHERE name = 'bypassacl';"), out bypassacl);

				log = new Log(db, loglevel);
			}
			catch (Exception ex)
			{
				System.Diagnostics.EventLog.WriteEntry(exeName, "Failed to read settings: " + ex.Message);
				return;
			}

			try
			{
				if (bypassacl != 0)
				{
					if (!TokenPrivileges.SetPrivilege("SeBackupPrivilege") || !TokenPrivileges.SetPrivilege("SeRestorePrivilege"))
					{
						log.Add(Log.LOGLEVEL.ERROR, "Setting privileges failed!");
					}
					else
					{
						log.Add(Log.LOGLEVEL.DEBUG, "Privileges set.");
					}
				}

				catalogue = new Catalogue();

				if(!catalogue.Load(db))
				{
					log.Add(Log.LOGLEVEL.EXCEPTION, "Failed to load catalogue!");
					return;
				}
				
				if (!catalogue.UpdateACLs())
				{
					log.Add(Log.LOGLEVEL.EXCEPTION, "UpdateACLs failed!");
					return;
				}

				catalogue.EnableMonitoring(log, created, renamed);
			}
			catch (Exception ex)
			{
				log.Add(Log.LOGLEVEL.EXCEPTION, "Initial setup failed: " + ex.Message);
				return;
			}

			refreshThread = new Thread(refreshLoop);
			refreshThread.Start();

			checkThread = new Thread(checkLoop);
			checkThread.Start();

			fixThread = new Thread(fixLoop);
			fixThread.Start();
		}

		private void stopLoops()
		{
			void dropThread(Thread thread)
			{
				if (thread == null)
					return;
				if (thread.IsAlive)
					thread.Abort();
			}

			catalogue.DisableMonitoring(log);

			stopEvent?.Set();
			Thread.Sleep(5000);

			dropThread(refreshThread);
			dropThread(checkThread);
			dropThread(fixThread);

			refreshThread = checkThread = fixThread = null;
		}

		private void refreshLoop()
		{
			while (true)
			{
				try
				{
					List<CatalogueItem> victims = catalogue.GetItemsForRefresh();

					foreach(CatalogueItem item in victims)
					{
						if (item.UpdateACL())
						{
							log.Add(Log.LOGLEVEL.DEBUG, "Next refresh for " + item.DebugId + " is " + item.DebugNextRefresh);

							if (item.Enqueue(checkQueue))
								log.Add(Log.LOGLEVEL.DEBUG, "Enqueued for recursive check " + item.DebugId);

							if (stopEvent.WaitOne(0))
								return;
						}
						else
						{
							log.Add(Log.LOGLEVEL.ERROR, "Failed to update ACL for " + item.DebugId);
						}
					}
				}
				catch (Exception ex)
				{
					log.Add(Log.LOGLEVEL.EXCEPTION, "RefreshLoop: " + ex.Message);
				}

				if (stopEvent.WaitOne(60000))
					break;
			}
		}

		private void checkLoop()
		{
			Queue<PathItem> nextTimeQueue = new Queue<PathItem>();

			while (true)
			{
				try
				{
					if (!checkQueue.IsEmpty)
					{
						while (checkQueue.TryDequeue(out PathItem pathItem))
						{
							log.Add(Log.LOGLEVEL.DEBUG, "Checking " + pathItem.Path);

							switch(pathItem.CheckACL())
							{
								case PathItem.CheckResult.PATHMISSING:
									log.Add(Log.LOGLEVEL.DEBUG, "Path does not exist " + pathItem.Path);
									break;
								
								case PathItem.CheckResult.NOROOT:
									log.Add(Log.LOGLEVEL.ERROR, "No root for " + pathItem.Path);
									break;

								case PathItem.CheckResult.FAILEDTOGETACL:
									log.Add(Log.LOGLEVEL.DEBUG, "Failed to get path ACL for " + pathItem.Path);
									if (!nextTimeQueue.Contains(pathItem))
										nextTimeQueue.Enqueue(pathItem);
									break;

								case PathItem.CheckResult.NEEDTOFIX:
									if (!fixQueue.Contains(pathItem))
									{
										fixQueue.Enqueue(pathItem);
										log.Add(Log.LOGLEVEL.DEBUG, "Enqueued for fix " + pathItem.Path);
									}
									if (pathItem.IsDirectory)
										enqueueSubItemsForCheck(pathItem);
									break;

								case PathItem.CheckResult.NONEEDTOFIX:
									log.Add(Log.LOGLEVEL.DEBUG, "No need to fix " + pathItem.Path);
									if (pathItem.IsDirectory)
										enqueueSubItemsForCheck(pathItem);
									break;
								
								default:
									log.Add(Log.LOGLEVEL.ERROR, "Unknown check result for " + pathItem.Path);
									break;
							}

							if (stopEvent.WaitOne(0))
								return;
						}

						while (nextTimeQueue.Count > 0)
						{
							PathItem dummy = nextTimeQueue.Dequeue();
							if (!checkQueue.Contains(dummy))
								checkQueue.Enqueue(dummy);
						}
					}
				}
				catch (Exception ex)
				{
					log.Add(Log.LOGLEVEL.EXCEPTION, "CheckLoop: " + ex.Message);
				}

				if (stopEvent.WaitOne((int)loop * 60000))
					break;
			}
		}

		private void enqueueSubItemsForCheck(PathItem pathItem)
		{
			try
			{
				foreach (string subPath in Directory.GetFiles(pathItem.Path))
				{
					PathItem subItem = new PathItem(subPath, pathItem.RootItem);
					if (!checkQueue.Contains(subItem))
					{
						checkQueue.Enqueue(subItem);
						log.Add(Log.LOGLEVEL.DEBUG, "Enqueued file for check " + subPath);
					}
				}
			}
			catch { }

			try
			{
				foreach (string subPath in Directory.GetDirectories(pathItem.Path))
				{
					PathItem subItem = new PathItem(subPath, pathItem.RootItem);
					if (!checkQueue.Contains(subItem))
					{
						checkQueue.Enqueue(subItem);
						log.Add(Log.LOGLEVEL.DEBUG, "Enqueued dir for check " + subPath);
					}
				}
			}
			catch { }
		}

		private void fixLoop()
		{
			Queue<PathItem> nextTimeQueue = new Queue<PathItem>();

			while (true)
			{
				try
				{
					if (!fixQueue.IsEmpty)
					{
						while (fixQueue.TryDequeue(out PathItem pathItem))
						{
							log.Add(Log.LOGLEVEL.DEBUG, "Fixing " + pathItem.Path);

							switch (pathItem.FixACL())
							{
								case PathItem.FixResult.PATHMISSING:
									log.Add(Log.LOGLEVEL.INFO, "Missing path " + pathItem.Path);
									break;

								case PathItem.FixResult.ISROOT:
									log.Add(Log.LOGLEVEL.ERROR, "Aborted fix of a root path " + pathItem.Path);
									break;

								case PathItem.FixResult.FIXED:
									log.Add(Log.LOGLEVEL.INFO, "Fixed " + pathItem.Path);
									break;

								case PathItem.FixResult.FAILEDTOFIX:
									log.Add(Log.LOGLEVEL.INFO, "Failed to fix " + pathItem.Path);
									if (!nextTimeQueue.Contains(pathItem))
										nextTimeQueue.Enqueue(pathItem);
									break;
								default:
									break;
								}

							if (stopEvent.WaitOne(0))
								return;
						}

						while(nextTimeQueue.Count > 0)
						{
							PathItem dummy = nextTimeQueue.Dequeue();
							if(!fixQueue.Contains(dummy))
								fixQueue.Enqueue(dummy);
						}
					}
				}
				catch (Exception ex)
				{
					log.Add(Log.LOGLEVEL.EXCEPTION, "FixLoop: " + ex.Message);
				}

				if (stopEvent.WaitOne((int)loop * 60000))
					break;
			}
		}

		private void created(object sender, FileSystemEventArgs e)
		{
			PathItem pathItem = new PathItem(e.FullPath, catalogue);
			if (!checkQueue.Contains(pathItem))
			{
				checkQueue.Enqueue(pathItem);
				log.Add(Log.LOGLEVEL.DEBUG, "Enqueued since created " + e.FullPath);
			}
		}

		private void renamed(object sender, RenamedEventArgs e)
		{
			PathItem pathItem = new PathItem(e.FullPath, catalogue);
			if (!checkQueue.Contains(pathItem))
			{
				checkQueue.Enqueue(pathItem);
				log.Add(Log.LOGLEVEL.DEBUG, "Enqueued since renamed " + e.FullPath);
			}
		}
	}
}
