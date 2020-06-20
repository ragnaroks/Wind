using Fleck;
using Google.Protobuf;
using PeterKottas.DotNetCore.WindowsService.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using wind.Entities.Common;
using wind.Entities.Protobuf;
using wind.Helpers;

namespace wind.Modules {
    /// <summary>远程(WebSocket)管理模块</summary>
    public class WebSocketControlModule:IDisposable {
        public Boolean Useable{get;private set;}=false;

        /// <summary>ipv4正则</summary>
        private Regex RegexAddress4{get;}=new Regex(@"^[0-9\.]{7,15}$",RegexOptions.Compiled);
        /// <summary>ipv6正则</summary>
        private Regex RegexAddress6{get;}=new Regex(@"^[a-f0-9\:\[\]]{5,41}$",RegexOptions.Compiled);
        /// <summary>key正则</summary>
        private Regex RegexControlKey{get;}=new Regex(@"^\S{32,4096}$",RegexOptions.Compiled);
        /// <summary>ping字节数组</summary>
        private Byte[] PingBytes{get;}=new Byte[9]{0x33,0x33,0x37,0x38,0x34,0x35,0x38,0x31,0x38};
        /// <summary>Key</summary>
        private String ControlKey{get;set;}=null;
        /// <summary>定时器</summary>
        private System.Threading.Timer Timer{get;set;}=null;
        /// <summary>定时器是否启用</summary>
        private Boolean TimerEnable{get;set;}=false;
        /// <summary>websocket服务器</summary>
        private WebSocketServer Server{get;set;}=null;
        /// <summary>客户端列表</summary>
        private Dictionary<String,ClientConnection> ClientConnectionDictionary{get;set;}=new Dictionary<String,ClientConnection>();

        /**
         * 此模块中均使用 this.ClientConnectionDictionary[clientConnectionId]?.Method() 来操作
         * 因为具体的 Item 可能已经因为断线或其他原因被移除了
         */

        #region IDisposable
        private bool disposedValue;

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)
                    this.Server?.Dispose();
                    this.Timer?.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                this.ClientConnectionDictionary=null;

                disposedValue=true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~WebSocketControlModule(){
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }

        public void Dispose() {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        /// <param name="controlKey"></param>
        /// <returns></returns>
        public Boolean Setup(String address,Int32 port,String controlKey){
            if(this.Useable){return true;}
            //校验参数
            if(String.IsNullOrWhiteSpace(address) || port>Int16.MaxValue || port<1024){
                LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.Setup[Error]",$"初始化模块失败,参数错误\naddress:{address},port:{port}");
                return false;
            }
            Boolean isV4=this.RegexAddress4.IsMatch(address);
            Boolean isV6=this.RegexAddress6.IsMatch(address);
            if(address!="localhost" && !isV4 && !isV6){
                LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.Setup[Error]",$"初始化模块失败,参数错误\naddress:{address},port:{port}");
                return false;
            }
            if(String.IsNullOrWhiteSpace(controlKey) || !this.RegexControlKey.IsMatch(controlKey)){
                LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.Setup[Error]",$"初始化模块失败,参数错误\ncontrolKey:{controlKey}");
                return false;
            }
            this.ControlKey=controlKey;
            //初始化定时器
            try {
                this.Timer=new System.Threading.Timer(this.TimerCallback,null,0,16000);
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.Setup[Error]",$"初始化定时器异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return false;
            }
            this.TimerEnable=true;
            //初始化服务端
            String location;
            if(address=="localhost") {
                location=$"ws://[::1]:{port}";
            } else {
                location=$"ws://{address}:{port}";
            }
            try {
                this.Server=new WebSocketServer(location,false);
                this.Server.ListenerSocket.NoDelay=false;
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.Setup[Error]",$"初始化服务端异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return false;
            }
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.Setup",$"服务端监听在 {location}");
            //完成
            this.Useable=true;
            return true;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        public Boolean Start(){
            if(!this.Useable){return false;}
            try {
                this.Server.Start((clientWebSocketConnection)=>{
                    clientWebSocketConnection.OnOpen=()=>{
                        String clientConnectionId=clientWebSocketConnection.ConnectionInfo.Id.ToString();
                        this.OnClientConnectionOpen(clientConnectionId,clientWebSocketConnection);
                    };
                    clientWebSocketConnection.OnClose=()=>{
                        String clientConnectionId=clientWebSocketConnection.ConnectionInfo.Id.ToString();
                        if(!this.ClientConnectionDictionary.ContainsKey(clientConnectionId)){return;}
                        this.OnClientConnectionClose(this.ClientConnectionDictionary[clientConnectionId]);
                    };
                    clientWebSocketConnection.OnError=(exception)=>{
                        String clientConnectionId=clientWebSocketConnection.ConnectionInfo.Id.ToString();
                        if(!this.ClientConnectionDictionary.ContainsKey(clientConnectionId)){
                            clientWebSocketConnection.Close();
                            return;
                        }
                        this.OnClientConnectionError(this.ClientConnectionDictionary[clientConnectionId],exception);
                    };
                    clientWebSocketConnection.OnPing=(bytes)=>{
                        String clientConnectionId=clientWebSocketConnection.ConnectionInfo.Id.ToString();
                        if(!this.ClientConnectionDictionary.ContainsKey(clientConnectionId)){
                            clientWebSocketConnection.Close();
                            return;
                        }
                        this.OnClientConnectionPing(this.ClientConnectionDictionary[clientConnectionId],bytes);
                    };
                    clientWebSocketConnection.OnPong=(bytes)=>{
                        String clientConnectionId=clientWebSocketConnection.ConnectionInfo.Id.ToString();
                        if(!this.ClientConnectionDictionary.ContainsKey(clientConnectionId)){
                            clientWebSocketConnection.Close();
                            return;
                        }
                        this.OnClientConnectionPong(this.ClientConnectionDictionary[clientConnectionId],bytes);
                    };
                    clientWebSocketConnection.OnMessage=(message)=>{
                        String clientConnectionId=clientWebSocketConnection.ConnectionInfo.Id.ToString();
                        if(!this.ClientConnectionDictionary.ContainsKey(clientConnectionId)){
                            clientWebSocketConnection.Close();
                            return;
                        }
                        this.OnClientConnectionMessage(this.ClientConnectionDictionary[clientConnectionId],message);
                    };
                    clientWebSocketConnection.OnBinary=(binary)=>{
                        String clientConnectionId=clientWebSocketConnection.ConnectionInfo.Id.ToString();
                        if(!this.ClientConnectionDictionary.ContainsKey(clientConnectionId)){
                            clientWebSocketConnection.Close();
                            return;
                        }
                        this.OnClientConnectionBinary(this.ClientConnectionDictionary[clientConnectionId],binary);
                    };
                });
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.Start[Error]",$"启动服务端异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 定时器操作
        /// </summary>
        /// <param name="state"></param>
        private void TimerCallback(Object state) {
            if(!this.Useable || !this.TimerEnable){return;}
            this.TimerEnable=false;
            //无需操作
            if(this.ClientConnectionDictionary.Count<1) {
                this.TimerEnable=true;
                return;
            }
            //检查失效客户端
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.TimerCallback",$"当前有 {this.ClientConnectionDictionary.Count} 个客户端等待检查");
            #if DEBUG
            Int64 ts=DateTimeOffset.Now.ToUnixTimeSeconds()-16;
            #else
            Int64 ts=DateTimeOffset.Now.ToUnixTimeSeconds()-60;
            #endif
            List<String> listToClose=new List<String>();
            foreach(KeyValuePair<String,ClientConnection> item in this.ClientConnectionDictionary){
                if(item.Value.LastOnlineTime<ts) {
                    listToClose.Add(item.Key);
                    continue;
                }
            }
            if(listToClose.Count<1) {
                this.TimerEnable=true;
                return;
            }
            //执行清理
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.TimerCallback",$"当前有 {listToClose.Count} 个客户端将被清理");
            foreach(String item in listToClose){ this.ClientConnectionDictionary[item]?.WebSocketConnection.Close(); }
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.TimerCallback",$"清理完成,当前还有 {this.ClientConnectionDictionary.Count} 个客户端");
            this.TimerEnable=true;
        }

        /// <summary>
        /// 客户端链接
        /// </summary>
        /// <param name="clientConnectionId"></param>
        /// <param name="clientWebSocketConnection"></param>
        private void OnClientConnectionOpen(String clientConnectionId,IWebSocketConnection clientWebSocketConnection){
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.OnClientConnectionOpen",$"客户端 {clientConnectionId} 已链接");
            //加入列表
            ClientConnection clientConnection=new ClientConnection{
                Id=clientConnectionId,LastOnlineTime=DateTimeOffset.Now.ToUnixTimeSeconds(),Valid=false,WebSocketConnection=clientWebSocketConnection};
            this.ClientConnectionDictionary.Add(clientConnectionId,clientConnection);
            //回复
            ServerAcceptConnectionProtobuf serverAcceptConnectionProtobuf=new ServerAcceptConnectionProtobuf{Type=21,ConnectionId=clientConnectionId};
            clientWebSocketConnection.Send(serverAcceptConnectionProtobuf.ToByteArray());
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.OnClientConnectionOpen",$"已向客户端 {clientConnectionId} 回复链接成功");
        }
        /// <summary>
        /// 客户端断开,如果是服务端主动调用close,则发正在这之后
        /// </summary>
        /// <param name="clientConnection"></param>
        private void OnClientConnectionClose(ClientConnection clientConnection){
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.OnClientConnectionClose",$"客户端 {clientConnection.Id} 已断开链接");
            this.ClientConnectionDictionary.Remove(clientConnection.Id);
        }
        /// <summary>
        /// 客户端链接异常
        /// </summary>
        /// <param name="clientConnection"></param>
        /// <param name="exception"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance","CA1822:将成员标记为 static",Justification = "<挂起>")]
        private void OnClientConnectionError(ClientConnection clientConnection,Exception exception){
            LoggerModuleHelper.TryLog(
                    "Modules.WebSocketControlModule.OnClientConnectionError",
                    $"客户端 {clientConnection.Id} 出现异常\n异常消息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
            if(clientConnection.WebSocketConnection.IsAvailable) {
                clientConnection.WebSocketConnection.Close();
            } else {
                this.ClientConnectionDictionary.Remove(clientConnection.Id);
            }
        }
        /// <summary>
        /// 客户端发来ping
        /// </summary>
        /// <param name="clientConnection"></param>
        /// <param name="bytes"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance","CA1822:将成员标记为 static",Justification = "<挂起>")]
        private void OnClientConnectionPing(ClientConnection clientConnection,Byte[] bytes){
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.OnClientConnectionPing",$"客户端 {clientConnection.Id} Ping");
            clientConnection.LastOnlineTime=DateTimeOffset.Now.ToUnixTimeSeconds();
            _=clientConnection.WebSocketConnection.SendPong(bytes);
        }
        /// <summary>
        /// 客户端发来pong
        /// </summary>
        /// <param name="clientConnection"></param>
        /// <param name="bytes"></param>
        private void OnClientConnectionPong(ClientConnection clientConnection,Byte[] bytes){
            if(!this.PingBytes.SequenceEqual(bytes)) {
                LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.OnClientConnectionPong[Warning]",$"客户端 {clientConnection.Id} Pong,数据错误");
                clientConnection.WebSocketConnection.Close();
                return;
            }
            clientConnection.LastOnlineTime=DateTimeOffset.Now.ToUnixTimeSeconds();
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.OnClientConnectionPong",$"客户端 {clientConnection.Id} Pong");
        }
        /// <summary>
        /// 客户端发来字符串消息
        /// </summary>
        /// <param name="clientConnection"></param>
        /// <param name="message"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance","CA1822:将成员标记为 static",Justification = "<挂起>")]
        private void OnClientConnectionMessage(ClientConnection clientConnection,String message){
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.OnClientConnectionMessage",$"客户端 {clientConnection.Id} 发来字符串消息 {message}");
            clientConnection.LastOnlineTime=DateTimeOffset.Now.ToUnixTimeSeconds();
            _=clientConnection.WebSocketConnection.Send(message);
        }
        /// <summary>
        /// 客户端发来二进制消息
        /// </summary>
        /// <param name="clientConnection"></param>
        /// <param name="binary"></param>
        private void OnClientConnectionBinary(ClientConnection clientConnection,Byte[] binary){
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.OnClientConnectionBinary",$"客户端 {clientConnection.Id} 发来二进制消息");
            //尝试解析数据包类型
            PacketTestProtobuf packetTestProtobuf;
            try {
                packetTestProtobuf=PacketTestProtobuf.Parser.ParseFrom(binary);
            }catch(Exception exception) {
                LoggerModuleHelper.TryLog(
                    "Modules.WebSocketControlModule.OnClientConnectionBinary",
                    $"解析客户端 {clientConnection.Id} 二进制消息异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                _=clientConnection.WebSocketConnection.Send(binary);
                return;
            }
            //更新在线
            clientConnection.LastOnlineTime=DateTimeOffset.Now.ToUnixTimeSeconds();
            //分拣
            switch(packetTestProtobuf.Type){
                case 1:break;//心跳包,忽略
                case 12:this.ClientOfferControlKey(clientConnection,binary);break;
                case 1001:this.StatusRequest(clientConnection,binary);break;
                default:_=clientConnection.WebSocketConnection.Send(binary);break;//无法识别则原样回复
            }
            LoggerModuleHelper.TryLog("Modules.WebSocketControlModule.OnClientConnectionBinary",$"已处理客户端 {clientConnection.Id} 二进制消息");
        }

        /// <summary>
        /// 客户端向服务端请求验证ControlKey
        /// </summary>
        /// <param name="clientConnection"></param>
        /// <param name="binary"></param>
        private void ClientOfferControlKey(ClientConnection clientConnection,Byte[] binary){
            //解析数据包
            ClientOfferControlKeyProtobuf clientOfferControlKeyProtobuf;
            try {
                clientOfferControlKeyProtobuf=ClientOfferControlKeyProtobuf.Parser.ParseFrom(binary);
            }catch(Exception exception){
                LoggerModuleHelper.TryLog(
                    "Modules.WebSocketControlModule.ClientOfferControlKey[Error]",
                    $"解析客户端 {clientConnection.Id} 二进制消息异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                _=clientConnection.WebSocketConnection.Send(binary);
                return;
            }
            //服务端回复客户端ControlKey验证结果
            Boolean valid=clientOfferControlKeyProtobuf.ConnectionId==clientConnection.Id && clientOfferControlKeyProtobuf.ControlKey==this.ControlKey;
            ServerValidateConnectionProtobuf serverValidateConnectionProtobuf=new ServerValidateConnectionProtobuf{Type=22,ConnectionId=clientConnection.Id,Valid=valid};
            //回复
            _=clientConnection.WebSocketConnection.Send(serverValidateConnectionProtobuf.ToByteArray());
        }
        /// <summary>
        /// windctl status unitKey
        /// </summary>
        /// <param name="clientConnection"></param>
        /// <param name="binary"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance","CA1822:将成员标记为 static",Justification = "<挂起>")]
        private void StatusRequest(ClientConnection clientConnection,Byte[] binary){
            //解析数据包
            StatusRequestProtobuf statusRequestProtobuf;
            try {
                statusRequestProtobuf=StatusRequestProtobuf.Parser.ParseFrom(binary);
            }catch(Exception exception){
                LoggerModuleHelper.TryLog(
                    "Modules.WebSocketControlModule.StatusRequest[Error]",
                    $"解析客户端 {clientConnection.Id} 二进制消息异常\n异常信息:{exception.Message}\n异常堆栈:{exception.StackTrace}");
                _=clientConnection.WebSocketConnection.Send(binary);
                return;
            }
            //初始化响应体
            StatusResponseProtobuf statusResponseProtobuf=new StatusResponseProtobuf{Type=2001,UnitKey=statusRequestProtobuf.UnitKey};
            //无效unit
            if(String.IsNullOrWhiteSpace(statusRequestProtobuf.UnitKey)){
                statusResponseProtobuf.NoExecuteMessage="unitKey invalid";
                _=clientConnection.WebSocketConnection.Send(statusResponseProtobuf.ToByteArray());
                return;
            }
            if(!Program.UnitManageModule.Useable){
                statusResponseProtobuf.NoExecuteMessage="unit manager not available";
                _=clientConnection.WebSocketConnection.Send(statusResponseProtobuf.ToByteArray());
                return;
            }
            Entities.Common.Unit unit=Program.UnitManageModule.GetUnit(statusRequestProtobuf.UnitKey);
            if(unit==null) {
                statusResponseProtobuf.NoExecuteMessage="unit not found";
                _=clientConnection.WebSocketConnection.Send(statusResponseProtobuf.ToByteArray());
                return;
            }
            //构造数据
            UnitProcessProtobuf unitProcessProtobuf=new UnitProcessProtobuf();
            if(unit.State==2) {
                unitProcessProtobuf.Id=unit.ProcessId;
                unitProcessProtobuf.StartTime=unit.Process.StartTime.ToLocalTimestamp();
            }
            UnitSettingsProtobuf unitSettingsProtobuf=new UnitSettingsProtobuf{
                Name=unit.Settings.Name,Description=unit.Settings.Description,Type=unit.Settings.Type,AbsoluteExecutePath=unit.Settings.AbsoluteExecutePath,
                AbsoluteWorkDirectory=unit.Settings.AbsoluteWorkDirectory,Arguments=String.IsNullOrWhiteSpace(unit.Settings.Arguments)?String.Empty:unit.Settings.Arguments,
                AutoStart=unit.Settings.AutoStart,AutoStartDelay=unit.Settings.AutoStartDelay,RestartWhenException=unit.Settings.RestartWhenException,
                MonitorPerformanceUsage=unit.Settings.MonitorPerformanceUsage,MonitorNetworkUsage=unit.Settings.MonitorNetworkUsage};
            UnitSettingsProtobuf unitRunningSettingsProtobuf=new UnitSettingsProtobuf();
            if(unit.State==2){
                unitRunningSettingsProtobuf=new UnitSettingsProtobuf{
                    Name=unit.RunningSettings.Name,Description=unit.RunningSettings.Description,Type=unit.RunningSettings.Type,
                    AbsoluteExecutePath=unit.RunningSettings.AbsoluteExecutePath,AbsoluteWorkDirectory=unit.RunningSettings.AbsoluteWorkDirectory,
                    Arguments=String.IsNullOrWhiteSpace(unit.RunningSettings.Arguments)?String.Empty:unit.RunningSettings.Arguments,
                    AutoStart=unit.RunningSettings.AutoStart,AutoStartDelay=unit.RunningSettings.AutoStartDelay,
                    RestartWhenException=unit.RunningSettings.RestartWhenException,MonitorPerformanceUsage=unit.RunningSettings.MonitorPerformanceUsage,
                    MonitorNetworkUsage=unit.RunningSettings.MonitorNetworkUsage};
            }            
            UnitPerformanceCounterProtobuf unitPerformanceCounterProtobuf=new UnitPerformanceCounterProtobuf();
            if(Program.UnitPerformanceCounterModule.Useable && unit.State==2 && unitRunningSettingsProtobuf.MonitorPerformanceUsage){
                unitPerformanceCounterProtobuf.CPU=Program.UnitPerformanceCounterModule.GetCpuValue(unit.ProcessId);
                unitPerformanceCounterProtobuf.RAM=Program.UnitPerformanceCounterModule.GetRamValue(unit.ProcessId);
            }
            UnitNetworkCounterProtobuf unitNetworkCounterProtobuf=new UnitNetworkCounterProtobuf();
            if(Program.UnitNetworkCounterModule.Useable && unit.State==2 && unitRunningSettingsProtobuf.MonitorNetworkUsage){
                UnitNetworkCounter unitNetworkCounter=Program.UnitNetworkCounterModule.GetValue(unit.ProcessId);
                if(unitNetworkCounter!=null){
                    unitNetworkCounterProtobuf.SendSpeed=unitNetworkCounter.SendSpeed;
                    unitNetworkCounterProtobuf.ReceiveSpeed=unitNetworkCounter.ReceiveSpeed;
                    unitNetworkCounterProtobuf.TotalSent=unitNetworkCounter.TotalSent;
                    unitNetworkCounterProtobuf.TotalReceived=unitNetworkCounter.TotalReceived;
                }
            }
            UnitProtobuf unitProtobuf=new UnitProtobuf{
                Key=unit.Key,State=unit.State,
                SettingsFilePath=String.Concat(Program.AppEnvironment.UnitsDirectory,Path.DirectorySeparatorChar,unit.Key,".json"),
                ProcessProtobuf=unitProcessProtobuf,SettingsProtobuf=unitSettingsProtobuf,RunningSettingsProtobuf=unitRunningSettingsProtobuf,
                PerformanceCounterProtobuf=unitPerformanceCounterProtobuf,NetworkCounterProtobuf=unitNetworkCounterProtobuf};
            statusResponseProtobuf.UnitProtobuf=unitProtobuf;
            statusResponseProtobuf.Executed=true;
            //回复
            _=clientConnection.WebSocketConnection.Send(statusResponseProtobuf.ToByteArray());
        }
    }
}
