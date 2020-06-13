using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Text;

namespace Daemon.Helpers {
    public static class ProcessHelper {
        /// <summary>
        /// 获取指定进程Id的所有子进程
        /// </summary>
        /// <param name="parentProcessId"></param>
        /// <returns></returns>
        public static List<Process> GetChildProcessListByParentProcessId(Int32 parentProcessId) {
            List<Int32> childProcessIdList=WindowsManagementHelper.GetChildProcessIdListByParentProcessId(parentProcessId);
            if(childProcessIdList==null || childProcessIdList.Count<1){return null;}
            List<Process> childProcessList=new List<Process>(childProcessIdList.Count);
            foreach(Int32 item in childProcessIdList) {
                try{
                    childProcessList.Add(Process.GetProcessById(item));
                } catch {
                    continue;
                }
            }
            return childProcessList;
        }

        /// <summary>
        /// 结束指定进程Id的所有子进程,在这之前记得先结束主进程
        /// </summary>
        /// <param name="parentProcessId"></param>
        public static void KillChildProcessByParentProcess(Int32 parentProcessId) {
            List<Process> childProcessList=ProcessHelper.GetChildProcessListByParentProcessId(parentProcessId);
            if(childProcessList==null || childProcessList.Count<1){return;}
            foreach(Process item in childProcessList) {
                try {
                    item.Kill();
                } catch {
                    //忽略结束子进程时的异常
                    continue;
                }
            }
        }
    }
}
