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
using System.Security.AccessControl;

namespace ACLKeeper
{
	internal class AccessRights : IEquatable<AccessRights>
	{
		private FileSystemRights allow;
		private FileSystemRights deny;

		public AccessRights(FileSystemRights allow, FileSystemRights deny)
		{
			this.allow = allow;
			this.deny = deny;
		}

		public void Add(FileSystemRights allow, FileSystemRights deny)
		{
			this.allow |= allow;
			this.deny |= deny;
		}

		public bool Equals(AccessRights other)
		{
			return (allow & FileSystemRights.FullControl) == (other.allow & FileSystemRights.FullControl) && (deny & FileSystemRights.FullControl) == (other.deny & FileSystemRights.FullControl);
		}
	}
}
