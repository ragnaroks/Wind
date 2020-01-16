using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace Daemon.Helpers {
    public static class WindowsManagementHelper {
        /// <summary>
        /// 获取指定进程Id的所有子进程Id
        /// </summary>
        /// <param name="parentProcessId"></param>
        /// <returns></returns>
        public static Int32[] GetChildProcessIdArrayByParentProcessId(Int32 parentProcessId){
            ManagementObjectSearcher managementObjectSearcher=new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ParentProcessID={parentProcessId}");
            ManagementObjectCollection managementObjectCollection=managementObjectSearcher.Get();
            managementObjectSearcher.Dispose();
            if(managementObjectCollection==null || managementObjectCollection.Count<1){return null;}
            Int32[] idArray=new Int32[managementObjectCollection.Count];
            Int32 index=0;
            foreach(ManagementBaseObject item in managementObjectCollection) {
                Int32.TryParse(item.GetPropertyValue("ProcessID").ToString(),out idArray[index]);
            }
            managementObjectCollection.Dispose();
            return idArray;
        }

        /// <summary>
        /// 获取主机内存字节数
        /// </summary>
        /// <returns></returns>
        public static UInt64 GetPhysicalMemorySize() {
            ManagementClass managementClass=new ManagementClass("Win32_PhysicalMemory");
            if(managementClass==null){return 0;}
            ManagementObjectCollection managementObjectCollection=managementClass.GetInstances();
            managementClass.Dispose();
            if(managementObjectCollection==null || managementObjectCollection.Count<1){return 0;}
            UInt64 size=0;
            foreach(ManagementObject item in managementObjectCollection) {
                try {
                    size+=(UInt64)item.GetPropertyValue("Capacity");
                }catch(Exception exception) {
                    Console.WriteLine($"Helpers.WindowsManagementHelper.GetPhysicalMemorySize => Capacity属性不能转为UInt64,{exception.Message},{exception.StackTrace}");
                }
            }
            managementObjectCollection.Dispose();
            return size;
        }
    }
}
