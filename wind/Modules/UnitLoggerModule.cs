using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using wind.Helpers;

namespace wind.Modules {
    /// <summary>单元日志模块</summary>
    public class UnitLoggerModule:IDisposable {
        public Boolean Useable{get;private set;}=false;

        /// <summary>日志目录</summary>
        private String UnitLogsDirectory{get;set;}=null;
        /// <summary>定时器</summary>
        private Timer Timer{get;set;}=null;
        /// <summary>是否启用定时器</summary>
        private Boolean TimerEnable{get;set;}=false;
        /// <summary>日志</summary>
        private ConcurrentDictionary<String,StringBuilder> UnitLogs{get;set;}=new ConcurrentDictionary<String, StringBuilder>();
        /// <summary>单元输出</summary>
        private ConcurrentDictionary<String,Queue<String>> UnitOutputs{get;set;}=new ConcurrentDictionary<String,Queue<String>>();
        /// <summary>输出保留长度</summary>
        private Int32 MaxUnitOutputLine{get;}=4096;
        
        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            //写完所有日志
            //this.TimerCallback(null);

            if (!disposedValue) {
                if (disposing) {
                    // TODO: 释放托管状态(托管对象)。
                    this.Timer?.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                this.UnitLogs=null;
                this.UnitOutputs=null;

                disposedValue=true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~UnitLoggerModule() {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose() {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <param name="unitLogsDirectory">日志目录</param>
        /// <param name="writePeriod">写入间隔</param>
        public Boolean Setup(String unitLogsDirectory,Int32 writePeriod) {
            if(this.Useable){return true;}
            //检查日志目录
            if(!Directory.Exists(unitLogsDirectory)){
                try{
                    _=Directory.CreateDirectory(unitLogsDirectory);
                }catch{
                    return false;
                }
            }
            this.UnitLogsDirectory=unitLogsDirectory;
            //初始化定时器
            try {
                this.Timer=new Timer(this.TimerCallback,null,0,writePeriod<1000?1000:writePeriod);
            } catch {
                return false;
            }
            this.TimerEnable=true;
            //完成
            this.Useable=true;
            return true;
        }


        /// <summary>
        /// 定时写入日志
        /// </summary>
        /// <param name="state"></param>
        private void TimerCallback(object state) {
            if(!this.Useable || !this.TimerEnable || this.UnitLogs.Count<1){return;}
            this.TimerEnable=false;
            String filename=String.Concat(DateTime.Now.ToString("yyyy-MM-dd",DateTimeFormatInfo.InvariantInfo),".log");
            foreach (KeyValuePair<String,StringBuilder> log in this.UnitLogs) {
                if(log.Value==null || log.Value.Length<1){continue;}
                String logFileDirectory=String.Concat(this.UnitLogsDirectory,Path.DirectorySeparatorChar,log.Key);
                if(!Directory.Exists(logFileDirectory)) {
                    try {
                        _=Directory.CreateDirectory(logFileDirectory);
                    }catch{
                        //异常直接跳过
                        continue;
                    }
                }
                String logFilePath=String.Concat(logFileDirectory,Path.DirectorySeparatorChar,filename);
                try{
                    FileStream fs=File.Open(logFilePath,FileMode.Append,FileAccess.Write,FileShare.Read);
                    Byte[] bytes=Encoding.UTF8.GetBytes(log.Value.ToString());
                    fs.Write(bytes,0,bytes.GetLength(0));
                    fs.Dispose();
                    //清空引用的值
                    log.Value.Clear();
                }catch(Exception exception){
                    Console.WriteLine($"Modules.UnitLoggerModule.TimerCallback[Error] => 异常信息:{exception.Message} | 异常栈:{exception.StackTrace}");
                    //异常直接跳过
                    continue;
                }
            }
            this.TimerEnable=true;
        }

        /// <summary>
        /// 记录
        /// </summary>
        /// <param name="unitKey"></param>
        /// <param name="text"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style","IDE0056:使用索引运算符",Justification = "<挂起>")]
        public void Log(String unitKey,String text){
            if(!this.Useable){return;}
            if(String.IsNullOrWhiteSpace(unitKey) || String.IsNullOrWhiteSpace(text)){return;}
            if(!this.UnitLogs.ContainsKey(unitKey) || this.UnitLogs[unitKey]==null){ this.UnitLogs[unitKey]=new StringBuilder(); }
            this.UnitLogs[unitKey].Append(text).AppendLine();//收到的消息会丢失换行
        }

        /// <summary>
        /// 记录输出
        /// </summary>
        /// <param name="unitKey"></param>
        /// <param name="text"></param>
        public void LogOutput(String unitKey,String text) {
            if(!this.Useable){return;}
            if(String.IsNullOrWhiteSpace(unitKey) || String.IsNullOrWhiteSpace(text)){return;}
            if(!this.UnitOutputs.ContainsKey(unitKey) || this.UnitOutputs[unitKey]==null){ this.UnitOutputs[unitKey]=new Queue<String>(); }
            if(this.UnitOutputs[unitKey].Count>this.MaxUnitOutputLine){ _=this.UnitOutputs[unitKey].Dequeue(); }
            this.UnitOutputs[unitKey].Enqueue(text);
        }

        /// <summary>
        /// 获取单元的日志文件路径
        /// </summary>
        /// <param name="unitKey"></param>
        /// <returns></returns>
        public String GetLogFilePath(String unitKey) {
            if(!this.Useable || String.IsNullOrWhiteSpace(unitKey)){return null;}
            String logDirectory=String.Concat(this.UnitLogsDirectory,Path.DirectorySeparatorChar,unitKey);
            if(!Directory.Exists(logDirectory)){return null;}
            String filename=String.Concat(DateTime.Now.ToString("yyyy-MM-dd",DateTimeFormatInfo.InvariantInfo),".log");
            return String.Concat(logDirectory,Path.DirectorySeparatorChar,filename);
        }

        /// <summary>
        /// 获取输出
        /// </summary>
        /// <param name="unitKey"></param>
        /// <returns></returns>
        public String[] GetOutputs(String unitKey) {
            if(!this.Useable || String.IsNullOrWhiteSpace(unitKey)){return null;}
            if(!this.UnitOutputs.ContainsKey(unitKey) || this.UnitOutputs[unitKey]==null){return null;}
            return this.UnitOutputs[unitKey].ToArray();
        }

        /*
        /// <summary>
        /// 读取日志的最后 N 行
        /// </summary>
        /// <param name="unitKey"></param>
        /// <param name="lineSize"></param>
        /// <returns></returns>
        public Boolean GetLogLastLines(String unitKey,Int32 lineSize,out String logFilePath,out String[] logLines){
            logFilePath=null;
            logLines=null;
            if(!this.Useable || String.IsNullOrWhiteSpace(unitKey)){return false;}
            String logDirectory=String.Concat(this.UnitLogsDirectory,Path.DirectorySeparatorChar,unitKey);
            if(!Directory.Exists(logDirectory)){return false;}
            String filename=String.Concat(DateTime.Now.ToString("yyyy-MM-dd",DateTimeFormatInfo.InvariantInfo),".log");
            logFilePath=String.Concat(logDirectory,Path.DirectorySeparatorChar,filename);
            if(!File.Exists(logFilePath)){return false;}
            String[] lines;
            try {
                lines=File.ReadLines(logFilePath,Encoding.UTF8).ToArray();
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog(
                    "Modules.UnitLoggerModule.GetLogLastLines[Error]",
                    $"打开日志文件异常,:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return false;
            }
            if(lines==null || lines.GetLength(0)<1){return false;}
            if(lines.GetLength(0)<=lineSize){
                logLines=lines;
            } else {
                logLines=lines.AsSpan(lines.GetLength(0)-lineSize).ToArray();
            }
            return true;
        }*/
    }
}
