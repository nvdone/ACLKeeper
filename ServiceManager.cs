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
//along with this program.If not, see<https://www.gnu.org/licenses/>

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ACLKeeper
{
	internal static class ServiceManager
	{
		public static bool ServiceInstall(string serviceName, ServiceManagerNative.SERVICE_START dwStartType, string path)
		{
			IntPtr scManager;
			IntPtr scService;

			scManager = ServiceManagerNative.OpenSCManager(IntPtr.Zero, IntPtr.Zero, ServiceManagerNative.SCM_ACCESS.SC_MANAGER_ALL_ACCESS);
			if (scManager.Equals(IntPtr.Zero))
				return false;

			scService = ServiceManagerNative.CreateService(
				scManager,
				serviceName,
				serviceName,
				ServiceManagerNative.SERVICE_ACCESS.SERVICE_ALL_ACCESS,
				(uint)ServiceManagerNative.SERVICE_TYPE.SERVICE_WIN32_OWN_PROCESS,
				(uint) dwStartType,
				(uint) ServiceManagerNative.SERVICE_ERROR.SERVICE_ERROR_NORMAL,
				path,
				IntPtr.Zero,
				IntPtr.Zero,
				IntPtr.Zero,
				IntPtr.Zero,
				IntPtr.Zero);

			ServiceManagerNative.CloseServiceHandle(scService);
			ServiceManagerNative.CloseServiceHandle(scManager);

			return true;
		}
		
		unsafe public static bool ServiceUninstall(string serviceName)
		{
			IntPtr scManager;
			IntPtr scService;

			scManager = ServiceManagerNative.OpenSCManager(IntPtr.Zero, IntPtr.Zero, ServiceManagerNative.SCM_ACCESS.SC_MANAGER_ALL_ACCESS);
			if (scManager.Equals(IntPtr.Zero))
				return false;

			scService = ServiceManagerNative.OpenService(scManager, serviceName, ServiceManagerNative.SERVICE_ACCESS.SERVICE_ALL_ACCESS);
			if (scService.Equals(IntPtr.Zero))
			{
				ServiceManagerNative.CloseServiceHandle(scManager);
				return false;
			}

			IntPtr buf = Marshal.AllocHGlobal(sizeof(ServiceManagerNative.SERVICE_STATUS_PROCESS));
			int buf_len = sizeof(ServiceManagerNative.SERVICE_STATUS_PROCESS);

			for (int i = 0; i < 30; i++)
			{
				if (!ServiceManagerNative.QueryServiceStatusEx(scService, 0, buf, buf_len, out buf_len))
				{
					if (!buf.Equals(IntPtr.Zero))
						Marshal.FreeHGlobal(buf);
					ServiceManagerNative.CloseServiceHandle(scService);
					ServiceManagerNative.CloseServiceHandle(scManager);
					return false;
				}

				ServiceManagerNative.SERVICE_STATUS_PROCESS pStatus = (ServiceManagerNative.SERVICE_STATUS_PROCESS)Marshal.PtrToStructure(buf, typeof(ServiceManagerNative.SERVICE_STATUS_PROCESS));

				switch (pStatus.currentState)
				{
					case (int) ServiceManagerNative.SERVICE_STATE.SERVICE_RUNNING:
						ServiceManagerNative.SERVICE_STATUS serviceStatus = new ServiceManagerNative.SERVICE_STATUS();
						ServiceManagerNative.ControlService(scService, ServiceManagerNative.SERVICE_CONTROL.STOP, ref serviceStatus);
						break;

					case (int)ServiceManagerNative.SERVICE_STATE.SERVICE_STOPPED:
						ServiceManagerNative.DeleteService(scService);
						i = 30;
						break;
					default:
						break;
				}

				Thread.Sleep(1000);
			}

			if (!buf.Equals(IntPtr.Zero))
				Marshal.FreeHGlobal(buf);

			ServiceManagerNative.CloseServiceHandle(scService);
			ServiceManagerNative.CloseServiceHandle(scManager);

			return true;
		}

		public static bool ServiceStart(string service_name)
		{
			IntPtr scManager;
			IntPtr scService;

			scManager = ServiceManagerNative.OpenSCManager(IntPtr.Zero, IntPtr.Zero, ServiceManagerNative.SCM_ACCESS.SC_MANAGER_ALL_ACCESS);
			if (scManager.Equals(IntPtr.Zero))
				return false;

			scService = ServiceManagerNative.OpenService(scManager, service_name, ServiceManagerNative.SERVICE_ACCESS.SERVICE_ALL_ACCESS);
			if (scService.Equals(IntPtr.Zero))
			{
				ServiceManagerNative.CloseServiceHandle(scManager);
				return false;
			}

			ServiceManagerNative.StartService(scService, 0, IntPtr.Zero);

			ServiceManagerNative.CloseServiceHandle(scService);
			ServiceManagerNative.CloseServiceHandle(scManager);

			return true;
		}
		
		public static bool ServiceStop(string service_name)
		{
			IntPtr scManager;
			IntPtr scService;
			ServiceManagerNative.SERVICE_STATUS serviceStatus = new ServiceManagerNative.SERVICE_STATUS();

			scManager = ServiceManagerNative.OpenSCManager(IntPtr.Zero, IntPtr.Zero, ServiceManagerNative.SCM_ACCESS.SC_MANAGER_ALL_ACCESS);
			if (scManager.Equals(IntPtr.Zero))
				return false;

			scService = ServiceManagerNative.OpenService(scManager, service_name, ServiceManagerNative.SERVICE_ACCESS.SERVICE_ALL_ACCESS);
			if (scService.Equals(IntPtr.Zero))
			{
				ServiceManagerNative.CloseServiceHandle(scManager);
				return false;
			}

			ServiceManagerNative.ControlService(scService, ServiceManagerNative.SERVICE_CONTROL.STOP, ref serviceStatus);

			ServiceManagerNative.CloseServiceHandle(scService);
			ServiceManagerNative.CloseServiceHandle(scManager);

			return true;
		}
	}
}
