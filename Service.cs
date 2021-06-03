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
using System.Collections.Concurrent;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Linq;

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

		private readonly ConcurrentQueue<string> checkQueue = new ConcurrentQueue<string>();
		private readonly ConcurrentQueue<string> fixQueue = new ConcurrentQueue<string>();

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
			while (true)
			{
				try
				{
					if (!checkQueue.IsEmpty)
					{
						while (checkQueue.TryDequeue(out string path))
						{
							log.Add(Log.LOGLEVEL.DEBUG, "Checking " + path);

							if (Directory.Exists(path) || File.Exists(path))
							{
								log.Add(Log.LOGLEVEL.DEBUG, "Path exists " + path);

								CatalogueItem rootItem = catalogue.GetRootItem(path);

								if (rootItem != null)
								{
									log.Add(Log.LOGLEVEL.DEBUG, "Found root for " + path);

									checkACL(rootItem, path);
								}
								else
								{
									log.Add(Log.LOGLEVEL.ERROR, "No root for " + path);
								}
							}
							else
							{
								log.Add(Log.LOGLEVEL.DEBUG, "Path does not exist " + path);
							}

							if (stopEvent.WaitOne(0))
								return;
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

		private void fixLoop()
		{
			while (true)
			{
				try
				{
					if (!fixQueue.IsEmpty)
					{
						while (fixQueue.TryDequeue(out string path))
						{
							log.Add(Log.LOGLEVEL.DEBUG, "Fixing " + path);

							if (Directory.Exists(path))
							{
								try
								{
									DirectoryInfo di = new DirectoryInfo(path);
									DirectorySecurity ds = di.GetAccessControl(AccessControlSections.Access);
									AuthorizationRuleCollection arc = ds.GetAccessRules(true, true, typeof(NTAccount));

									foreach (FileSystemAccessRule fsar in arc)
									{
										ds.RemoveAccessRule(fsar);
									}
									ds.SetAccessRuleProtection(false, true);
									di.SetAccessControl(ds);

									log.Add(Log.LOGLEVEL.INFO, "Fixed " + path);
								}
								catch
								{
									log.Add(Log.LOGLEVEL.INFO, "Failed to fix " + path);

									if (!fixQueue.Contains(path))
										fixQueue.Enqueue(path);
								}
							}
							else if (File.Exists(path))
							{
								try
								{
									FileInfo fi = new FileInfo(path);
									FileSecurity fs = fi.GetAccessControl(AccessControlSections.Access);
									AuthorizationRuleCollection arc = fs.GetAccessRules(true, true, typeof(NTAccount));

									foreach (FileSystemAccessRule fsar in arc)
									{
										fs.RemoveAccessRule(fsar);
									}
									fs.SetAccessRuleProtection(false, true);
									fi.SetAccessControl(fs);

									log.Add(Log.LOGLEVEL.INFO, "Fixed " + path);
								}
								catch
								{
									log.Add(Log.LOGLEVEL.INFO, "Failed to fix " + path);

									if (!fixQueue.Contains(path))
										fixQueue.Enqueue(path);
								}
							}

							if (stopEvent.WaitOne(0))
								return;
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
			if (!checkQueue.Contains(e.FullPath))
			{
				checkQueue.Enqueue(e.FullPath);
				log.Add(Log.LOGLEVEL.DEBUG, "Enqueued since created " + e.FullPath);
			}
		}

		private void renamed(object sender, RenamedEventArgs e)
		{
			if (!checkQueue.Contains(e.FullPath))
			{
				checkQueue.Enqueue(e.FullPath);
				log.Add(Log.LOGLEVEL.DEBUG, "Enqueued since renamed " + e.FullPath);
			}
		}

		void checkACL(CatalogueItem rootItem, string path)
		{
			ACL pathACL = new ACL();

			if(!pathACL.Load(path))
			{
				log.Add(Log.LOGLEVEL.DEBUG, "Failed to get path ACL for " + path);
				if (!checkQueue.Contains(path))
					checkQueue.Enqueue(path);
				return;
			}

			if (!rootItem.ACLEquals(pathACL))
			{
				if (!rootItem.IsPath(path))
				{
					if (!fixQueue.Contains(path))
					{
						fixQueue.Enqueue(path);
						log.Add(Log.LOGLEVEL.DEBUG, "Enqueued for fix " + path);
					}
				}
				else
				{
					log.Add(Log.LOGLEVEL.ERROR, "Aborted root path fix for " + path);
				}
			}
			else
			{
				log.Add(Log.LOGLEVEL.DEBUG, "No need to fix " + path);
			}

			if(Directory.Exists(path))
			{
				try
				{
					foreach (string subPath in Directory.GetFiles(path))
					{
						if (!checkQueue.Contains(subPath))
						{
							checkQueue.Enqueue(subPath);
							log.Add(Log.LOGLEVEL.DEBUG, "Enqueued file for check " + subPath);
						}
					}
				}
				catch { }

				try
				{
					foreach (string subPath in Directory.GetDirectories(path))
					{
						if (!checkQueue.Contains(subPath))
						{
							checkQueue.Enqueue(subPath);
							log.Add(Log.LOGLEVEL.DEBUG, "Enqueued dir for check " + subPath);
						}
					}
				}
				catch { }
			}
		}
	}
}
