using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Daemon.Modules {
    /// <summary>应用程序配置模块</summary>
    public class AppSettingsModule:IDisposable{
        public AppSettingsModule(ref Entities.AppSettings appSettings){
            if(!File.Exists(Program.AppEnvironment.ConfigFilePath)){this.ApplyDefaultAppSettings(ref appSettings);return;}
            if(!this.LoadAppSettings(ref appSettings)){this.ApplyDefaultAppSettings(ref appSettings);return;}
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                
                disposedValue=true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        //~AppSettingsModule(){
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //Dispose(false);
        //}

        // 添加此代码以正确实现可处置模式。
        public void Dispose() {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            //GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// 读取应用程序配置
        /// </summary>
        /// <param name="appSettings"></param>
        /// <returns></returns>
        private Boolean LoadAppSettings(ref Entities.AppSettings appSettings) {
            FileStream fs=null;
            Byte[] bytes=null;
            try {
                fs=File.Open(Program.AppEnvironment.ConfigFilePath,FileMode.Open,FileAccess.Read);
                if(fs.Length>Int32.MaxValue){return false;}
                bytes=new Byte[fs.Length];
                fs.Read(bytes,0,bytes.Length);
                fs.Close();
            }catch(Exception exception){
                ConsoleColor cc=Console.ForegroundColor;
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine($"Modules.AppSettingsModule.LoadAppSettings => {exception.Message} | {exception.StackTrace}");
                Console.ForegroundColor=cc;
                return false;
            } finally {
                if(fs!=null){fs.Dispose();}
            }
            if(bytes==null || bytes.Length<1){return false;}
            String json=Encoding.UTF8.GetString(bytes);
            if(String.IsNullOrWhiteSpace(json)){return false;}
            appSettings=JsonConvert.DeserializeObject<Entities.AppSettings>(json);
            if(appSettings.ControlPort<1024){
                Console.WriteLine($"Modules.AppSettingsModule.LoadAppSettings[Warning] => 远程控制端口不能小于1024,已重置为25565");
                appSettings.ControlPort=25565;
            }
            ConsoleColor cc2=Console.ForegroundColor;
            Console.ForegroundColor=ConsoleColor.Green;
            Console.WriteLine($"Modules.AppSettingsModule.LoadAppSettings => {json}");
            Console.ForegroundColor=cc2;
            return true;
        }

        /// <summary>
        /// 应用默认设置
        /// </summary>
        /// <param name="appSettings"></param>
        private void ApplyDefaultAppSettings(ref Entities.AppSettings appSettings)=>appSettings=new Entities.AppSettings();
    }
}
