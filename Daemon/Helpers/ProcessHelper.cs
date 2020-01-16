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
        public static Process[] GetChildProcessArrayByParentProcess(Int32 parentProcessId) {
            Int32[] childProcessIdArray=WindowsManagementHelper.GetChildProcessIdArrayByParentProcessId(parentProcessId);
            if(childProcessIdArray==null || childProcessIdArray.Length<1){return null;}
            Process[] childProcessArray=new Process[childProcessIdArray.Length];
            for(Int32 i1 = 0;i1<childProcessIdArray.Length;i1++) {
                try {
                    childProcessArray[i1]=Process.GetProcessById(childProcessIdArray[i1]);
                } catch {
                    childProcessArray[i1]=null;
                }
                
            }
            return childProcessArray;
        }
        /// <summary>
        /// 获取指定进程的所有子进程
        /// </summary>
        /// <param name="parentProcess"></param>
        /// <returns></returns>
        public static Process[] GetChildProcessArrayByParentProcess(Process parentProcess) {
            Int32[] childProcessIdArray=WindowsManagementHelper.GetChildProcessIdArrayByParentProcessId(parentProcess.Id);
            if(childProcessIdArray==null || childProcessIdArray.Length<1){return null;}
            Process[] childProcessArray=new Process[childProcessIdArray.Length];
            for(Int32 i1 = 0;i1<childProcessIdArray.Length;i1++) {
                try {
                    childProcessArray[i1]=Process.GetProcessById(childProcessIdArray[i1]);
                } catch {
                    childProcessArray[i1]=null;
                }
                
            }
            return childProcessArray;
        }

        /// <summary>
        /// 结束指定进程的所有子进程
        /// </summary>
        /// <param name="parentProcess"></param>
        public static void KillChildProcessByParentProcess(Process parentProcess) {
            Process[] childProcessArray=ProcessHelper.GetChildProcessArrayByParentProcess(parentProcess);
            if(childProcessArray==null || childProcessArray.Length<1){return;}
            for(Int32 i1 = 0;i1<childProcessArray.Length;i1++) {
                if(childProcessArray[i1]==null){continue;}
                try {
                    childProcessArray[i1].Kill();
                } catch {
                    continue;
                }
            }
        }
        /// <summary>
        /// 结束指定进程Id的所有子进程
        /// </summary>
        /// <param name="parentProcessId"></param>
        public static void KillChildProcessByParentProcess(Int32 parentProcessId) {
            Process[] childProcessArray=ProcessHelper.GetChildProcessArrayByParentProcess(parentProcessId);
            if(childProcessArray==null || childProcessArray.Length<1){return;}
            for(Int32 i1 = 0;i1<childProcessArray.Length;i1++) {
                if(childProcessArray[i1]==null){continue;}
                try {
                    childProcessArray[i1].Kill();
                } catch {
                    //忽略结束子进程时的异常
                    continue;
                }
            }
        }
    }
}
