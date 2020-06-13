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
        public static List<Int32> GetChildProcessIdListByParentProcessId(Int32 parentProcessId){
            ManagementObjectSearcher managementObjectSearcher=new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ParentProcessID={parentProcessId}");
            ManagementObjectCollection managementObjectCollection=managementObjectSearcher.Get();
            managementObjectSearcher.Dispose();
            if(managementObjectCollection==null || managementObjectCollection.Count<1){return null;}
            List<Int32> idList=new List<Int32>();
            foreach(ManagementBaseObject item in managementObjectCollection) {
                if(!Int32.TryParse(item.GetPropertyValue("ProcessID").ToString(),out Int32 processId)){continue;}
                idList.Add(processId);
            }
            managementObjectCollection.Dispose();
            return idList;
        }


        /// <summary>
        /// 获取主机内存字节数
        /// </summary>
        /// <returns></returns>
        public static UInt64 GetPhysicalMemorySize(){
            using ManagementClass managementClass = new ManagementClass("Win32_PhysicalMemory");
            if(managementClass==null){return 0;}
            using ManagementObjectCollection managementObjectCollection = managementClass.GetInstances();
            if(managementObjectCollection==null || managementObjectCollection.Count<1){return 0;}
            UInt64 size = 0;
            foreach(ManagementObject item in managementObjectCollection) {
                try {
                    size+=(UInt64)item.GetPropertyValue("Capacity");
                } catch {
                    Helpers.LoggerModuleHelper.TryLog("Helpers.WindowsManagementHelper.GetPhysicalMemorySize","Capacity属性不能转为UInt64");
                }
            }
            return size;
        }
    }
}
