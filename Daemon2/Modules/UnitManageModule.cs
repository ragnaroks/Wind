using Daemon.Entities.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Daemon.Modules {
    public class UnitManageModule:IDisposable{
        public Boolean Useable{get;private set;}=false;

        private Dictionary<String,Unit> UnitDictionary{get;set;}=new Dictionary<String,Unit>();
        private String UnitsDirectory{get;set;}=null;
        
        #region IDisposable
        private bool disposedValue;
        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                disposedValue=true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~UnitManageModule()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose() {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public Boolean Setup(String unitsDirectory){
            if(this.Useable){return true;}
            //检查单元存放路径,不要尝试创建
            if(!Directory.Exists(unitsDirectory)){return false;}
            this.UnitsDirectory=unitsDirectory;
            //
            this.Useable=true;
            return true;
        }

        public void ParseAllUnits() {

        }

        public void ReloadUnit(){
            //
        }

        public void ReloadAllUnit() {
            //
        }
    }
}
