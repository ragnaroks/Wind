using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Host.Helper {
    public static class UnitControl {
        /// <summary>
        /// 刷新单元
        /// </summary>
        public static void RefreshUnits() {
            Program.Logger.Log("HostService","开始刷新单元");
            if(!Directory.Exists(Program.Settings.UnitDirectory)){
                Program.Logger.Log("HostService","单元目录\""+Program.Settings.UnitDirectory+"\"不存在");
            return;}

            DirectoryInfo directoryInfo=null;
            try{directoryInfo=new DirectoryInfo(Program.Settings.UnitDirectory);}catch(Exception _e){
                Program.Logger.Log("HostService","单元列表读取失败,"+_e.Message);
            return;}
            
            FileInfo[] fileInfos=null;
            try{fileInfos=directoryInfo.GetFiles("*.json",SearchOption.TopDirectoryOnly);}catch(Exception _e) {
                Program.Logger.Log("HostService","单元列表读取失败,"+_e.Message);
            return;}
            
            List<String> names=new List<String>();
            foreach (FileInfo fileInfo in fileInfos){
                Entity.UnitSettings unitSettings=null;
                try{
                    FileStream fs=fileInfo.OpenRead();
                    Byte[] buf=new Byte[fs.Length];
                    fs.Read(buf,0,(Int32)fs.Length);
                    fs.Dispose();
                    unitSettings=JsonConvert.DeserializeObject<Entity.UnitSettings>(Encoding.ASCII.GetString(buf));
                }catch(Exception _e) {
                    Program.Logger.Log("HostService","单元配置文件读取失败,"+_e.Message);
                    continue;
                }
                if(unitSettings==null || String.IsNullOrWhiteSpace(unitSettings.AbsolutePath) || !File.Exists(unitSettings.AbsolutePath)){
                    Program.Logger.Log("HostService","单元读取失败,可执行文件路径无效");
                continue;}
                //单元名称
                String name=fileInfo.Name.Replace(".json","");
                names.Add(name);
                Program.Units[name]=new Entity.Unit();
                //构造进程数据,不知道覆盖正在运行的是否会导致问题
                Program.Units[name].ProcessStartInfo=new ProcessStartInfo{UseShellExecute=false,FileName=unitSettings.AbsolutePath,WorkingDirectory=unitSettings.WorkPath};
                if(!String.IsNullOrWhiteSpace(unitSettings.Params)){Program.Units[name].ProcessStartInfo.Arguments=unitSettings.Params;}
                Program.Units[name].ProcessStartInfo.CreateNoWindow=true;
                Program.Units[name].ProcessStartInfo.WindowStyle=ProcessWindowStyle.Hidden;
                if(Program.Units.ContainsKey(name)){
                    Program.Units[name].UnitSettings=unitSettings;//如果已有则只替换单元配置
                }else{
                    Program.Units[name]=new Entity.Unit{UnitSettings=unitSettings,State=0,Process=null};//否则加入单元配置
                }
            }

            //List<String> names_removed=new List<String>();
            foreach(KeyValuePair<String,Entity.Unit> kvp in Program.Units) {
                if (names.Contains(kvp.Key)) {continue;}
                UnitControl.StopUnit(kvp.Key);
                Program.Units.Remove(kvp.Key);
                //names_removed.Add(kvp.Key);
            }
            //for(Int32 i1=0;i1<names_removed.Count;i1++){Program.Units.Remove(names_removed[i1]);}

            Program.Logger.Log("HostService","单元列表读取完成,共"+Program.Units.Count+"个有效单元,无效单元已被清理");
        }

        /// <summary>
        /// 启动所有单元
        /// </summary>
        /// <param name="_ByAutoStart">是否是开机自启事件</param>
        public static void StartAllUnits(Boolean _ByAutoStart=false) {
            if(Program.Units.Count<1){return;}
            foreach (KeyValuePair<String,Entity.Unit> kvp in Program.Units){
                if(kvp.Value.State!=0){continue;}//跳过已启动的
                if(_ByAutoStart && !kvp.Value.UnitSettings.AutoStart){continue;}//如果是开机自启,则跳过未设置自启的
                kvp.Value.Process=new Process{StartInfo=kvp.Value.ProcessStartInfo};
                kvp.Value.Process.EnableRaisingEvents=true;
                kvp.Value.Process.Exited+=OnUnitExited;
                Task.Run(async ()=>{
                    kvp.Value.State=1;
                    if (kvp.Value.UnitSettings.AutoStartDelay>0){await Task.Delay(kvp.Value.UnitSettings.AutoStartDelay*1000);}
                    Boolean b1=false;
                    try{
                        b1=kvp.Value.Process.Start();
                    }catch(Exception _e){
                        kvp.Value.State=0;
                        Program.Logger.Log("Unit_"+kvp.Key,"单元执行异常,"+_e.Message);
                    return;}
                    if(!b1){
                        kvp.Value.State=0;
                        Program.Logger.Log("Unit_"+kvp.Key,"单元执行失败");
                    return;}
                    kvp.Value.State=2;
                    Program.Logger.Log("Unit_"+kvp.Key,"单元执行成功");
                });
            }
        }
        /// <summary>
        /// 启动指定单元
        /// </summary>
        /// <param name="_UnitName"></param>
        public static void StartUnit(String _UnitName){
            if(Program.Units.Count<1 || !Program.Units.ContainsKey(_UnitName) || Program.Units[_UnitName].State!=0){return;}
            Entity.Unit unit=Program.Units[_UnitName];
            unit.Process=new Process{StartInfo=unit.ProcessStartInfo};
            unit.Process.EnableRaisingEvents=true;
            unit.Process.Exited+=OnUnitExited;
            unit.State=1;
            Boolean b1=false;
            try {
                b1=unit.Process.Start();
            }catch(Exception _e){
                unit.State=0;
                Program.Logger.Log("Unit_"+_UnitName,"单元执行异常,"+_e.Message);
            return;}
            if(!b1){
                unit.State=0;
                Program.Logger.Log("Unit_"+_UnitName,"单元执行失败");
            return;}
            unit.State=2;
            Program.Logger.Log("Unit_"+_UnitName,"单元执行成功");
            return;
        }

        /// <summary>
        /// 停止所有单元
        /// </summary>
        public static void StopAllUnits() {
            if(Program.Units.Count<1){return;}
            foreach (KeyValuePair<String,Entity.Unit> kvp in Program.Units) {
                if(kvp.Value.State==0){continue;}
                try{kvp.Value.Process.Kill();}catch(Exception _e){Program.Logger.Log("Unit_"+kvp.Key,"单元停止异常,"+_e.Message);continue;}
                kvp.Value.State=0;
                kvp.Value.Process.Dispose();
                kvp.Value.Process=null;
                Program.Logger.Log("Unit_"+kvp.Key,"单元停止成功");
            }
        }
        /// <summary>
        /// 停止指定单元
        /// </summary>
        /// <param name="_UnitName"></param>
        public static void StopUnit(String _UnitName) {
            if(Program.Units.Count<1 || !Program.Units.ContainsKey(_UnitName) || Program.Units[_UnitName].State==0){return;}
            Entity.Unit unit=Program.Units[_UnitName];
            try{unit.Process.Kill();}catch(Exception _e){Program.Logger.Log("Unit_"+_UnitName,"单元停止异常,"+_e.Message);return;}
            unit.State=0;
            unit.Process.Dispose();
            unit.Process=null;
            Program.Logger.Log("Unit_"+_UnitName,"单元停止成功");
        }

        /// <summary>
        /// 某个单元被动停止后,遍历所有单元并释放已停止单元
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnUnitExited(object sender,EventArgs e) {
            if(Program.Units.Count<1){return;}
            List<String> stopped=new List<String>();
            foreach(KeyValuePair<String,Entity.Unit> kvp in Program.Units) {
                if(kvp.Value.State==0 || kvp.Value.Process==null || !kvp.Value.Process.HasExited){continue;}
                stopped.Add(kvp.Key);
            }
            if(stopped.Count<1){return;}
            foreach (String name in stopped) {
                Program.Units[name].Process.Dispose();
                Program.Units[name].Process=null;
                Program.Units[name].State=0;
                Program.Logger.Log("Unit_"+name,"单元被动停止,已释放");
            }
        }
    }
}
