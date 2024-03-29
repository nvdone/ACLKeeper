﻿//NVD ACLKeeper
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
//along with this program.If not, see<https://www.gnu.org/licenses/>

using System;
using System.Runtime.InteropServices;

namespace ACLKeeper
{
	internal static class ServiceManagerNative
	{
		#region Enums

		[Flags]
		public enum SCM_ACCESS : uint
		{
			SC_MANAGER_CONNECT = 0x00001,
			SC_MANAGER_CREATE_SERVICE = 0x00002,
			SC_MANAGER_ENUMERATE_SERVICE = 0x00004,
			SC_MANAGER_LOCK = 0x00008,
			SC_MANAGER_QUERY_LOCK_STATUS = 0x00010,
			SC_MANAGER_MODIFY_BOOT_CONFIG = 0x00020,
			SC_MANAGER_ALL_ACCESS = ACCESS_MASK.STANDARD_RIGHTS_REQUIRED |
				SC_MANAGER_CONNECT |
				SC_MANAGER_CREATE_SERVICE |
				SC_MANAGER_ENUMERATE_SERVICE |
				SC_MANAGER_LOCK |
				SC_MANAGER_QUERY_LOCK_STATUS |
				SC_MANAGER_MODIFY_BOOT_CONFIG,
			GENERIC_READ = ACCESS_MASK.STANDARD_RIGHTS_READ |
				SC_MANAGER_ENUMERATE_SERVICE |
				SC_MANAGER_QUERY_LOCK_STATUS,
			GENERIC_WRITE = ACCESS_MASK.STANDARD_RIGHTS_WRITE |
				SC_MANAGER_CREATE_SERVICE |
				SC_MANAGER_MODIFY_BOOT_CONFIG,
			GENERIC_EXECUTE = ACCESS_MASK.STANDARD_RIGHTS_EXECUTE |
				SC_MANAGER_CONNECT | SC_MANAGER_LOCK,
			GENERIC_ALL = SC_MANAGER_ALL_ACCESS,
		}

		[Flags]
		public enum SERVICE_ACCESS : uint
		{
			SERVICE_QUERY_CONFIG = 0x00001,
			SERVICE_CHANGE_CONFIG = 0x00002,
			SERVICE_QUERY_STATUS = 0x00004,
			SERVICE_ENUMERATE_DEPENDENTS = 0x00008,
			SERVICE_START = 0x00010,
			SERVICE_STOP = 0x00020,
			SERVICE_PAUSE_CONTINUE = 0x00040,
			SERVICE_INTERROGATE = 0x00080,
			SERVICE_USER_DEFINED_CONTROL = 0x00100,
			SERVICE_ALL_ACCESS = (ACCESS_MASK.STANDARD_RIGHTS_REQUIRED |
				SERVICE_QUERY_CONFIG |
				SERVICE_CHANGE_CONFIG |
				SERVICE_QUERY_STATUS |
				SERVICE_ENUMERATE_DEPENDENTS |
				SERVICE_START |
				SERVICE_STOP |
				SERVICE_PAUSE_CONTINUE |
				SERVICE_INTERROGATE |
				SERVICE_USER_DEFINED_CONTROL),
			GENERIC_READ = ACCESS_MASK.STANDARD_RIGHTS_READ |
				SERVICE_QUERY_CONFIG |
				SERVICE_QUERY_STATUS |
				SERVICE_INTERROGATE |
				SERVICE_ENUMERATE_DEPENDENTS,
			GENERIC_WRITE = ACCESS_MASK.STANDARD_RIGHTS_WRITE |
				SERVICE_CHANGE_CONFIG,
			GENERIC_EXECUTE = ACCESS_MASK.STANDARD_RIGHTS_EXECUTE |
				SERVICE_START |
				SERVICE_STOP |
				SERVICE_PAUSE_CONTINUE |
				SERVICE_USER_DEFINED_CONTROL,
			ACCESS_SYSTEM_SECURITY = ACCESS_MASK.ACCESS_SYSTEM_SECURITY,
			DELETE = ACCESS_MASK.DELETE,
			READ_CONTROL = ACCESS_MASK.READ_CONTROL,
			WRITE_DAC = ACCESS_MASK.WRITE_DAC,
			WRITE_OWNER = ACCESS_MASK.WRITE_OWNER,
		}

		public enum SERVICE_CONTROL : uint
		{
			STOP = 0x00000001,
			PAUSE = 0x00000002,
			CONTINUE = 0x00000003,
			INTERROGATE = 0x00000004,
			SHUTDOWN = 0x00000005,
			PARAMCHANGE = 0x00000006,
			NETBINDADD = 0x00000007,
			NETBINDREMOVE = 0x00000008,
			NETBINDENABLE = 0x00000009,
			NETBINDDISABLE = 0x0000000A,
			DEVICEEVENT = 0x0000000B,
			HARDWAREPROFILECHANGE = 0x0000000C,
			POWEREVENT = 0x0000000D,
			SESSIONCHANGE = 0x0000000E
		}

		public enum SERVICE_STATE : uint
		{
			SERVICE_STOPPED = 0x00000001,
			SERVICE_START_PENDING = 0x00000002,
			SERVICE_STOP_PENDING = 0x00000003,
			SERVICE_RUNNING = 0x00000004,
			SERVICE_CONTINUE_PENDING = 0x00000005,
			SERVICE_PAUSE_PENDING = 0x00000006,
			SERVICE_PAUSED = 0x00000007
		}

		[Flags]
		public enum SERVICE_TYPES : int
		{
			SERVICE_KERNEL_DRIVER = 0x00000001,
			SERVICE_FILE_SYSTEM_DRIVER = 0x00000002,
			SERVICE_WIN32_OWN_PROCESS = 0x00000010,
			SERVICE_WIN32_SHARE_PROCESS = 0x00000020,
			SERVICE_INTERACTIVE_PROCESS = 0x00000100
		}

		[Flags]
		public enum SERVICE_TYPE : uint
		{
			SERVICE_KERNEL_DRIVER = 0x00000001,
			SERVICE_FILE_SYSTEM_DRIVER = 0x00000002,
			SERVICE_WIN32_OWN_PROCESS = 0x00000010,
			SERVICE_WIN32_SHARE_PROCESS = 0x00000020,
			SERVICE_INTERACTIVE_PROCESS = 0x00000100,
		}

		public enum SERVICE_START : uint
		{
			SERVICE_BOOT_START = 0x00000000,
			SERVICE_SYSTEM_START = 0x00000001,
			SERVICE_AUTO_START = 0x00000002,
			SERVICE_DEMAND_START = 0x00000003,
			SERVICE_DISABLED = 0x00000004,
		}

		public enum SERVICE_ERROR
		{
			SERVICE_ERROR_IGNORE = 0x00000000,
			SERVICE_ERROR_NORMAL = 0x00000001,
			SERVICE_ERROR_SEVERE = 0x00000002,
			SERVICE_ERROR_CRITICAL = 0x00000003,
		}

		[Flags]
		private enum ACCESS_MASK : uint
		{
			DELETE = 0x00010000,
			READ_CONTROL = 0x00020000,
			WRITE_DAC = 0x00040000,
			WRITE_OWNER = 0x00080000,
			SYNCHRONIZE = 0x00100000,
			STANDARD_RIGHTS_REQUIRED = 0x000F0000,
			STANDARD_RIGHTS_READ = 0x00020000,
			STANDARD_RIGHTS_WRITE = 0x00020000,
			STANDARD_RIGHTS_EXECUTE = 0x00020000,
			STANDARD_RIGHTS_ALL = 0x001F0000,
			SPECIFIC_RIGHTS_ALL = 0x0000FFFF,
			ACCESS_SYSTEM_SECURITY = 0x01000000,
			MAXIMUM_ALLOWED = 0x02000000,
			GENERIC_READ = 0x80000000,
			GENERIC_WRITE = 0x40000000,
			GENERIC_EXECUTE = 0x20000000,
			GENERIC_ALL = 0x10000000,
			DESKTOP_READOBJECTS = 0x00000001,
			DESKTOP_CREATEWINDOW = 0x00000002,
			DESKTOP_CREATEMENU = 0x00000004,
			DESKTOP_HOOKCONTROL = 0x00000008,
			DESKTOP_JOURNALRECORD = 0x00000010,
			DESKTOP_JOURNALPLAYBACK = 0x00000020,
			DESKTOP_ENUMERATE = 0x00000040,
			DESKTOP_WRITEOBJECTS = 0x00000080,
			DESKTOP_SWITCHDESKTOP = 0x00000100,
			WINSTA_ENUMDESKTOPS = 0x00000001,
			WINSTA_READATTRIBUTES = 0x00000002,
			WINSTA_ACCESSCLIPBOARD = 0x00000004,
			WINSTA_CREATEDESKTOP = 0x00000008,
			WINSTA_WRITEATTRIBUTES = 0x00000010,
			WINSTA_ACCESSGLOBALATOMS = 0x00000020,
			WINSTA_EXITWINDOWS = 0x00000040,
			WINSTA_ENUMERATE = 0x00000100,
			WINSTA_READSCREEN = 0x00000200,
			WINSTA_ALL_ACCESS = 0x0000037F
		}

		#endregion

		#region Structs

		[StructLayout(LayoutKind.Sequential, Pack = 0)]
		public struct SERVICE_STATUS
		{
			public SERVICE_TYPES dwServiceType;
			public SERVICE_STATE dwCurrentState;
			public uint dwControlsAccepted;
			public uint dwWin32ExitCode;
			public uint dwServiceSpecificExitCode;
			public uint dwCheckPoint;
			public uint dwWaitHint;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct SERVICE_STATUS_PROCESS
		{
			public int serviceType;
			public int currentState;
			public int controlsAccepted;
			public int win32ExitCode;
			public int serviceSpecificExitCode;
			public int checkPoint;
			public int waitHint;
			public int processID;
			public int serviceFlags;

			public static explicit operator SERVICE_STATUS_PROCESS(IntPtr v)
			{
				throw new NotImplementedException(v.ToString());
			}
		}

		#endregion

		#region Functions

		[DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr OpenSCManager(string machineName, string databaseName, ServiceManagerNative.SCM_ACCESS dwAccess);

		[DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr OpenSCManager(IntPtr machineName, IntPtr databaseName, ServiceManagerNative.SCM_ACCESS dwAccess);

		[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, ServiceManagerNative.SERVICE_ACCESS dwDesiredAccess, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, string lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword);

		[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, ServiceManagerNative.SERVICE_ACCESS dwDesiredAccess, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, IntPtr lpLoadOrderGroup, IntPtr lpdwTagId, IntPtr lpDependencies, IntPtr lpServiceStartName, IntPtr lpPassword);

		[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, ServiceManagerNative.SERVICE_ACCESS dwDesiredAccess);

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool QueryServiceStatusEx(IntPtr serviceHandle, int infoLevel, IntPtr buffer, int bufferSize, out int bytesNeeded);

		[DllImport("advapi32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool StartService(IntPtr hService, int dwNumServiceArgs, IntPtr lpServiceArgVectors);

		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ControlService(IntPtr hService, SERVICE_CONTROL dwControl, ref SERVICE_STATUS lpServiceStatus);

		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteService(IntPtr hService);

		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseServiceHandle(IntPtr hSCObject);

		#endregion
	}
}
