using wind.Entities.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using wind.Helpers;

namespace wind.Modules {
    /// <summary>本地(命名管道)管理模块</summary>
    public class PipelineControlModule2 {

        /// <summary>
        /// 处理消息 0x02
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static Byte[] OnMessageStart(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStart","开始处理 start 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessageStart[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStart[Error]","解析 unitKey 失败");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Unit unit=Program.UnitManageModule.GetUnit(unitKey);
            if(unit==null) {
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unit is not found")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.StartUnit(unitKey,false);
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStart","已处理 start 指令");
            return new Byte[8]{0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x03
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static Byte[] OnMessageStop(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStop","开始处理 stop 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessageStop[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStop[Error]","解析 unitKey 失败");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Unit unit=Program.UnitManageModule.GetUnit(unitKey);
            if(unit==null) {
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unit is not found")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.StopUnit(unitKey);
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStop","已处理 stop 指令");
            return new Byte[8]{0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x04
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static Byte[] OnMessageRestart(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRestart","开始处理 restart 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessageRestart[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRestart[Error]","解析 unitKey 失败");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Unit unit=Program.UnitManageModule.GetUnit(unitKey);
            if(unit==null) {
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unit is not found")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.RestartUnit(unitKey);
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRestart","已处理 restart 指令");
            return new Byte[8]{0x04,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x05
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static Byte[] OnMessageLoad(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoad","开始处理 load 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessageLoad[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoad[Error]","解析 unitKey 失败");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Boolean b1=Program.UnitManageModule.LoadUnit(unitKey);
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoad","已处理 load 指令");
            return new Byte[8]{0x05,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes($"unit load {(b1?"success":"fail")}")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x06
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static Byte[] OnMessageRemove(Byte[] bytes){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemove","开始处理 remove 指令");
            //解析 unitKey
            String unitKey;
            try {
                unitKey=Encoding.UTF8.GetString(bytes,8,104-8).TrimEnd().TrimEnd(new Char[]{'\0'});
            }catch(Exception exception){
                Helpers.LoggerModuleHelper.TryLog(
                    "Modules.PipelineControlModule.OnMessageRemove[Error]",
                    $"解析 unitKey 异常\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            if(String.IsNullOrWhiteSpace(unitKey)){
                Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemove[Error]","解析 unitKey 失败");
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("invalid unitKey")).ToArray();
            }
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            if(Program.UnitManageModule.GetUnit(unitKey)==null) {
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unit is not found")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.RemoveUnit(unitKey);
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemove","已处理 remove 指令");
            return new Byte[8]{0x06,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }

        /// <summary>
        /// 处理消息 0x12
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnMessageStartAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStartAll","开始处理 start-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.StartAllUnits(false);
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStartAll","已处理 start-all 指令");
            return new Byte[8]{0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x13
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnMessageStopAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStopAll","开始处理 stop-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.StopAllUnits();
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageStopAll","已处理 stop 指令");
            return new Byte[8]{0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x14
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnMessageRestartAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRestartAll","开始处理 restart-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.RestartAllUnits();
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRestartAll","已处理 restart-all 指令");
            return new Byte[8]{0x04,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x15
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnMessageLoadAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoadAll","开始处理 load-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Int32 count=Program.UnitManageModule.LoadAllUnits();
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageLoadAll","已处理 load-all 指令");
            return new Byte[8]{0x05,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes($"loaded {count} unit")).ToArray();
        }
        /// <summary>
        /// 处理消息 0x16
        /// </summary>
        /// <returns></returns>
        private static Byte[] OnMessageRemoveAll(){
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemoveAll","开始处理 remove-all 指令");
            //检查模块
            if(!Program.UnitManageModule.Useable){
                return new Byte[8]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("unable respond command")).ToArray();
            }
            Task.Run(()=>{
                Program.UnitManageModule.RemoveAllUnits();
            });
            //拼接返回数据
            Helpers.LoggerModuleHelper.TryLog("Modules.PipelineControlModule.OnMessageRemoveAll","已处理 remove-all 指令");
            return new Byte[8]{0x06,0x00,0x00,0x00,0x00,0x00,0x00,0x00}.Concat(Encoding.UTF8.GetBytes("command executed in background")).ToArray();
        }
    }
}
