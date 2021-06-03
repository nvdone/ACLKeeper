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

namespace ACLKeeper
{
	internal class Log
	{
		readonly SQLite db = null;
		readonly long loglevel = 0;

		public enum LOGLEVEL : int
		{
			ALL = 0,
			DEBUG = 1,
			INFO = 2,
			ERROR = 3,
			EXCEPTION = 4
		}

		public Log(SQLite db, long loglevel)
		{
			this.db = db;
			this.loglevel = loglevel;
		}

		public void Add(LOGLEVEL loglevel, string message)
		{
			if (this.loglevel <= (int)loglevel)
			{
				lock (db)
				{
					try
					{
						db.Execute("INSERT INTO logs (date, type, message) VALUES (@0, @1, @2);", DateTime.Now, (int)loglevel, message.Substring(0, Math.Min(message.Length, 4095)));
					}
					catch
					{ }
				}
			}
		}
	}
}
