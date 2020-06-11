using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Daemon.Modules {
    /// <summary>日志模块</summary>
    [Localizable(false)]
    public class LoggerModule:IDisposable {
        public Boolean Useable{get;private set;}=false;

        /// <summary>日志目录</summary>
        private String LogsDirectory{get;set;}=null;
        /// <summary>定时器</summary>
        private Timer Timer{get;set;}=null;
        /// <summary>是否启用定时器</summary>
        private Boolean TimerEnable{get;set;}=false;
        /// <summary>日志</summary>
        private Dictionary<String,StringBuilder> Logs{get;set;}=new Dictionary<String, StringBuilder>();
        
        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: 释放托管状态(托管对象)。
                    this.Timer.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                this.LogsDirectory=null;
                this.Logs=null;

                disposedValue=true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~LoggerModule() {
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
        /// <param name="logsDirectory">日志目录</param>
        /// <param name="writePeriod">写入间隔</param>
        /// <exception cref="Exception">目录不存在可能异常</exception>
        public Boolean Setup(String logsDirectory,Int32 writePeriod) {
            if(this.Useable){return true;}
            //检查日志目录
            if(!Directory.Exists(logsDirectory)){
                try{
                    _=Directory.CreateDirectory(logsDirectory);
                }catch{
                    //throw;
                    return false;
                }
            }
            this.LogsDirectory=logsDirectory;
            //初始化定时器
            try {
                this.Timer=new Timer(this.TimerCallback,null,0,writePeriod<1000?1000:writePeriod);
            } catch {
                //throw;
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
            if(!this.Useable || !this.TimerEnable || this.Logs.Count<1){return;}
            this.TimerEnable=false;
            String filename=DateTime.Now.ToString("yyyy-MM-dd",System.Globalization.DateTimeFormatInfo.InvariantInfo);
            foreach (KeyValuePair<String,StringBuilder> log in this.Logs) {
                if(log.Value==null || log.Value.Length<1){continue;}
                String keyToPath=log.Key.Replace('.',Path.DirectorySeparatorChar);
                String logFileDirectory=$"{this.LogsDirectory}{Path.DirectorySeparatorChar}{keyToPath}";
                if(!Directory.Exists(logFileDirectory)) {
                    try {
                        _=Directory.CreateDirectory(logFileDirectory);
                    } catch {
                        //异常直接跳过
                        continue;
                    }
                }
                String logFilePath=$"{this.LogsDirectory}{Path.DirectorySeparatorChar}{keyToPath}{Path.DirectorySeparatorChar}{filename}.log";
                try{
                    FileStream fs=File.Open(logFilePath,FileMode.Append,FileAccess.ReadWrite,FileShare.Read);
                    Byte[] bytes=Encoding.UTF8.GetBytes(log.Value.ToString());
                    fs.Write(bytes,0,bytes.GetLength(0));
                    fs.Dispose();
                    //清空引用的值
                    log.Value.Clear();
                }catch(Exception exception){
                    Console.Error.WriteLine($"Modules.LoggerModule.TimerCallback[Error] => 异常信息:{exception.Message} | 异常栈:{exception.StackTrace}");
                    //异常直接跳过
                    continue;
                }
            }
            this.TimerEnable=true;
        }

        /// <summary>
        /// 记录
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style","IDE0056:使用索引运算符",Justification = "<挂起>")]
        public void Log(String title,String text){
            if(String.IsNullOrWhiteSpace(title) || String.IsNullOrWhiteSpace(text)){return;}
            Regex regex=new Regex(@"^[a-zA-Z0-9\.\[\]]{1,64}$",RegexOptions.Compiled);
            if(!regex.IsMatch(title)){return;}
            if(title[0]=='.' || title[title.Length-1]=='.'){return;}
            if(!this.Logs.ContainsKey(title) || this.Logs[title]==null){ this.Logs[title]=new StringBuilder(); }
            this.Logs[title]
                .Append('[').Append(DateTime.Now.ToString("HH:mm:ss",System.Globalization.DateTimeFormatInfo.InvariantInfo)).Append(']').AppendLine()
                .Append(text)
                .AppendLine()
                .AppendLine();
        }
    }
}
