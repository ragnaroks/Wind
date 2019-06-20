using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Timers;

namespace Host.Helper {
    class Logger:IDisposable {
        /// <summary>
        /// 定时器
        /// </summary>
        private Timer Timer{get;set;}
        /// <summary>
        /// 日志
        /// </summary>
        private StringBuilder[] Logs{get;set;}
        /// <summary>
        /// 日志目录
        /// </summary>
        private String LogDirectory{get;set;}
        /// <summary>
        /// 文件后缀名
        /// </summary>
        private readonly String[] FileNames=new String[3]{"_debug.log","_error.log","_info.log"};

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_LogDirectory"></param>
        public Logger(String _LogDirectory){
            if(String.IsNullOrWhiteSpace(_LogDirectory)){throw new Exception("未设置日志存放目录\nNot set log files directory");}
            if(!Directory.Exists(_LogDirectory)){try {Directory.CreateDirectory(_LogDirectory);}catch(Exception _e){throw _e;}}
            this.LogDirectory=_LogDirectory;
            this.Timer=new Timer(){AutoReset=true,Enabled=true,Interval=1000};
            this.Timer.Elapsed+=this.Timer_Elapsed;
            this.Logs=new StringBuilder[3];
            this.Logs[0]=new StringBuilder();//Debug
            this.Logs[1]=new StringBuilder();//Error
            this.Logs[2]=new StringBuilder();//Info
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: 释放托管状态(托管对象)。
                    this.Timer.Elapsed-=this.Timer_Elapsed;
                    this.Timer.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                this.Logs=null;
                disposedValue=true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~Logger() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose() {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="_Text"></param>
        /// <param name="_Level"></param>
        public void Log(String _Text,Int32 _Level = 2) {
            if(_Level<0 || _Level>2 || _Level<Program.AppSettings.LogLevel){return;}
            this.Logs[_Level].Append(String.Format("{0} # {1}\n",DateTime.Now.ToString("HH:mm:ss"),_Text));
        }

        /// <summary>
        /// 定时写入日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender,ElapsedEventArgs e){
            for(Int32 i1 = 0;i1<3;i1++) {
                if(this.Logs[i1].Length<1){continue;}
                try {
                    FileStream fs=File.OpenWrite(LogDirectory+Path.DirectorySeparatorChar+DateTime.Now.ToString("yyyy-MM-dd")+this.FileNames[i1]);
                    fs.Seek(0,SeekOrigin.End);
                    Byte[] buffer=Encoding.UTF8.GetBytes(this.Logs[i1].ToString());
                    fs.Write(buffer,0,buffer.GetLength(0));
                    fs.Dispose();
                    this.Logs[i1].Clear();
                }catch{
                    //什么也不做
                }
            }
        }
    }
}
