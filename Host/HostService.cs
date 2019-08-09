using PeterKottas.DotNetCore.WindowsService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;

namespace Host {
    public class HostService:IMicroService {
        private IMicroServiceController controller;
        private List<Entity.UnitSettings> UnitSettings=new List<Entity.UnitSettings>();

        public HostService(){this.controller=null;}
        public HostService(IMicroServiceController _controller) {this.controller=_controller;}

        public void Start() {
            //var a=Function.AesEncrypt.Encrypt(Program.AppSettings.ControlKey,"1234abcdABCD周吴郑王");
            //var b=Function.AesEncrypt.Decrypt(Program.AppSettings.ControlKey,a).TrimEnd('\0');
            Program.Logger.Log("HostService","Start");
            //this.InitUdpSocketServer();
            this.RefreshUnits();
            this.StartUnits();
        }

        public void Stop() {
            this.StopUnits();
            Program.Logger.Log("HostService","Stop");
        }

        /// <summary>
        /// 刷新单元
        /// </summary>
        public void RefreshUnits() {
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
                unitSettings.name=fileInfo.Name.Replace(".json","");
                this.UnitSettings.Add(unitSettings);
            }

            Program.Logger.Log("HostService","单元列表读取完成,共"+this.UnitSettings.Count+"个有效单元");
        }

        /// <summary>
        /// 处理单元
        /// </summary>
        public void StartUnits() {
            if(this.UnitSettings.Count<1){return;}
            foreach (Entity.UnitSettings unitSettings in this.UnitSettings) {
                //跳过已存在的进程
                if(Program.Units.ContainsKey(unitSettings.name)){continue;}
                //理论进程数据
                ProcessStartInfo processStartInfo=new ProcessStartInfo{UseShellExecute=false,FileName=unitSettings.AbsolutePath,WorkingDirectory=unitSettings.WorkPath};
                if(!String.IsNullOrWhiteSpace(unitSettings.Params)){processStartInfo.Arguments=unitSettings.Params;}
                processStartInfo.CreateNoWindow=true;
                processStartInfo.WindowStyle=ProcessWindowStyle.Hidden;
                Process process=new Process{StartInfo=processStartInfo};
                Program.Units[unitSettings.name]=new Entity.Unit{UnitSettings=unitSettings,Process=process};
                //处理
                if(!unitSettings.AutoStart){continue;}
                Task.Run(async ()=>{
                    Program.Units[unitSettings.name].State=1;
                    if (unitSettings.AutoStartDelay>0){await Task.Delay(unitSettings.AutoStartDelay*1000);}
                    Boolean b1=false;
                    try{
                        b1=process.Start();
                    }catch(Exception _e){
                        Program.Logger.Log("Unit_"+unitSettings.name,"单元执行异常,"+_e.Message);
                    }
                    if(!b1){
                        Program.Units[unitSettings.name].State=0;
                        Program.Logger.Log("Unit_"+unitSettings.name,"单元执行失败");
                    return;}
                    Program.Units[unitSettings.name].State=2;
                    Program.Logger.Log("Unit_"+unitSettings.name,"单元执行成功");
                });
            }
        }

        /// <summary>
        /// 停止单元
        /// </summary>
        public void StopUnits() {
            if(Program.Units.Count<1){return;}
            foreach (KeyValuePair<String,Entity.Unit> unit in Program.Units) {
                if(unit.Value.State==0 || unit.Value.Process==null){continue;}
                try{unit.Value.Process.Kill();}catch(Exception _e){Program.Logger.Log("Unit_"+unit.Key,"单元停止异常,"+_e.Message);continue;}
                unit.Value.State=0;
                unit.Value.Process.Dispose();
                Program.Logger.Log("Unit_"+unit.Key,"单元停止成功");
            }
        }

        /*
        /// <summary>
        /// 初始化被控
        /// </summary>
        public void InitUdpSocketServer() {
            if(!Program.AppSettings.ControlEnable){return;}
            if(String.IsNullOrWhiteSpace(Program.AppSettings.ControlAddress) || String.IsNullOrWhiteSpace(Program.AppSettings.ControlKey) || Program.AppSettings.ControlPort<1024){Program.Logger.Log("UdpSocketServer","参数缺失");return;}
            Regex regex_ip=new Regex(@"^[a-f\d\.\:]{3,39}$",RegexOptions.Compiled);
            if(!regex_ip.IsMatch(Program.AppSettings.ControlAddress) && Program.AppSettings.ControlAddress!="any" && Program.AppSettings.ControlAddress!="localhost"){Program.Logger.Log("UdpSocketServer","ControlAddress 配置有误");return;}
            IPAddress ip=null;
            switch (Program.AppSettings.ControlAddress) {
                case "any":ip=IPAddress.Any;break;//ip=IPAddress.IPv6Any;
                case "localhost":ip=IPAddress.Loopback;break;//ip=IPAddress.IPv6Loopback;
                default:if(!IPAddress.TryParse(Program.AppSettings.ControlAddress,out ip)){Program.Logger.Log("UdpSocketServer","ControlAddress 配置有误");return;}break;
            }
            IPEndPoint ipep=null;
            try{ipep=new IPEndPoint(ip,Program.AppSettings.ControlPort);}catch(Exception _e){Program.Logger.Log("UdpSocketServer","创建端点异常,"+_e.Message);return;}
            //Program.UpdSocketServer=new Module.UpdSocketServer{AddressFamily=AddressFamily.InterNetwork,Local=new NetUri(NetType.Udp,ipep),Port=Program.AppSettings.ControlPort};
            //Program.UpdSocketServer.Start();
            Program.UdpSocketServer=new Module.UdpSocketServer();
        }*/
    }
}
