using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Daemon.Modules {
    /// <summary>日志模块</summary>
    public class LoggerModule:IDisposable {
        /// <summary>计时器</summary>
        private readonly Timer Timer;
        /// <summary>日志根目录,没有"/"</summary>
        private readonly String LogsDirectory;
        /// <summary>日志</summary>
        private Dictionary<String,StringBuilder> Logs;
        /// <summary>计时器触发间隔</summary>
        private readonly Int32 TimerInterval;
        
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="logsDirectory"></param>
        public LoggerModule(String logsDirectory,Int32 writeInterval){
            if(!Directory.Exists(logsDirectory)){
                try{
                    Directory.CreateDirectory(logsDirectory);
                }catch(Exception exception){
                    throw new Exception("无法创建日志根目录,"+exception.Message);
                }
            }
            this.LogsDirectory=logsDirectory;
            this.TimerInterval=writeInterval<1000?1000:writeInterval;
            this.Logs=new Dictionary<String,StringBuilder>();
            //this.Logs["Daemon"]=new StringBuilder();
            this.Timer=new Timer{AutoReset=true,Enabled=true,Interval=this.TimerInterval};
            this.Timer.Elapsed+=this.Timer_Elapsed;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: 释放托管状态(托管对象)。
                    this.Timer.Elapsed-=this.Timer_Elapsed;
                    this.Timer.Enabled=false;
                    this.Timer.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
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
        /// 定时写入日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender,ElapsedEventArgs e) {
            this.Timer.Enabled=false;
            if(this.Logs==null || this.Logs.Count<1){return;}
            String datetime=DateTime.Now.ToString("yyyy-MM-dd");
            foreach (KeyValuePair<String,StringBuilder> log in this.Logs) {
                if(log.Value==null || log.Value.Length<1){continue;}
                String directory;
                if(log.Key.IndexOf(".")>-1){
                    directory=this.LogsDirectory+Path.DirectorySeparatorChar+log.Key.Replace('.',Path.DirectorySeparatorChar);
                } else {
                    directory=this.LogsDirectory;
                }
                if(!Directory.Exists(directory)) {
                    try {
                        Directory.CreateDirectory(directory);
                    }catch(Exception exception) {
                        ConsoleColor cc=Console.ForegroundColor;
                        Console.ForegroundColor=ConsoleColor.Red;
                        Console.WriteLine($"Modules.LoggerModule => 写入日志时目录异常 | {exception.Message} | {exception.StackTrace}");
                        Console.ForegroundColor=cc;
                        continue;
                    }
                }
                String filePath=directory+Path.DirectorySeparatorChar+datetime+".log";
                FileStream fs=null;
                try{
                    fs=File.Open(filePath,FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.Read);
                    fs.Seek(0,SeekOrigin.End);
                    ReadOnlySpan<Byte> bytes=Encoding.UTF8.GetBytes(log.Value.ToString());
                    fs.Write(bytes);
                    log.Value.Clear();
                } catch (Exception exception) {
                    ConsoleColor cc=Console.ForegroundColor;
                    Console.ForegroundColor=ConsoleColor.Red;
                    Console.WriteLine($"Modules.LoggerModule => 写入日志时文件异常 | {exception.Message} | {exception.StackTrace}");
                    Console.ForegroundColor=cc;
                    continue;
                } finally {
                    if(fs!=null){fs.Dispose();}
                }
            }
            this.Timer.Enabled=true;
        }
        
        /// <summary>
        /// 记录
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        public void Log(String title,String text){
            if(String.IsNullOrWhiteSpace(title) || String.IsNullOrWhiteSpace(title)){return;}
            if(!this.Logs.ContainsKey(title) || this.Logs[title]==null){this.Logs[title]=new StringBuilder();}
            //this.Logs[title].Append("[").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("]\n").Append(text).Append("\n");
            this.Logs[title].Append($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]\n{text}\n\n");
        }
    }
}
