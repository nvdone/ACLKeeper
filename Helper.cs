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

namespace ACLKeeper
{
	internal static class Helper
	{
		public static bool CreateDB(string path, string exeName)
		{
			try
			{
				SQLite.CreateDatabase(path);
				SQLite db = new SQLite("Data Source = \"" + path + "\"");
				db.Execute("CREATE TABLE catalogue (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
					"path NVARCHAR(32767), " +
					"monitoring INTEGER, " + 
					"refresh_time INTEGER, " +
					"refresh_dow INTEGER)"
					);

				db.Execute("CREATE TABLE settings (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name NVARCHAR(256), value NVARCHAR(1024));");
				db.Execute("INSERT INTO settings (name, value) VALUES ('loop', '5');");
				db.Execute("INSERT INTO settings (name, value) VALUES ('loglevel', '4');");
				db.Execute("INSERT INTO settings (name, value) VALUES ('bypassacl', '0');");

				db.Execute("CREATE TABLE logs (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, date DATETIME, type integer, message NVARCHAR(4096));");
				return true;
			}
			catch (Exception ex)
			{
				System.Diagnostics.EventLog.WriteEntry(exeName, "Failed to create a database: " + ex.Message);
				return false;
			}
		}

		public static (int h, int m) SplitMinutes(long minutes)
		{
			return ((int)Math.Floor(minutes / 60.0), (int)(minutes % 60));
		}

		public static string DowToString(long dow)
		{
			List<string> strs = new List<string>();

			if ((dow & 2) != 0) strs.Add("Mon");
			if ((dow & 4) != 0) strs.Add("Tue");
			if ((dow & 8) != 0) strs.Add("Wed");
			if ((dow & 16) != 0) strs.Add("Thu");
			if ((dow & 32) != 0) strs.Add("Fri");
			if ((dow & 64) != 0) strs.Add("Sat");
			if ((dow & 1) != 0) strs.Add("Sun");

			return String.Join(", ", strs);
		}

	}
}
