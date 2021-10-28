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
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ACLKeeper
{
	internal class ACL : IEquatable<ACL>
	{
		private readonly Dictionary<IdentityReference, AccessRights> rules = new Dictionary<IdentityReference, AccessRights>();

		public bool Load(string path)
		{
			try
			{
				AuthorizationRuleCollection arc = new DirectorySecurity(path, AccessControlSections.Access).GetAccessRules(true, true, typeof(NTAccount));
				foreach (FileSystemAccessRule fsar in arc)
				{
					addRule(fsar.IdentityReference, fsar.AccessControlType, fsar.FileSystemRights);
				}
			}
			catch
			{
				return false;
			}

			return true;
		}

		private void addRule(IdentityReference ir, AccessControlType act, FileSystemRights fsr)
		{
			if (!rules.ContainsKey(ir))
			{
				AccessRights ar;

				if (act == AccessControlType.Allow)
					ar = new AccessRights(fsr, 0);
				else
					ar = new AccessRights(0, fsr);

				rules.Add(ir, ar);
			}
			else
			{
				AccessRights ar = rules[ir];

				if (act == AccessControlType.Allow)
					ar.Add(fsr, 0);
				else
					ar.Add(0, fsr);
			}
		}

		public AccessRights this[IdentityReference ir]
		{
			get
			{
				if (!rules.ContainsKey(ir))
					return null;

				return
					rules[ir];
			}
		}

		public bool Equals(ACL other)
		{
			Dictionary<IdentityReference, AccessRights> mySubstantialRules = rules.Where(i => i.Value.Substantial).ToDictionary(j => j.Key, j => j.Value);
			Dictionary<IdentityReference, AccessRights> otherSubstantialRules = other.rules.Where(k => k.Value.Substantial).ToDictionary(l => l.Key, l => l.Value);

			if (mySubstantialRules.Count() != otherSubstantialRules.Count())
				return false;

			foreach (IdentityReference ir in mySubstantialRules.Keys)
			{
				if (!otherSubstantialRules.ContainsKey(ir))
					return false;
				if (!mySubstantialRules[ir].Equals(other.rules[ir]))
					return false;
			}

			return true;
		}
	}
}
