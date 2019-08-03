using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Host.Module {
    public class Logger:IDisposable {
        /// <summary>
        /// 计时器
        /// </summary>
        private Timer Timer{get;set;}=null;
        /// <summary>
        /// 日志根目录,没有"/"
        /// </summary>
        private String LogDirectory{get;set;}=null;
        /// <summary>
        /// 日志
        /// </summary>
        private Dictionary<String,StringBuilder> Logs{get;set;}=null;

        /// <summary>
        /// 计时器触发间隔
        /// </summary>
        public Int32 TimerInterval{get;set;}=1000;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_LogDirectory"></param>
        public Logger(String _LogDirectory){
            if(!Directory.Exists(_LogDirectory)){
                try {Directory.CreateDirectory(_LogDirectory);}catch(Exception _e){throw new IOException("无法创建日志根目录,"+_e.Message);}
            }
            this.LogDirectory=_LogDirectory;
            this.Logs=new Dictionary<String,StringBuilder>();
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
        /// 定时写入日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender,ElapsedEventArgs e) {
            if(this.Logs==null || this.Logs.Count<1){return;}
            String prefix=DateTime.Now.ToString("yyyy-MM-dd");
            FileStream fs=null;
            foreach (KeyValuePair<String,StringBuilder> _log in this.Logs) {
                if(_log.Value==null || _log.Value.Length<1){continue;}
                try{
                    fs=File.OpenWrite(String.Format("{0}{1}{2}_{3}.log",this.LogDirectory,Path.DirectorySeparatorChar,prefix,_log.Key));
                    fs.Seek(0,SeekOrigin.End);
                    Byte[] buffer=Encoding.UTF8.GetBytes(_log.Value.ToString());
                    fs.Write(buffer,0,buffer.GetLength(0));
                    fs.Dispose();
                    _log.Value.Clear();
                } catch (Exception _e) {
                    fs=null;
                    Console.WriteLine("物理写入日志时异常,"+_e.Message);
                    continue;
                }
            }
        }
        
        /// <summary>
        /// 记录
        /// </summary>
        /// <param name="_title"></param>
        /// <param name="_text"></param>
        public void Log(String _title,String _text){
            if(String.IsNullOrWhiteSpace(_title) || String.IsNullOrWhiteSpace(_text)){return;}
            if(!this.Logs.ContainsKey(_title) || this.Logs[_title]==null){this.Logs[_title]=new StringBuilder();}
            this.Logs[_title].Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append(" # ").Append(_text).Append("\n");
        }
    }
}
