using Daemon.Helpers;
using Fleck;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Daemon.Modules {
    public class ControlServerModule:IDisposable {
        /// <summary>模块是否可用</summary>
        public Boolean ModuleAvailable{get;}=false;
        /// <summary>websocket地址</summary>
        private String WebSocketServerUrl{get;}
        /// <summary>websocket服务端</summary>
        private WebSocketServer WebSocketServer{get;set;}
        /// <summary>Ping包</summary>
        private Byte[] PingPacket{get;}=new Byte[]{0x07,0x03,0x05,0x05,0x06,0x00,0x08};
        /// <summary>Ping计时器</summary>
        private Timer PingTimer{get;}
        /// <summary>是否可Ping</summary>
        private Boolean PingTimerEnable{get;set;}=false;
        /// <summary>清理无效会话计时器</summary>
        private Timer CleanTimer{get;}
        /// <summary>是否可清理</summary>
        private Boolean CleanTimerEnable{get;set;}=false;
        /// <summary>会话列表</summary>
        private Dictionary<Guid,Entities.WebSocketConnectionWrap> WebSocketConnectionWrapDictionary{get;set;}=new Dictionary<Guid,Entities.WebSocketConnectionWrap>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        public ControlServerModule(Int16 port,String address){
            if(!this.CheckAddress(address)){
                Program.LoggerModule.Log("Modules.ControlServerModule.DameonControlServerModule[Error]","address 参数错误");
                return;
            }
            if(port<1024 || port>(Int16.MaxValue-1)){
                Program.LoggerModule.Log("Modules.ControlServerModule.DameonControlServerModule[Error]","port 参数错误");
                return;
            }
            if(address=="localhost"){address="127.0.0.1";}
            if(address=="any"){address="0.0.0.0";}
            this.WebSocketServerUrl=$"ws://{address}:{port}";
            this.PingTimer=new Timer(OnPingTimerCallback,null,10_000,10_000);
            this.CleanTimer=new Timer(OnCleanTimerCallback,null,60_000,60_000);//timeout 60s
            this.ModuleAvailable=true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)。
                    if(this.WebSocketServer!=null){
                        this.WebSocketServer.Dispose();
                    }
                    this.PingTimer.Dispose();
                    this.CleanTimer.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                this.WebSocketConnectionWrapDictionary.Clear();

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~ControlServerModule(){
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
        /// 检查地址
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private Boolean CheckAddress(String address){
            if(String.IsNullOrWhiteSpace(address)){
                Program.LoggerModule.Log("Modules.ControlServerModule.CheckAddress[Error]","地址不能为空");
                return false;
            }
            if(address=="localhost" || address=="any"){return true;}
            Regex regexV4=new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$",RegexOptions.Compiled);
            Regex regexV6=new Regex(@"^[a-z\d]{2,39}$",RegexOptions.Compiled);
            return regexV4.IsMatch(address) || regexV6.IsMatch(address);
        }

        /// <summary>
        /// 定时ping客户端
        /// </summary>
        /// <param name="state"></param>
        private void OnPingTimerCallback(Object state) {
            if(!this.PingTimerEnable || this.WebSocketConnectionWrapDictionary.Count<1){return;}
            this.PingTimerEnable=false;
            Program.LoggerModule.Log("Modules.ControlServerModule.OnPingTimerCallback","开始ping所有客户端");
            foreach(KeyValuePair<Guid,Entities.WebSocketConnectionWrap> item in this.WebSocketConnectionWrapDictionary){
                item.Value.WebSocketConnection.SendPing(this.PingPacket);
            }
            Program.LoggerModule.Log("Modules.ControlServerModule.OnPingTimerCallback",$"完成ping所有客户端,共{this.WebSocketConnectionWrapDictionary.Count}个");
            this.PingTimerEnable=true;
        }

        /// <summary>
        /// 定时清理超时会话
        /// </summary>
        /// <param name="state"></param>
        private void OnCleanTimerCallback(Object state) {
            if(!this.CleanTimerEnable || this.WebSocketConnectionWrapDictionary.Count<1){return;}
            this.CleanTimerEnable=false;
            Program.LoggerModule.Log("Modules.ControlServerModule.OnCleanTimerCallback","开始清理客户端");
            Int32 count=0;
            Int64 tsn=DateTimeOffset.Now.ToUnixTimeSeconds();
            foreach(KeyValuePair<Guid,Entities.WebSocketConnectionWrap> item in this.WebSocketConnectionWrapDictionary){
                if(item.Value.LastPongTime+60>tsn){continue;}
                item.Value.WebSocketConnection.Close();
                count++;
            }
            Program.LoggerModule.Log("Modules.ControlServerModule.OnCleanTimerCallback",$"完成清理客户端,共{this.WebSocketConnectionWrapDictionary.Count}个,清理{count}个");
            this.CleanTimerEnable=true;
        }

        /// <summary>
        /// 开始服务
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        public void StartServer(){
            const String infoLogPrefix="Modules.ControlServerModule.StartServer";
            const String errorLogPrefix="Modules.ControlServerModule.StartServer[Error]";
            if(!this.ModuleAvailable){return;}
            Program.LoggerModule.Log(infoLogPrefix,"尝试创建WebSocketServer");
            try {
                this.WebSocketServer=new WebSocketServer(this.WebSocketServerUrl,false);
                //this.WebSocketServer.ListenerSocket.NoDelay=true;
            }catch(Exception exception){
                Console.WriteLine($"{errorLogPrefix} => 创建WebSocketServer异常 | {exception.Message} | {exception.StackTrace}");
                Program.LoggerModule.Log(errorLogPrefix,$"创建WebSocketServer异常,{exception.Message},{exception.StackTrace}");
            }
            this.WebSocketServer.Start((webSocketConnection)=>{
                webSocketConnection.OnOpen=()=>this.SocketOnOpenAsync(webSocketConnection);
                webSocketConnection.OnClose=()=>this.SocketOnCloseAsync(webSocketConnection);
                webSocketConnection.OnError=(exception)=>this.SocketOnErrorAsync(webSocketConnection,exception);
                webSocketConnection.OnPing=(bytes)=>this.SocketOnPingAsync(webSocketConnection,bytes);
                webSocketConnection.OnPong=(bytes)=>this.SocketOnPongAsync(webSocketConnection,bytes);
                //webSocketConnection.OnMessage=(message)=>this.SocketOnMessageAsync(webSocketConnection,message);
                webSocketConnection.OnMessage=(message) => {
                    webSocketConnection.Send(message);
                    Console.WriteLine($"{infoLogPrefix} => 客户端\"{webSocketConnection.ConnectionInfo.Id}\"发来文本消息,已做Echo处理");
                    Program.LoggerModule.Log(infoLogPrefix,$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"发来文本消息,已做Echo处理");
                };
                webSocketConnection.OnBinary=(bytes)=>this.SocketOnBinaryAsync(webSocketConnection,bytes);
            });
        }

        /// <summary>
        /// 接收到来自客户端打开链接请求
        /// </summary>
        /// <param name="webSocketConnection"></param>
        private async void SocketOnOpenAsync(IWebSocketConnection webSocketConnection){
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnOpenAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求链接");
            Entities.WebSocketConnectionWrap webSocketConnectionWrap =new Entities.WebSocketConnectionWrap {
                WebSocketConnectionId=webSocketConnection.ConnectionInfo.Id,
                WebSocketConnection=webSocketConnection};
            this.WebSocketConnectionWrapDictionary.Add(webSocketConnection.ConnectionInfo.Id,webSocketConnectionWrap);
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnOpenAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"允许链接并加入临时列表");
            Protocol.WebSocketServerResponseAfterOnOpen protobuf=new Protocol.WebSocketServerResponseAfterOnOpen{
                Type=2001,
                ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),
                HelloMessage=$"{webSocketConnection.ConnectionInfo.ClientIpAddress}:{webSocketConnection.ConnectionInfo.ClientPort} websocket connected,please validate your control key"};
            await webSocketConnection.Send(protobuf.ToBytes()).ConfigureAwait(false);
        }

        /// <summary>
        /// 接收到来自客户端关闭链接请求
        /// </summary>
        /// <param name="webSocketConnection"></param>
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        private async void SocketOnCloseAsync(IWebSocketConnection webSocketConnection){
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnCloseAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"断开链接");
            if(this.WebSocketConnectionWrapDictionary.Count<1 || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)){return;}
            this.WebSocketConnectionWrapDictionary.Remove(webSocketConnection.ConnectionInfo.Id);
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnCloseAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"已移出会话列表");
        }

        /// <summary>
        /// 接收到来自客户端异常
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="exception"></param>
        private async void SocketOnErrorAsync(IWebSocketConnection webSocketConnection,Exception exception) {
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnErrorAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"链接异常,{exception.Message},{exception.StackTrace}");
            if(this.WebSocketConnectionWrapDictionary.Count<1 || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)){return;}
            Entities.WebSocketConnectionWrap webSocketConnectionWrap =this.WebSocketConnectionWrapDictionary[webSocketConnection.ConnectionInfo.Id];
            if(!webSocketConnectionWrap.Valid) {
                webSocketConnection.Close();
            } else {
                await webSocketConnection.SendPing(this.PingPacket).ConfigureAwait(false);
            }
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnErrorAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"链接异常已随便处理");
        }

        /// <summary>
        /// 接收到来自客户端ping请求
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="bytes"></param>
#pragma warning disable CA1822 //不访问实例数据，可标记为 static
        private async void SocketOnPingAsync(IWebSocketConnection webSocketConnection,Byte[] bytes){
#pragma warning restore CA1822 //不访问实例数据，可标记为 static
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnPingAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求Ping");
            await webSocketConnection.SendPong(bytes).ConfigureAwait(false);
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnPingAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求Ping已回应");
        }

        /// <summary>
        /// 接收到来自客户端pong响应
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="bytes"></param>
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        private async void SocketOnPongAsync(IWebSocketConnection webSocketConnection,Byte[] bytes) {
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
            if(bytes==null){return;}
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnPongAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"回复Pong");
            if(this.WebSocketConnectionWrapDictionary.Count<1 || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)){return;}
            this.WebSocketConnectionWrapDictionary[webSocketConnection.ConnectionInfo.Id].LastPongTime=DateTimeOffset.Now.ToUnixTimeSeconds();
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnPongAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"回复Pong,已更新最后Pong时间戳");
        }

        /// <summary>
        /// 接收到来自客户端的字节数组消息
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="bytes"></param>
        private async void SocketOnBinaryAsync(IWebSocketConnection webSocketConnection,Byte[] bytes) {
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"发来字节数组消息,共{bytes.Length}字节");
            Protocol.WebSocketPacketTest packetTest=Protocol.WebSocketPacketTest.Parser.ParseFrom(bytes);
            if(packetTest==null) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"发来无效消息字节数组消息");
                await webSocketConnection.Send(bytes).ConfigureAwait(false);
                return;
            }
            switch(packetTest.Type){
                //客户端向服务端请求验证ControlKey
                case 1002:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" WebSocketClientRequestValidateControlKey");
                    this.SocketOnBinaryWebSocketClientRequestValidateControlKeyAsync(webSocketConnection,Protocol.WebSocketClientRequestValidateControlKey.Parser.ParseFrom(bytes));
                    break;
                //客户端向服务端请求Daemon元数据
                case 1003:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" WebSocketClientRequestFetchDaemonMeta");
                    this.SocketOnBinaryWebSocketClientRequestFetchDaemonMetaAsync(webSocketConnection,Protocol.WebSocketClientRequestFetchDaemonMeta.Parser.ParseFrom(bytes));
                    break;
                //客户端向服务端请求Daemon状态
                case 1004:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" WebSocketClientRequestFetchDaemonStatus");
                    this.SocketOnBinaryWebSocketClientRequestFetchDaemonStatusAsync(webSocketConnection,Protocol.WebSocketClientRequestFetchDaemonStatus.Parser.ParseFrom(bytes));
                    break;
                //客户端向服务端请求所有单元状态
                case 1005:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" WebSocketClientRequestFetchUnitsStatus");
                    this.SocketOnBinaryWebSocketClientRequestFetchUnitsStatusAsync(webSocketConnection,Protocol.WebSocketClientRequestFetchUnitsStatus.Parser.ParseFrom(bytes));
                    break;
                //客户端向服务端请求指定单元状态
                case 1006:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" WebSocketClientRequestFetchUnitStatus");
                    this.SocketOnBinaryWebSocketClientRequestFetchUnitStatusAsync(webSocketConnection,Protocol.WebSocketClientRequestFetchUnitStatus.Parser.ParseFrom(bytes));
                    break;
                //客户端向服务端请求重载所有单元配置
                case 1007:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" WebSocketClientRequestReloadUnitsSettings");
                    this.SocketOnBinaryWebSocketClientRequestReloadUnitsSettingsAsync(webSocketConnection,Protocol.WebSocketClientRequestReloadUnitsSettings.Parser.ParseFrom(bytes));
                    break;
                //客户端向服务端请求重载指定单元配置
                case 1008:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" WebSocketClientRequestReloadUnitSettings");
                    this.SocketOnBinaryWebSocketClientRequestReloadUnitSettingsAsync(webSocketConnection,Protocol.WebSocketClientRequestReloadUnitSettings.Parser.ParseFrom(bytes));
                    break;
                //客户端向服务端请求启动所有单元
                case 1009:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" WebSocketClientRequestStartUnits");
                    this.SocketOnBinaryWebSocketClientRequestStartUnitsAsync(webSocketConnection,Protocol.WebSocketClientRequestStartUnits.Parser.ParseFrom(bytes));
                    break;
                //客户端向服务端请求启动指定单元
                case 1010:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" WebSocketClientRequestStartUnit");
                    this.SocketOnBinaryWebSocketClientRequestStartUnitAsync(webSocketConnection,Protocol.WebSocketClientRequestStartUnit.Parser.ParseFrom(bytes));
                    break;
                //客户端向服务端请求停止所有单元
                case 1011:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" WebSocketClientRequestStopUnits");
                    this.SocketOnBinaryWebSocketClientRequestStopUnitsAsync(webSocketConnection,Protocol.WebSocketClientRequestStopUnits.Parser.ParseFrom(bytes));
                    break;
                //客户端向服务端请求停止指定单元
                case 1012:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" WebSocketClientRequestStopUnit");
                    this.SocketOnBinaryWebSocketClientRequestStopUnitAsync(webSocketConnection,Protocol.WebSocketClientRequestStopUnit.Parser.ParseFrom(bytes));
                    break;
                //客户端向服务端请求指定单元网络数据
                case 1018:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" WebSocketClientRequestStopUnit");
                    this.SocketOnBinaryWebSocketClientRequestFetchUnitStatusNetworkCounterAsync(webSocketConnection,
                        Protocol.WebSocketClientRequestFetchUnitStatusNetworkCounter.Parser.ParseFrom(bytes));
                    break;
                default:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"发来无法识别消息字节数组消息");
                    await webSocketConnection.Send(bytes).ConfigureAwait(false);
                    break;
            }
        }

        /// <summary>
        /// 客户端向服务端请求验证ControlKey
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="packet"></param>
        private async void SocketOnBinaryWebSocketClientRequestValidateControlKeyAsync(IWebSocketConnection webSocketConnection,Protocol.WebSocketClientRequestValidateControlKey packetRequest) {
            if(packetRequest==null){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestValidateControlKey[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的数据包为空");
                webSocketConnection.Close();
                return;
            }
            if(packetRequest.ClientConnectionGuid!=webSocketConnection.ConnectionInfo.Id.ToString() || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestValidateControlKey[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的会话Id不匹配");
                webSocketConnection.Close();
                return;
            }
            if(String.IsNullOrWhiteSpace(packetRequest.ControlKey)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestValidateControlKey[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的<ControlKey>为空");
                webSocketConnection.Close();
                return;
            }
            if(packetRequest.ControlKey!=Program.AppSettingsModule.AppSettings.ControlKey) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestValidateControlKey[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的<ControlKey>不匹配");
                webSocketConnection.Close();
                return;
            }
            Entities.WebSocketConnectionWrap webSocketConnectionWrap =this.WebSocketConnectionWrapDictionary[webSocketConnection.ConnectionInfo.Id];
            webSocketConnectionWrap.Valid=true;
            webSocketConnectionWrap.LastPongTime=DateTimeOffset.Now.ToUnixTimeSeconds();
            //回复客户端已通过验证
            Protocol.WebSocketServerResponseValidateControlKey packetResponse=new Protocol.WebSocketServerResponseValidateControlKey{
                Type=2002,
                ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),
                Validation=webSocketConnectionWrap.Valid,
                ValidationMessage="your connection is validated"};
            await webSocketConnection.Send(packetResponse.ToBytes<Protocol.WebSocketServerResponseValidateControlKey>()).ConfigureAwait(false);
        }

        /// <summary>
        /// 客户端向服务端请求Daemon元数据
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="packet"></param>
        private async void SocketOnBinaryWebSocketClientRequestFetchDaemonMetaAsync(IWebSocketConnection webSocketConnection,Protocol.WebSocketClientRequestFetchDaemonMeta packetRequest) {
            if(packetRequest==null){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestFetchDaemonMetaAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的数据包为空");
                webSocketConnection.Close();
                return;
            }
            if(packetRequest.ClientConnectionGuid!=webSocketConnection.ConnectionInfo.Id.ToString() || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestFetchDaemonMetaAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的会话Id不匹配");
                webSocketConnection.Close();
                return;
            }
            Protocol.DaemonMeta packetDaemonMeta=new Protocol.DaemonMeta{
                Version=Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                HostCpuCores=(UInt32)Environment.ProcessorCount,
                HostMemorySize=(UInt64)WindowsManagementHelper.GetPhysicalMemorySize(),
                WorkDirectory=Program.AppEnvironment.BaseDirectory,
                ProcessId=(UInt32)Program.AppProcess.Id};
            Protocol.WebSocketServerResponseFetchDaemonMeta packetResponse=new Protocol.WebSocketServerResponseFetchDaemonMeta{
                Type=2003,ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),DaemonMeta=packetDaemonMeta};
            await webSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
        }

        /// <summary>
        /// 客户端向服务端请求Daemon状态
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="packetRequest"></param>
        private async void SocketOnBinaryWebSocketClientRequestFetchDaemonStatusAsync(IWebSocketConnection webSocketConnection,Protocol.WebSocketClientRequestFetchDaemonStatus packetRequest){
            if(packetRequest==null){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestFetchDaemonStatusAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的数据包为空");
                webSocketConnection.Close();
                return;
            }
            if(packetRequest.ClientConnectionGuid!=webSocketConnection.ConnectionInfo.Id.ToString() || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestFetchDaemonStatusAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的会话Id不匹配");
                webSocketConnection.Close();
                return;
            }
            Protocol.DaemonStatus packetDaemonStatus=new Protocol.DaemonStatus{
                ProcessTimePercentage=Program.AppPerformanceCounterModule.GetProcessTimePercentage()/Environment.ProcessorCount,
                ProcessWorkingSetSize=(UInt64)Program.AppPerformanceCounterModule.GetProcessWorkingSetSize(),
                UnitSettingsCount=(UInt32)Program.UnitControlModule.GetUnitSettingsCount(),
                UnitProcessCount=(UInt32)Program.UnitControlModule.GetUnitProcessCount(),
                NetworkTotalSent=0,
                NetworkTotalReceived=0};
            Entities.UnitNetworkCounter unitNetworkCounter=Program.UnitNetworkPerformanceTracerModule.GetCounter(Program.AppProcess.Id);
            if(unitNetworkCounter!=null) {
                packetDaemonStatus.NetworkTotalSent=(UInt64)unitNetworkCounter.TotalSent;
                packetDaemonStatus.NetworkTotalReceived=(UInt64)unitNetworkCounter.TotalReceived;
            }
            Protocol.WebSocketServerResponseFetchDaemonStatus packetResponse=new Protocol.WebSocketServerResponseFetchDaemonStatus{
                Type=2004,ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),DaemonStatus=packetDaemonStatus};
            await webSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
        }

        /// <summary>
        /// 客户端向服务端请求所有单元状态
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="packetRequest"></param>
        private async void SocketOnBinaryWebSocketClientRequestFetchUnitsStatusAsync(IWebSocketConnection webSocketConnection,Protocol.WebSocketClientRequestFetchUnitsStatus packetRequest){
            if(packetRequest==null){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestFetchUnitsStatusAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的数据包为空");
                webSocketConnection.Close();
                return;
            }
            if(packetRequest.ClientConnectionGuid!=webSocketConnection.ConnectionInfo.Id.ToString() || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestFetchUnitsStatusAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的会话Id不匹配");
                webSocketConnection.Close();
                return;
            }
            List<Entities.UnitStatus> unitStatusList=Program.UnitControlModule.FetchAllUnitsStatus();
            if(unitStatusList==null || unitStatusList.Count<1) {return;}
            Protocol.WebSocketServerResponseFetchUnitsStatus packetResponse=new Protocol.WebSocketServerResponseFetchUnitsStatus {
                Type=2005,ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString()};
            foreach(Entities.UnitStatus item in unitStatusList) {
                Protocol.UnitStatus unitStatus=new Protocol.UnitStatus{UnitName=item.UnitName};
                //单元配置
                unitStatus.UnitSettings=new Protocol.UnitSettings{
                    Name=item.UnitSettings.Name,
                    Description=item.UnitSettings.Description,
                    ExecuteAbsolutePath=item.UnitSettings.ExecuteAbsolutePath,
                    WorkAbsoluteDirectory=item.UnitSettings.WorkAbsoluteDirectory,
                    ExecuteParams=item.UnitSettings.ExecuteParams??String.Empty,
                    AutoStart=item.UnitSettings.AutoStart,
                    AutoStartDelay=(UInt32)item.UnitSettings.AutoStartDelay,
                    DaemonProcess=item.UnitSettings.DaemonProcess,
                    HaveChildProcesses=item.UnitSettings.HaveChildProcesses,
                    FetchNetworkUsage=item.UnitSettings.FetchNetworkUsage};
                //单元进程
                unitStatus.UnitProcess=new Protocol.UnitProcess{Name=item.UnitSettings.Name,State=1,ProcessId=0};
                if(item.UnitProcess!=null) {
                    unitStatus.UnitProcess.State=(UInt32)item.UnitProcess.State;
                    unitStatus.UnitProcess.ProcessId=(UInt32)item.UnitProcess.ProcessId;
                }
                //单元网络
                unitStatus.UnitNetworkCounter=new Protocol.UnitNetworkCounter{Name=item.UnitSettings.Name,TotalSent=0,TotalReceived=0,SendSpeed=0,ReceiveSpeed=0};
                if(item.UnitProcess!=null && item.UnitSettings.FetchNetworkUsage) {
                    item.UnitNetworkCounter=Program.UnitNetworkPerformanceTracerModule.GetCounter(item.UnitProcess.ProcessId);
                    if(item.UnitNetworkCounter==null) {
                        unitStatus.UnitNetworkCounter=new Protocol.UnitNetworkCounter{
                        Name=item.UnitSettings.Name,
                        TotalSent=(UInt64)item.UnitNetworkCounter.TotalSent+4096,
                        TotalReceived=(UInt64)item.UnitNetworkCounter.TotalReceived+4096,
                        SendSpeed=(UInt64)item.UnitNetworkCounter.SendSpeed+4096,
                        ReceiveSpeed=(UInt64)item.UnitNetworkCounter.ReceiveSpeed+4096};
                    }
                }
                //添加到列表
                packetResponse.UnitStatus.Add(unitStatus);
            }
            await webSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
        }

        /// <summary>
        /// 客户端向服务端请求指定单元状态
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="packetRequest"></param>
        private async void SocketOnBinaryWebSocketClientRequestFetchUnitStatusAsync(IWebSocketConnection webSocketConnection,Protocol.WebSocketClientRequestFetchUnitStatus packetRequest){
            if(packetRequest==null){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestFetchUnitStatusAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的数据包为空");
                webSocketConnection.Close();
                return;
            }
            if(packetRequest.ClientConnectionGuid!=webSocketConnection.ConnectionInfo.Id.ToString() || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestFetchUnitStatusAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的会话Id不匹配");
                webSocketConnection.Close();
                return;
            }
            Entities.UnitStatus item=Program.UnitControlModule.FetchUnitStatus(packetRequest.UnitName);
            if(item==null){return;}
            Protocol.WebSocketServerResponseFetchUnitStatus packetResponse=new Protocol.WebSocketServerResponseFetchUnitStatus {
                Type=2006,ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),UnitName=packetRequest.UnitName};
            Protocol.UnitStatus unitStatus=new Protocol.UnitStatus{UnitName=item.UnitName};
            //单元配置
            unitStatus.UnitSettings=new Protocol.UnitSettings{
                Name=item.UnitSettings.Name,
                Description=item.UnitSettings.Description,
                ExecuteAbsolutePath=item.UnitSettings.ExecuteAbsolutePath,
                WorkAbsoluteDirectory=item.UnitSettings.WorkAbsoluteDirectory,
                ExecuteParams=item.UnitSettings.ExecuteParams??String.Empty,
                AutoStart=item.UnitSettings.AutoStart,
                AutoStartDelay=(UInt32)item.UnitSettings.AutoStartDelay,
                DaemonProcess=item.UnitSettings.DaemonProcess,
                HaveChildProcesses=item.UnitSettings.HaveChildProcesses,
                FetchNetworkUsage=item.UnitSettings.FetchNetworkUsage};
            //单元进程
            unitStatus.UnitProcess=new Protocol.UnitProcess{Name=item.UnitSettings.Name,State=1,ProcessId=0};
            if(item.UnitProcess!=null) {
                unitStatus.UnitProcess.State=(UInt32)item.UnitProcess.State;
                unitStatus.UnitProcess.ProcessId=(UInt32)item.UnitProcess.ProcessId;
            }
            //单元网络
            unitStatus.UnitNetworkCounter=new Protocol.UnitNetworkCounter{Name=item.UnitSettings.Name,TotalSent=0,TotalReceived=0,SendSpeed=0,ReceiveSpeed=0};
            if(item.UnitProcess!=null && item.UnitSettings.FetchNetworkUsage) {
                item.UnitNetworkCounter=Program.UnitNetworkPerformanceTracerModule.GetCounter(item.UnitProcess.ProcessId);
                if(item.UnitNetworkCounter!=null) {
                    unitStatus.UnitNetworkCounter=new Protocol.UnitNetworkCounter{
                        Name=item.UnitSettings.Name,
                        TotalSent=(UInt64)item.UnitNetworkCounter.TotalSent,
                        TotalReceived=(UInt64)item.UnitNetworkCounter.TotalReceived,
                        SendSpeed=(UInt64)item.UnitNetworkCounter.SendSpeed,
                        ReceiveSpeed=(UInt64)item.UnitNetworkCounter.ReceiveSpeed};
                }
            }
            //
            packetResponse.UnitStatus=unitStatus;
            await webSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
        }

        /// <summary>
        /// 客户端向服务端请求重载所有单元配置
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="packetRequest"></param>
        private async void SocketOnBinaryWebSocketClientRequestReloadUnitsSettingsAsync(IWebSocketConnection webSocketConnection,Protocol.WebSocketClientRequestReloadUnitsSettings packetRequest){
            if(packetRequest==null){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestReloadUnitsSettingsAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的数据包为空");
                webSocketConnection.Close();
                return;
            }
            if(packetRequest.ClientConnectionGuid!=webSocketConnection.ConnectionInfo.Id.ToString() || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestReloadUnitsSettingsAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的会话Id不匹配");
                webSocketConnection.Close();
                return;
            }
            Program.UnitControlModule.ReloadAllUnits(packetRequest.RestartIfUpdate);
            Protocol.WebSocketServerResponseReloadUnitsSettings packetResponse=new Protocol.WebSocketServerResponseReloadUnitsSettings{
                Type=2007,ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),RestartIfUpdate=packetRequest.RestartIfUpdate,Executed=true};
            await webSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
        }

        /// <summary>
        /// 客户端向服务端请求重载指定单元配置
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="packetRequest"></param>
        private async void SocketOnBinaryWebSocketClientRequestReloadUnitSettingsAsync(IWebSocketConnection webSocketConnection,Protocol.WebSocketClientRequestReloadUnitSettings packetRequest){
            if(packetRequest==null){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestReloadUnitSettingsAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的数据包为空");
                webSocketConnection.Close();
                return;
            }
            if(packetRequest.ClientConnectionGuid!=webSocketConnection.ConnectionInfo.Id.ToString() || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestReloadUnitSettingsAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的会话Id不匹配");
                webSocketConnection.Close();
                return;
            }
            Program.UnitControlModule.ReloadUnit(packetRequest.UnitName,packetRequest.RestartIfUpdate);
            Protocol.WebSocketServerResponseReloadUnitSettings packetResponse=new Protocol.WebSocketServerResponseReloadUnitSettings{
                Type=2008,ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),RestartIfUpdate=packetRequest.RestartIfUpdate,Executed=true,UnitName=packetRequest.UnitName};
            await webSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
        }

        /// <summary>
        /// 客户端向服务端请求启动所有单元
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="packetRequest"></param>
        private async void SocketOnBinaryWebSocketClientRequestStartUnitsAsync(IWebSocketConnection webSocketConnection,Protocol.WebSocketClientRequestStartUnits packetRequest){
            if(packetRequest==null){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestStartUnitsAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的数据包为空");
                webSocketConnection.Close();
                return;
            }
            if(packetRequest.ClientConnectionGuid!=webSocketConnection.ConnectionInfo.Id.ToString() || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestStartUnitsAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的会话Id不匹配");
                webSocketConnection.Close();
                return;
            }
            Program.UnitControlModule.StartAllUnits();
            Protocol.WebSocketServerResponseStartUnits packetResponse=new Protocol.WebSocketServerResponseStartUnits{
                Type=2009,ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),Executed=true};
            await webSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
        }

        /// <summary>
        /// 客户端向服务端请求启动指定单元
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="packetRequest"></param>
        private async void SocketOnBinaryWebSocketClientRequestStartUnitAsync(IWebSocketConnection webSocketConnection,Protocol.WebSocketClientRequestStartUnit packetRequest) {
            if(packetRequest==null){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestStartUnitAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的数据包为空");
                webSocketConnection.Close();
                return;
            }
            if(packetRequest.ClientConnectionGuid!=webSocketConnection.ConnectionInfo.Id.ToString() || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestStartUnitAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的会话Id不匹配");
                webSocketConnection.Close();
                return;
            }
            Program.UnitControlModule.StartUnit(packetRequest.UnitName);
            Protocol.WebSocketServerResponseStartUnit packetResponse=new Protocol.WebSocketServerResponseStartUnit{
                Type=2010,ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),UnitName=packetRequest.UnitName,Executed=true};
            await webSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
        }

        /// <summary>
        /// 客户端向服务端请求停止所有单元
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="packetRequest"></param>
        private async void SocketOnBinaryWebSocketClientRequestStopUnitsAsync(IWebSocketConnection webSocketConnection,Protocol.WebSocketClientRequestStopUnits packetRequest) {
            if(packetRequest==null){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestStopUnitsAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的数据包为空");
                webSocketConnection.Close();
                return;
            }
            if(packetRequest.ClientConnectionGuid!=webSocketConnection.ConnectionInfo.Id.ToString() || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestStopUnitsAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的会话Id不匹配");
                webSocketConnection.Close();
                return;
            }
            Program.UnitControlModule.StopAllUnits(false);
            Protocol.WebSocketServerResponseStopUnits packetResponse=new Protocol.WebSocketServerResponseStopUnits{
                Type=2011,ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),Executed=true};
            await webSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
        }

        /// <summary>
        /// 客户端向服务端请求停止指定单元
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="packetRequest"></param>
        private async void SocketOnBinaryWebSocketClientRequestStopUnitAsync(IWebSocketConnection webSocketConnection,Protocol.WebSocketClientRequestStopUnit packetRequest) {
            if(packetRequest==null){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestStopUnitAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的数据包为空");
                webSocketConnection.Close();
                return;
            }
            if(packetRequest.ClientConnectionGuid!=webSocketConnection.ConnectionInfo.Id.ToString() || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestStopUnitAsync[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的会话Id不匹配");
                webSocketConnection.Close();
                return;
            }
            Program.UnitControlModule.StopUnit(packetRequest.UnitName,false);
            Protocol.WebSocketServerResponseStopUnit packetResponse=new Protocol.WebSocketServerResponseStopUnit{
                Type=2012,ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),UnitName=packetRequest.UnitName,Executed=true};
            await webSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
        }
        
        /// <summary>
        /// 服务端通知所有客户端指定单元被重载
        /// </summary>
        /// <param name="unitName"></param>
        /// <param name="unitSettings"></param>
        public async void SocketSendBinaryWebSocketServerNotifyClientsThatUnitSettingsReloadAsync(String unitName,Entities.UnitSettings unitSettings){
            const String infoLogPrefix="Modules.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitSettingsReloadAsync";
            Program.LoggerModule.Log(infoLogPrefix,"服务端通知所有客户端指定单元被重载");
            if(this.WebSocketConnectionWrapDictionary.Count<1){
                Program.LoggerModule.Log(infoLogPrefix,"没有需要通知的客户端");
                return;
            }
            if(unitSettings==null){
                Program.LoggerModule.Log(infoLogPrefix,"unitSettings 为空,跳过通知");
                return;
            }
            Protocol.UnitSettings packetUnitSettings=new Protocol.UnitSettings{
                Name=unitSettings.Name,
                Description=unitSettings.Description,
                ExecuteAbsolutePath=unitSettings.ExecuteAbsolutePath,
                WorkAbsoluteDirectory=unitSettings.WorkAbsoluteDirectory,
                ExecuteParams=unitSettings.ExecuteParams??String.Empty,
                AutoStart=unitSettings.AutoStart,
                AutoStartDelay=(UInt32)unitSettings.AutoStartDelay,
                DaemonProcess=unitSettings.DaemonProcess,
                HaveChildProcesses=unitSettings.HaveChildProcesses,
                FetchNetworkUsage=unitSettings.FetchNetworkUsage};
            Protocol.WebSocketServerNotifyClientsThatUnitSettingsReload packetResponse=new Protocol.WebSocketServerNotifyClientsThatUnitSettingsReload{Type=2013,UnitName=unitName,UnitSettings=packetUnitSettings};
            foreach(KeyValuePair<Guid,Entities.WebSocketConnectionWrap> item in this.WebSocketConnectionWrapDictionary) {
                if(!item.Value.Valid || !item.Value.WebSocketConnection.IsAvailable){continue;}
                packetResponse.ClientConnectionGuid=item.Value.WebSocketConnection.ConnectionInfo.Id.ToString();
                await item.Value.WebSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 服务端通知所有客户端指定单元已启动
        /// </summary>
        /// <param name="unitName"></param>
        /// <param name="unitProcess"></param>
        public async void SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartedAsync(String unitName,Entities.UnitProcess unitProcess){
            const String infoLogPrefix="Modules.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartedAsync";
            Program.LoggerModule.Log(infoLogPrefix,"服务端通知所有客户端指定单元已启动");
            if(this.WebSocketConnectionWrapDictionary.Count<1){
                Program.LoggerModule.Log(infoLogPrefix,"没有需要通知的客户端");
                return;
            }
            if(unitProcess==null) {
                Program.LoggerModule.Log(infoLogPrefix,"unitProcess 为空,跳过通知");
                return;
            }
            Protocol.UnitProcess packetUnitProcess=new Protocol.UnitProcess{
                Name=unitProcess.Name,
                State=(UInt32)unitProcess.State,
                ProcessId=(UInt32)unitProcess.ProcessId};
            Protocol.WebSocketServerNotifyClientsThatUnitStarted packetResponse=new Protocol.WebSocketServerNotifyClientsThatUnitStarted{Type=2014,UnitName=unitName,UnitProcess=packetUnitProcess};
            foreach(KeyValuePair<Guid,Entities.WebSocketConnectionWrap> item in this.WebSocketConnectionWrapDictionary) {
                if(!item.Value.Valid || !item.Value.WebSocketConnection.IsAvailable){continue;}
                packetResponse.ClientConnectionGuid=item.Value.WebSocketConnection.ConnectionInfo.Id.ToString();
                await item.Value.WebSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 服务端通知所有客户端指定单元已停止
        /// </summary>
        /// <param name="unitName"></param>
        public async void SocketSendBinaryWebSocketServerNotifyClientsThatUnitStoppedAsync(String unitName){
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStoppedAsync","服务端通知所有客户端指定单元已停止");
            if(this.WebSocketConnectionWrapDictionary.Count<1){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStoppedAsync","没有需要通知的客户端");
                return;
            }
            Protocol.WebSocketServerNotifyClientsThatUnitStopped packetResponse=new Protocol.WebSocketServerNotifyClientsThatUnitStopped{Type=2015,UnitName=unitName};
            foreach(KeyValuePair<Guid,Entities.WebSocketConnectionWrap> item in this.WebSocketConnectionWrapDictionary) {
                if(!item.Value.Valid || !item.Value.WebSocketConnection.IsAvailable){continue;}
                packetResponse.ClientConnectionGuid=item.Value.WebSocketConnection.ConnectionInfo.Id.ToString();
                await item.Value.WebSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 服务端通知所有客户端指定单元启动失败
        /// </summary>
        /// <param name="unitName"></param>
        public async void SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartFailedAsync(String unitName){
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartFailedAsync","服务端通知所有客户端指定单元启动失败");
            if(this.WebSocketConnectionWrapDictionary.Count<1){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartFailedAsync","没有需要通知的客户端");
                return;
            }
            Protocol.WebSocketServerNotifyClientsThatUnitStartFailed packetResponse=new Protocol.WebSocketServerNotifyClientsThatUnitStartFailed{Type=2016,UnitName=unitName};
            foreach(KeyValuePair<Guid,Entities.WebSocketConnectionWrap> item in this.WebSocketConnectionWrapDictionary) {
                if(!item.Value.Valid || !item.Value.WebSocketConnection.IsAvailable){continue;}
                packetResponse.ClientConnectionGuid=item.Value.WebSocketConnection.ConnectionInfo.Id.ToString();
                await item.Value.WebSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 服务端通知所有客户端指定单元停止失败
        /// </summary>
        /// <param name="unitName"></param>
        public async void SocketSendBinaryWebSocketServerNotifyClientsThatUnitStopFailedAsync(String unitName){
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStopFailedAsync","服务端通知所有客户端指定单元停止失败");
            if(this.WebSocketConnectionWrapDictionary.Count<1){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStopFailedAsync","没有需要通知的客户端");
                return;
            }
            Protocol.WebSocketServerNotifyClientsThatUnitStopFailed packetResponse=new Protocol.WebSocketServerNotifyClientsThatUnitStopFailed{Type=2015,UnitName=unitName};
            foreach(KeyValuePair<Guid,Entities.WebSocketConnectionWrap> item in this.WebSocketConnectionWrapDictionary) {
                if(!item.Value.Valid || !item.Value.WebSocketConnection.IsAvailable){continue;}
                packetResponse.ClientConnectionGuid=item.Value.WebSocketConnection.ConnectionInfo.Id.ToString();
                await item.Value.WebSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 服务端通知客户端指定单元网络数据
        /// </summary>
        /// <param name="unitName"></param>
        private async void SocketOnBinaryWebSocketClientRequestFetchUnitStatusNetworkCounterAsync(IWebSocketConnection webSocketConnection,
            Protocol.WebSocketClientRequestFetchUnitStatusNetworkCounter packetRequest){
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestFetchUnitStatusNetworkCounterAsync","服务端通知所有客户端指定单元网络数据");
            if(this.WebSocketConnectionWrapDictionary.Count<1){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestFetchUnitStatusNetworkCounterAsync","没有需要通知的客户端");
                return;
            }
            if(packetRequest==null || String.IsNullOrEmpty(packetRequest.UnitName)){return;}
            Entities.UnitStatus unitStatus=Program.UnitControlModule.FetchUnitStatus(packetRequest.UnitName);
            if(unitStatus==null || unitStatus.UnitProcess==null){return;}
            Entities.UnitNetworkCounter unitNetworkCounter=Program.UnitNetworkPerformanceTracerModule.GetCounter(unitStatus.UnitProcess.ProcessId);
            if(unitNetworkCounter==null){return;}
            Protocol.WebSocketServerResponseFetchUnitStatusNetworkCounter packetResponse=new Protocol.WebSocketServerResponseFetchUnitStatusNetworkCounter{Type=2018,UnitName=packetRequest.UnitName};
            packetResponse.UnitNetworkCounter=new Protocol.UnitNetworkCounter{
                Name=packetRequest.UnitName,
                TotalSent=(UInt64)unitNetworkCounter.TotalSent,
                TotalReceived=(UInt64)unitNetworkCounter.TotalReceived,
                SendSpeed=(UInt64)unitNetworkCounter.SendSpeed,
                ReceiveSpeed=(UInt64)unitNetworkCounter.ReceiveSpeed};
            /*
            foreach(KeyValuePair<Guid,Entities.WebSocketConnectionWrap> item in this.WebSocketConnectionWrapDictionary) {
                if(!item.Value.Valid || !item.Value.WebSocketConnection.IsAvailable){continue;}
                packetResponse.ClientConnectionGuid=item.Value.WebSocketConnection.ConnectionInfo.Id.ToString();
                await item.Value.WebSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
            }
            */
            await webSocketConnection.Send(packetResponse.ToBytes()).ConfigureAwait(false);
        }
    }
}