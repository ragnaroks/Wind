using AsyncNet.Udp.Server;
using Host.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Threading;
using AsyncNet.Udp.Server.Events;
using AsyncNet.Udp.Error.Events;
using AsyncNet.Udp.Remote.Events;
using System.Diagnostics;

namespace Host.Module {
    public class UdpSocketServer{
        private AsyncNetUdpServer AsyncNetUdpServer{get;set;}
        private List<Entity.UdpSocketRemote> Remotes{get;set;}=null;
        private Timer Timer{get;set;}=null;

        public UdpSocketServer(){
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
            try{IPEndPoint ipep=new IPEndPoint(ip,Program.AppSettings.ControlPort);}catch(Exception _e){Program.Logger.Log("UdpSocketServer","创建端点异常,"+_e.Message);return;}//只是为了验证是否能创建端点
            AsyncNetUdpServerConfig config=new AsyncNetUdpServerConfig{IPAddress=ip,Port=Program.AppSettings.ControlPort};
            this.AsyncNetUdpServer=new AsyncNetUdpServer(config);
            this.AsyncNetUdpServer.ServerStarted+=this.AsyncNetUdpServer_ServerStarted;
            this.AsyncNetUdpServer.ServerStopped+=this.AsyncNetUdpServer_ServerStopped;
            this.AsyncNetUdpServer.ServerExceptionOccured+=this.AsyncNetUdpServer_ServerExceptionOccured;
            this.AsyncNetUdpServer.UdpPacketArrived+=this.AsyncNetUdpServer_UdpPacketArrived;
            this.AsyncNetUdpServer.UdpSendErrorOccured+=this.AsyncNetUdpServer_UdpSendErrorOccured;
            this.AsyncNetUdpServer.StartAsync();
            this.Remotes=new List<Entity.UdpSocketRemote>();
            this.Timer=new Timer(new TimerCallback(CleanIdleRemote),null,10000,10000);
        }

        /// <summary>
        /// 当前会话集合是否包含指定IPEndPoint
        /// </summary>
        /// <param name="_IPEndPoint"></param>
        /// <returns></returns>
        private Boolean IncludeRemote(IPEndPoint _IPEndPoint) {
            if(this.Remotes.Count<1){return false;}
            for(Int32 i = 0;i<this.Remotes.Count;i++) {
                if(this.Remotes[i].IPEndPoint.Equals(_IPEndPoint)){return true;}
            }
            return false;
        }
        /// <summary>
        /// 更新会话信息
        /// </summary>
        /// <param name="_IPEndPoint"></param>
        private void UpdateRemote(IPEndPoint _IPEndPoint) {
            if(this.Remotes.Count<1){return;}
            for(Int32 i = 0;i<this.Remotes.Count;i++) {
                if(!this.Remotes[i].IPEndPoint.Equals(_IPEndPoint)){continue;}
                this.Remotes[i].LastUpdateTime=DateTimeOffset.Now.ToUnixTimeSeconds();
            }
        }
        /// <summary>
        /// 清理闲置会话
        /// </summary>
        private void CleanIdleRemote(Object _state) {
            if(this.Remotes.Count<1){return;}
            Int64 tsn=DateTimeOffset.Now.ToUnixTimeSeconds();
            List<Entity.UdpSocketRemote> remove=new List<Entity.UdpSocketRemote>();
            for(Int32 i = 0;i<this.Remotes.Count;i++) {
                if(this.Remotes[i].LastUpdateTime+60>tsn){continue;}
                remove.Add(this.Remotes[i]);
            }
            if(remove.Count<1){return;}
            for(Int32 i = 0;i<remove.Count;i++) {
                this.Remotes.Remove(remove[i]);
                Program.Logger.Log("UdpSocketServer",$"清理闲置会话=>{remove[i].IPEndPoint.Address}:{remove[i].IPEndPoint.Port}");
            }
        }

        /// <summary>
        /// 服务端启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncNetUdpServer_ServerStarted(object sender,UdpServerStartedEventArgs e)=>Program.Logger.Log("UdpSocketServer","Started");
        /// <summary>
        /// 服务端停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncNetUdpServer_ServerStopped(object sender,UdpServerStoppedEventArgs e)=>Program.Logger.Log("UdpSocketServer","Stopped");
        /// <summary>
        /// 服务端异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncNetUdpServer_ServerExceptionOccured(object sender,UdpServerExceptionEventArgs e)=>Program.Logger.Log("UdpSocketServer","ExceptionOccured=>"+e.Exception.Message);
        /// <summary>
        /// 服务端发送异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncNetUdpServer_UdpSendErrorOccured(object sender,UdpSendErrorEventArgs e)=>Program.Logger.Log("UdpSocketServer",$"UdpSendErrorOccured=>{e.Packet.RemoteEndPoint.Address}:{e.Packet.RemoteEndPoint.Port}=>{e.Exception.Message}");
        /// <summary>
        /// 服务端收到数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsyncNetUdpServer_UdpPacketArrived(object sender,UdpPacketArrivedEventArgs e) {
            String dataJson=AesEncrypt.Decrypt(Program.AppSettings.ControlKey,e.PacketData).TrimEnd('\0');
            Program.Logger.Log("UdpSocketServer",$"UdpPacketArrived=>{e.RemoteEndPoint.Address}:{e.RemoteEndPoint.Port}=>{dataJson}");
            if(String.IsNullOrWhiteSpace(dataJson) || dataJson.IndexOf("ActionId")<0 ||dataJson.IndexOf("ActionName")<0) {
                this.AsyncNetUdpServer.Post($"UnknownDataPacket,Logged\"{e.RemoteEndPoint.Address}:{e.RemoteEndPoint.Port}\"".GetBytes_Utf8(),e.RemoteEndPoint);
            return;}
            Entity.UdpSocketPacketRecive packet=null;
            try {packet=JsonConvert.DeserializeObject<Entity.UdpSocketPacketRecive>(dataJson);} catch{}
            if(packet==null){
                this.AsyncNetUdpServer.Post($"UnknownDataPacket,Logged\"{e.RemoteEndPoint.Address}:{e.RemoteEndPoint.Port}\"".GetBytes_Utf8(),e.RemoteEndPoint);
            return;}
            switch (packet.ActionId) {
                case 1:this.PacketAction1(e.RemoteEndPoint);break;
                case 2:this.PacketAction2(e.RemoteEndPoint);break;
                case 1001:this.PacketAction1001(e.RemoteEndPoint);break;
                case 1002:this.PacketAction1002(e.RemoteEndPoint);break;
                case 1003:this.PacketAction1003(e.RemoteEndPoint,packet);break;
                case 1004:this.PacketAction1004(e.RemoteEndPoint);break;
                case 1005:this.PacketAction1005(e.RemoteEndPoint,packet);break;
            }
        }

        /// <summary>
        /// 数据处理 Hello
        /// </summary>
        /// <param name="_IPEndPoint"></param>
        private void PacketAction1(IPEndPoint _IPEndPoint) {
            if(!this.IncludeRemote(_IPEndPoint)){
                this.Remotes.Add(new Entity.UdpSocketRemote{IPEndPoint=_IPEndPoint,LastUpdateTime=DateTimeOffset.Now.ToUnixTimeSeconds()});
            } else {
                this.UpdateRemote(_IPEndPoint);
            }
            Object packet=new{ActionId=1,ActionName="Hello",ErrorCode=0,ErrorMessage=String.Empty,Message=$"Hello,{_IPEndPoint.Address}:{_IPEndPoint.Port}"};
            String json=JsonConvert.SerializeObject(packet);
            Program.Logger.Log("UdpSocketServer",$"UdpPacketSent=>{_IPEndPoint.Address}:{_IPEndPoint.Port}=>{json}");
            this.AsyncNetUdpServer.SendAsync(AesEncrypt.Encrypt(Program.AppSettings.ControlKey,json),_IPEndPoint);
        }
        /// <summary>
        /// 数据处理 GetHostVersion
        /// </summary>
        /// <param name="_IPEndPoint"></param>
        private void PacketAction2(IPEndPoint _IPEndPoint){
            if(!this.IncludeRemote(_IPEndPoint)){return;}else{this.UpdateRemote(_IPEndPoint);}
            Process p1=Process.GetCurrentProcess();
            Object packet=new{
                ActionId=2,ActionName="GetHostVersion",ErrorCode=0,ErrorMessage=String.Empty,
                HostServiceVersion=System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                HostServicePid=p1.Id,
                HostServiceMemoryUsageBytes=p1.PrivateMemorySize64,
                HostServiceThreadsCount=p1.Threads.Count,
#pragma warning disable IDE0037 // 使用推断的成员名称
                MachineName = p1.MachineName,
#pragma warning restore IDE0037 // 使用推断的成员名称
                MachineProcessorCount =Environment.ProcessorCount,
                MachineMemoryTotalBytes=WMI.GetPhysicalMemorySize()
                //MachineMemoryAvailableBytes=PerformanceCounters.GetMachineMemoryAvailableBytes(),
            };
            p1.Dispose();
            String json=JsonConvert.SerializeObject(packet);
            Program.Logger.Log("UdpSocketServer",$"UdpPacketSent=>{_IPEndPoint.Address}:{_IPEndPoint.Port}=>{json}");
            this.AsyncNetUdpServer.SendAsync(AesEncrypt.Encrypt(Program.AppSettings.ControlKey,json),_IPEndPoint);
        }
        /// <summary>
        /// 数据处理 FetchUnits
        /// </summary>
        /// <param name="_IPEndPoint"></param>
        private void PacketAction1001(IPEndPoint _IPEndPoint) {
            if(!this.IncludeRemote(_IPEndPoint)){return;}else{this.UpdateRemote(_IPEndPoint);}
            Dictionary<String,Object> d1=new Dictionary<String,Object>();
#pragma warning disable IDE0037 // 使用推断的成员名称
            foreach (KeyValuePair<String,Entity.Unit> kvp in Program.Units) {d1[kvp.Key]=new {State=kvp.Value.State,UnitSettings=kvp.Value.UnitSettings}; }
#pragma warning restore IDE0037 // 使用推断的成员名称
            Object packet=new{ActionId=1001,ActionName="FetchUnits",ErrorCode=0,ErrorMessage=String.Empty,Units=d1};
            String json=JsonConvert.SerializeObject(packet);
            Program.Logger.Log("UdpSocketServer",$"UdpPacketSent=>{_IPEndPoint.Address}:{_IPEndPoint.Port}=>{json}");
            this.AsyncNetUdpServer.SendAsync(AesEncrypt.Encrypt(Program.AppSettings.ControlKey,json),_IPEndPoint);
        }
        /// <summary>
        /// 数据处理 StartAllUnits
        /// </summary>
        /// <param name="_IPEndPoint"></param>
        private void PacketAction1002(IPEndPoint _IPEndPoint) {
            if(!this.IncludeRemote(_IPEndPoint)){return;}else{this.UpdateRemote(_IPEndPoint);}
            UnitControl.StartAllUnits();
            Object packet=new{ActionId=1002,ActionName="StartAllUnits",ErrorCode=0,ErrorMessage=String.Empty};
            String json=JsonConvert.SerializeObject(packet);
            Program.Logger.Log("UdpSocketServer",$"UdpPacketSent=>{_IPEndPoint.Address}:{_IPEndPoint.Port}=>{json}");
            this.AsyncNetUdpServer.SendAsync(AesEncrypt.Encrypt(Program.AppSettings.ControlKey,json),_IPEndPoint);
        }
        /// <summary>
        /// 数据处理 StartUnit
        /// </summary>
        /// <param name="_IPEndPoint"></param>
        /// <param name="_UdpSocketPacketRecive"></param>
        private void PacketAction1003(IPEndPoint _IPEndPoint,Entity.UdpSocketPacketRecive _UdpSocketPacketRecive) {
            if(!this.IncludeRemote(_IPEndPoint)){return;}else{this.UpdateRemote(_IPEndPoint);}
            Object packet=new{ActionId=1003,ActionName="StartUnit",ErrorCode=0,ErrorMessage=String.Empty};
            if(String.IsNullOrWhiteSpace(_UdpSocketPacketRecive.UnitName)){packet=new{ActionId=1003,ActionName="StartUnit",ErrorCode=101,ErrorMessage="单元名称无效"};}//TODO 优化
            UnitControl.StartUnit(_UdpSocketPacketRecive.UnitName);
            String json=JsonConvert.SerializeObject(packet);
            Program.Logger.Log("UdpSocketServer",$"UdpPacketSent=>{_IPEndPoint.Address}:{_IPEndPoint.Port}=>{json}");
            this.AsyncNetUdpServer.SendAsync(AesEncrypt.Encrypt(Program.AppSettings.ControlKey,json),_IPEndPoint);
        }
        /// <summary>
        /// 数据处理 StopAllUnits
        /// </summary>
        /// <param name="_IPEndPoint"></param>
        private void PacketAction1004(IPEndPoint _IPEndPoint) {
            if(!this.IncludeRemote(_IPEndPoint)){return;}else{this.UpdateRemote(_IPEndPoint);}
            UnitControl.StopAllUnits();
            Object packet=new{ActionId=1004,ActionName="StopAllUnits",ErrorCode=0,ErrorMessage=String.Empty};
            String json=JsonConvert.SerializeObject(packet);
            Program.Logger.Log("UdpSocketServer",$"UdpPacketSent=>{_IPEndPoint.Address}:{_IPEndPoint.Port}=>{json}");
            this.AsyncNetUdpServer.SendAsync(AesEncrypt.Encrypt(Program.AppSettings.ControlKey,json),_IPEndPoint);
        }
        /// <summary>
        /// 数据处理 StopUnit
        /// </summary>
        /// <param name="_IPEndPoint"></param>
        /// <param name="_UdpSocketPacketRecive"></param>
        private void PacketAction1005(IPEndPoint _IPEndPoint,Entity.UdpSocketPacketRecive _UdpSocketPacketRecive) {
            if(!this.IncludeRemote(_IPEndPoint)){return;}else{this.UpdateRemote(_IPEndPoint);}
            Object packet=new{ActionId=1005,ActionName="StopUnit",ErrorCode=0,ErrorMessage=String.Empty};
            if(String.IsNullOrWhiteSpace(_UdpSocketPacketRecive.UnitName)){packet=new{ActionId=1005,ActionName="StopUnit",ErrorCode=101,ErrorMessage="单元名称无效"};}//TODO 优化
            UnitControl.StopUnit(_UdpSocketPacketRecive.UnitName);
            String json=JsonConvert.SerializeObject(packet);
            Program.Logger.Log("UdpSocketServer",$"UdpPacketSent=>{_IPEndPoint.Address}:{_IPEndPoint.Port}=>{json}");
            this.AsyncNetUdpServer.SendAsync(AesEncrypt.Encrypt(Program.AppSettings.ControlKey,json),_IPEndPoint);
        }
    }
}
