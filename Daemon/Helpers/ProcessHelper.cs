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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        public static List<Process> GetChildProcessListByParentProcess(Int32 parentProcessId) {
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
        /// 获取指定进程的所有子进程
        /// </summary>
        /// <param name="parentProcess"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        public static List<Process> GetChildProcessListByParentProcess(Process parentProcess) {
            if(parentProcess==null){return null;}
            List<Int32> childProcessIdList=WindowsManagementHelper.GetChildProcessIdListByParentProcessId(parentProcess.Id);
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
        /// 结束指定进程的所有子进程
        /// </summary>
        /// <param name="parentProcess"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        public static void KillChildProcessByParentProcess(Process parentProcess) {
            List<Process> childProcessList=ProcessHelper.GetChildProcessListByParentProcess(parentProcess);
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

        /// <summary>
        /// 结束指定进程Id的所有子进程
        /// </summary>
        /// <param name="parentProcessId"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        public static void KillChildProcessByParentProcess(Int32 parentProcessId) {
            List<Process> childProcessList=ProcessHelper.GetChildProcessListByParentProcess(parentProcessId);
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
