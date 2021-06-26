using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ACLKeeper
{
	internal class PathItem : IEquatable<PathItem>
	{
		public enum CheckResult : int
		{
			NA = 0,
			PATHMISSING = 1,
			NOROOT = 2,
			FAILEDTOGETACL = 3,
			NONEEDTOFIX = 4,
			NEEDTOFIX = 5
		}

		public enum FixResult : int
		{
			NA = 0,
			PATHMISSING = 1,
			ISROOT = 2,
			FIXED = 3,
			FAILEDTOFIX = 4
		}

		private readonly string path;
		private readonly CatalogueItem rootItem = null;
		
		public string Path { get { return path; } }
		public CatalogueItem RootItem { get { return rootItem; } }
		public bool IsDirectory { get; private set; }

		public PathItem(string path, CatalogueItem rootItem)
		{
			this.path = path;
			this.rootItem = rootItem;

			IsDirectory = Directory.Exists(path);
		}

		public PathItem(string path, Catalogue catalogue)
		{
			this.path = path;
			rootItem = catalogue.GetRootItem(path);

			IsDirectory = Directory.Exists(path);
		}

		public CheckResult CheckACL()
		{
			if(rootItem == null)
				return CheckResult.NOROOT;

			if (!File.Exists(path) && !Directory.Exists(path))
				return CheckResult.PATHMISSING;

			ACL pathACL = new ACL();
			if (!pathACL.Load(path))
				return CheckResult.FAILEDTOGETACL;

			return rootItem.ACLEquals(pathACL) ? CheckResult.NONEEDTOFIX : CheckResult.NEEDTOFIX;
		}

		public FixResult FixACL()
		{
			if (rootItem.IsPath(path))
				return FixResult.ISROOT;

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

					return FixResult.FIXED;
				}
				catch
				{
					return FixResult.FAILEDTOFIX;
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

					return FixResult.FIXED;
				}
				catch
				{
					return FixResult.FAILEDTOFIX;
				}
			}
			else
			{
				return FixResult.PATHMISSING;
			}
		}

		public bool Equals(PathItem other)
		{
			return path.Equals(other.path);
		}
	}
}
