using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace wind.Helpers {
    public static class UnitPerformanceCounterModuleHelper {
        /// <summary>
        /// 通过 processId 获取实际的 instanceName
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static String GetActuallyInstanceNameByProcessId(Int32 processId){
            PerformanceCounterCategory performanceCounterCategory;
            try {
                performanceCounterCategory=new PerformanceCounterCategory{CategoryName="Process"};
            }catch(Exception exception) {
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.UnitPerformanceCounterModule.GetActuallyInstanceNameByProcessId[Error]",
                    $"创建性能计数器分类异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return null;
            }
            String[] instanceNameArray;
            try {
                instanceNameArray=performanceCounterCategory.GetInstanceNames();
            }catch(Exception exception) {
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.UnitPerformanceCounterModule.GetActuallyInstanceNameByProcessId[Error]",
                    $"获取性能计数器分类下的实例名称异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return null;
            }
            String instanceName=null;
            for(Int32 i1 = 0;i1<instanceNameArray.GetLength(0);i1++) {
                PerformanceCounter performanceCounter=null;
                try {
                    performanceCounter=new PerformanceCounter{CategoryName="Process",CounterName="ID Process",InstanceName=instanceNameArray[i1],ReadOnly=true};
                    if((Int32)performanceCounter.RawValue!=processId){continue;}
                } catch {
                    continue;
                } finally {
                    performanceCounter?.Dispose();
                }
                instanceName=instanceNameArray[i1];
                break;
            }
            return instanceName;
        }
    }
}
