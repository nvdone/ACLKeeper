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

using System;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Windows.Forms;

namespace ACLKeeper
{
	static class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			string exePath = Assembly.GetExecutingAssembly().Location;
			string dbPath = Path.Combine(Path.GetDirectoryName(exePath), Path.GetFileNameWithoutExtension(exePath) + ".db");
			string exeName = Path.GetFileNameWithoutExtension(exePath);

			if (!File.Exists(dbPath))
			{
				if (!Helper.CreateDB(dbPath, exeName))
				{
					return;
				}
			}

			if (args.Length == 0)
			{
				new ConsoleForm(exePath, dbPath, exeName).ShowDialog();
			}

			foreach (string arg in args)
			{
				switch (arg.ToLowerInvariant())
				{
					case "-?":
					case "/?":
						MessageBox.Show(Assembly.GetExecutingAssembly().GetName().Name + "\r\n" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\r\n" + ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute))).Copyright, Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;

					case "-run":
					case "/run":
						Service service = new Service(dbPath, exeName);

						ServiceBase[] ServicesToRun;
						ServicesToRun = new ServiceBase[]
						{
							service
						};
						ServiceBase.Run(ServicesToRun);
						break;

					case "-install":
					case "/install":
						ServiceManager.ServiceInstall(exeName, ServiceManagerNative.SERVICE_START.SERVICE_AUTO_START, exePath + " -run");
						break;

					case "-uninstall":
					case "/uninstall":
						ServiceManager.ServiceUninstall(exeName);
						break;

					case "-start":
					case "/start":
						ServiceManager.ServiceStart(exeName);
						break;

					case "-stop":
					case "/stop":
						ServiceManager.ServiceStop(exeName);
						break;
				}
			}
		}
	}
}
