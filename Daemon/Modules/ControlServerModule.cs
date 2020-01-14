using Daemon.Entities;
using Daemon.Helpers;
using Fleck;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Daemon.Modules {
    public class ControlServerModule:IDisposable {
        private readonly WebSocketServer WebSocketServer;
        private readonly Byte[] PingPacket=new Byte[]{0x07,0x03,0x05,0x05,0x06,0x00,0x08};
        private readonly Timer PingTimer;
        private Boolean PingTimerEnable=true;
        private readonly Timer CleanTimer;
        private Boolean CleanTimerEnable=true;
        private Dictionary<Guid,WebSocketConnectionWrap> WebSocketConnectionWrapDictionary=new Dictionary<Guid, WebSocketConnectionWrap>();

        public ControlServerModule(Int16 port,String address){
            if(!this.CheckAddress(address)){
                Program.LoggerModule.Log("Modules.ControlServerModule.DameonControlServerModule[Error]","取消创建WebSocketServer,参数错误");
                return;
            }
            if(address=="localhost"){address="127.0.0.1";}
            if(address=="any"){address="0.0.0.0";}
            Program.LoggerModule.Log("Modules.ControlServerModule.DameonControlServerModule","尝试创建WebSocketServer");
            try {
                this.WebSocketServer=new WebSocketServer($"ws://{address}:{port}",false);
                //this.WebSocketServer.ListenerSocket.NoDelay=true;
            }catch(Exception exception){
                Console.WriteLine($"Modules.ControlServerModule.DameonControlServerModule[Error] => 创建WebSocketServer异常 | {exception.Message} | {exception.StackTrace}");
                Program.LoggerModule.Log("Modules.ControlServerModule.DameonControlServerModule[Error]",$"创建WebSocketServer异常,{exception.Message},{exception.StackTrace}");
            }
            this.PingTimer=new Timer(OnPingTimerCallback,null,10_000,10_000);
            this.CleanTimer=new Timer(OnCleanTimerCallback,null,60_000,60_000);//timeout 60s
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)。
                    if(this.WebSocketServer!=null){this.WebSocketServer.Dispose();}
                    if(this.PingTimer!=null){this.PingTimer.Dispose();}
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
            foreach(KeyValuePair<Guid,WebSocketConnectionWrap> item in this.WebSocketConnectionWrapDictionary){
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
            foreach(KeyValuePair<Guid,WebSocketConnectionWrap> item in this.WebSocketConnectionWrapDictionary){
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
        public void StartServer(){
            if(this.WebSocketServer==null){return;}
            this.WebSocketServer.Start((webSocketConnection)=>{
                webSocketConnection.OnOpen=()=>this.SocketOnOpenAsync(webSocketConnection);
                webSocketConnection.OnClose=()=>this.SocketOnCloseAsync(webSocketConnection);
                webSocketConnection.OnError=(exception)=>this.SocketOnErrorAsync(webSocketConnection,exception);
                webSocketConnection.OnPing=(bytes)=>this.SocketOnPingAsync(webSocketConnection,bytes);
                webSocketConnection.OnPong=(bytes)=>this.SocketOnPongAsync(webSocketConnection,bytes);
                //webSocketConnection.OnMessage=(message)=>this.SocketOnMessageAsync(webSocketConnection,message);
                webSocketConnection.OnMessage=(message) => {
                    webSocketConnection.Send(message);
                    Console.WriteLine($"客户端\"{webSocketConnection.ConnectionInfo.Id}\"发来文本消息,已做Echo处理");
                    Program.LoggerModule.Log("Modules.ControlServerModule.StartServer",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"发来文本消息,已做Echo处理");
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
            WebSocketConnectionWrap webSocketConnectionWrap=new WebSocketConnectionWrap{
                WebSocketConnectionId=webSocketConnection.ConnectionInfo.Id,
                WebSocketConnection=webSocketConnection};
            this.WebSocketConnectionWrapDictionary.Add(webSocketConnection.ConnectionInfo.Id,webSocketConnectionWrap);
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnOpenAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"允许链接并加入临时列表");
            Protocol.WebSocketServerResponseAfterOnOpen protobuf=new Protocol.WebSocketServerResponseAfterOnOpen{
                Type=2001,
                ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),
                HelloMessage=$"{webSocketConnection.ConnectionInfo.ClientIpAddress}:{webSocketConnection.ConnectionInfo.ClientPort} websocket connected,please validate your control key"};
            await webSocketConnection.Send(protobuf.ToBytes());
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
            WebSocketConnectionWrap webSocketConnectionWrap=this.WebSocketConnectionWrapDictionary[webSocketConnection.ConnectionInfo.Id];
            if(!webSocketConnectionWrap.Valid) {
                webSocketConnection.Close();
            } else {
                await webSocketConnection.SendPing(this.PingPacket);
            }
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnErrorAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"链接异常已随便处理");
        }

        /// <summary>
        /// 接收到来自客户端ping请求
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="bytes"></param>
        private async void SocketOnPingAsync(IWebSocketConnection webSocketConnection,Byte[] bytes){
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnPingAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求Ping");
            await webSocketConnection.SendPong(bytes);
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
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnPongAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"回复Pong");
            if(this.WebSocketConnectionWrapDictionary.Count<1 || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)){return;}
            this.WebSocketConnectionWrapDictionary[webSocketConnection.ConnectionInfo.Id].LastPongTime=DateTimeOffset.Now.ToUnixTimeSeconds();
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnPongAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"回复Pong,已更新最后Pong时间戳");
        }

        /*
        /// <summary>
        /// 接收到来自客户端的文本消息
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="message"></param>
        private async void SocketOnMessageAsync(IWebSocketConnection webSocketConnection,String message) {
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"发来文本消息");
            //空消息
            if(String.IsNullOrWhiteSpace(message)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"发来空消息");
                return;
            }
            String[] args=message.Split(this.SplitChar);
            if(args.Length<2){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"发来空消息");
                return;
            }
            if(args[0]!=webSocketConnection.ConnectionInfo.Id.ToString()) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"Guid不一致");
                return;
            }
            //Action 验证 $"{ClientGuid}§CheckControlKey§{ControlKey}
            if(args[1]=="CheckControlKey"){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求验证");
                if(String.IsNullOrWhiteSpace(args[2])) {return;}
                if(args[2]==Program.AppSettings.ControlKey){
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"验证通过");
                    this.WebSocketConnectionDictionary.Add(webSocketConnection.ConnectionInfo.Id,webSocketConnection);
                    this.InvalidWebSocketConnectionDictionary.Remove(webSocketConnection.ConnectionInfo.Id);
                    await webSocketConnection.Send($"{args[0]}{this.SplitChar}NotifyCheckControlKey{this.SplitChar}success");
                } else {
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"验证失败,已被断开链接");
                    this.InvalidWebSocketConnectionDictionary.Remove(webSocketConnection.ConnectionInfo.Id);
                    await webSocketConnection.Send($"{args[0]}{this.SplitChar}NotifyCheckControlKey{this.SplitChar}failure");
                    webSocketConnection.Close();
                }
                return;
            }
            //Action 刷新所有单元 $"{ClientGuid}§ReloadAllUnits§{RestartIfUpdate}
            if(args[1]=="ReloadAllUnits"){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求刷新所有单元");
                if(String.IsNullOrWhiteSpace(args[2])) {return;}
                if(!this.WebSocketConnectionDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)){
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求刷新所有单元,未经验证已被拒绝");
                    return;
                }
                Program.UnitControlModule.ReloadAllUnits(args[2]!="false");
                return;
            }
            //Action 启动所有单元 $"{ClientGuid}§StartAllUnits
            if(args[1]=="StartAllUnits") {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求启动所有单元");
                if(!this.WebSocketConnectionDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)){
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求启动所有单元,未经验证已被拒绝");
                    return;
                }
                Program.UnitControlModule.StartAllUnits();
                return;
            }
            //Action 停止所有单元 $"{ClientGuid}§StopAllUnits
            if(args[1]=="StopAllUnits") {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求停止所有单元");
                if(!this.WebSocketConnectionDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)){
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求停止所有单元,未经验证已被拒绝");
                    return;
                }
                Program.UnitControlModule.StopAllUnits();
                return;
            }
            //Action 刷新单元 $"{ClientGuid}§ReloadUnit§{unitName}
            if(args[1]=="ReloadUnit") {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求刷新单元");
                if(!this.WebSocketConnectionDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)){
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求刷新单元,未经验证已被拒绝");
                    return;
                }
                if(args.Length<3 || args[2]==null) {
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求刷新单元,未提供参数已被拒绝");
                    return;
                }
                Program.UnitControlModule.ReloadUnit(args[2]);
                return;
            }
            //Action 启动单元 $"{ClientGuid}§StartUnit§{unitName}
            if(args[1]=="StartUnit") {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求启动单元");
                if(!this.WebSocketConnectionDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)){
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求启动单元,未经验证已被拒绝");
                    return;
                }
                if(args.Length<3 || args[2]==null) {
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求启动单元,未提供参数已被拒绝");
                    return;
                }
                Program.UnitControlModule.StartUnit(args[2]);
                return;
            }
            //Action 停止单元 $"{ClientGuid}§StopUnit§{unitName}
            if(args[1]=="StopUnit") {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求停止单元");
                if(!this.WebSocketConnectionDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)){
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求停止单元,未经验证已被拒绝");
                    return;
                }
                if(args.Length<3 || args[2]==null) {
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求停止单元,未提供参数已被拒绝");
                    return;
                }
                Program.UnitControlModule.StopUnit(args[2]);
                return;
            }
            //Action 获取所有单元的状态
            if(args[1]=="FetchAllUnitsStatus") {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求所有单元状态");
                if(!this.WebSocketConnectionDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)){
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求所有单元状态,未经验证已被拒绝");
                    return;
                }
                this.NotifyClientFetchAllUnitsStatusAsync(webSocketConnection.ConnectionInfo.Id,JsonConvert.SerializeObject(Program.UnitControlModule.FetchAllUnitsStatus()));
                return;
            }
            //Action 获取指定单元的状态
            if(args[1]=="FetchAllUnitsStatus") {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"获取指定单元状态");
                if(!this.WebSocketConnectionDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)){
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"获取指定单元状态,未经验证已被拒绝");
                    return;
                }
                if(args.Length<3 || args[2]==null) {
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"获取指定单元状态,未提供参数已被拒绝");
                    return;
                }
                this.NotifyClientFetchUnitStatusAsync(webSocketConnection.ConnectionInfo.Id,JsonConvert.SerializeObject(Program.UnitControlModule.FetchUnitStatus(args[2])));
                return;
            }
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnMessageAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求未识别,{message}");
        }
        */

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
                await webSocketConnection.Send(bytes);
                return;
            }
            switch(packetTest.Type){
                case 1002:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" WebSocketClientRequestValidateControlKey");
                    this.SocketOnBinaryWebSocketClientRequestValidateControlKeyAsync(webSocketConnection,Protocol.WebSocketClientRequestValidateControlKey.Parser.ParseFrom(bytes));
                    break;
                default:
                    Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryAsync[Warning]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"发来无法识别消息字节数组消息");
                    await webSocketConnection.Send(bytes);
                    break;
            }
        }

        /// <summary>
        /// 客户端向服务端请求验证ControlKey
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="packet"></param>
        private async void SocketOnBinaryWebSocketClientRequestValidateControlKeyAsync(IWebSocketConnection webSocketConnection,Protocol.WebSocketClientRequestValidateControlKey packet) {
            if(packet==null){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestValidateControlKey[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的数据包为空");
                webSocketConnection.Close();
                return;
            }
            if(packet.ClientConnectionGuid!=webSocketConnection.ConnectionInfo.Id.ToString() || !this.WebSocketConnectionWrapDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestValidateControlKey[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的会话Id不匹配");
                webSocketConnection.Close();
                return;
            }
            if(String.IsNullOrWhiteSpace(packet.ControlKey)) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestValidateControlKey[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的<ControlKey>为空");
                webSocketConnection.Close();
                return;
            }
            if(packet.ControlKey!=Program.AppSettings.ControlKey) {
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketOnBinaryWebSocketClientRequestValidateControlKey[Error]",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\" 请求的<ControlKey>不匹配");
                webSocketConnection.Close();
                return;
            }
            WebSocketConnectionWrap webSocketConnectionWrap=this.WebSocketConnectionWrapDictionary[webSocketConnection.ConnectionInfo.Id];
            webSocketConnectionWrap.Valid=true;
            webSocketConnectionWrap.LastPongTime=DateTimeOffset.Now.ToUnixTimeSeconds();
            //回复客户端已通过验证
            Protocol.WebSocketServerResponseValidateControlKey packetResponse=new Protocol.WebSocketServerResponseValidateControlKey{
                Type=2002,
                ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),
                Validation=webSocketConnectionWrap.Valid,
                ValidationMessage="your connection is validated"};
            await webSocketConnection.Send(packetResponse.ToBytes());
        }

        public async void NotifyClientsReloadUnitAsync(String unitName,Entities.UnitSettings unitSettings){
            /*
            Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientsReloadUnit","通知所有客户端指定单元正在刷新配置");
            if(this.WebSocketConnectionDictionary.Count<1){
                Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientsReloadUnit","没有需要通知的客户端");
                return;
            }
            foreach(KeyValuePair<Guid,IWebSocketConnection> item in this.WebSocketConnectionDictionary) {
                if(!item.Value.IsAvailable){return;}
                await item.Value.Send($"{item.Key.ToString()}{this.SplitChar}NotifyReloadUnit{this.SplitChar}{unitName}{this.SplitChar}{JsonConvert.SerializeObject(unitSettings)}");
            }
            */
        }

        public async void NotifyClientsStartUnitAsync(String unitName,UnitProcess unitProcess) {
            /*
            Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientsStartUnit","通知所有客户端指定单元正在启动");
            if(this.WebSocketConnectionDictionary.Count<1){
                Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientsStartUnit","没有需要通知的客户端");
                return;
            }
            String unitProcessJson=JsonConvert.SerializeObject(unitProcess);
            foreach(KeyValuePair<Guid,IWebSocketConnection> item in this.WebSocketConnectionDictionary) {
                if(!item.Value.IsAvailable){return;}
                await item.Value.Send($"{item.Key.ToString()}{this.SplitChar}NotifyStartUnit{this.SplitChar}{unitProcessJson}");
            }
            */
        }

        public async void NotifyClientsStartUnitFailedAsync(String unitName) {
            /*
            Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientsStartUnitFailedAsync","通知所有客户端指定单元启动失败");
            if(this.WebSocketConnectionDictionary.Count<1){
                Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientsStartUnitFailedAsync","没有需要通知的客户端");
                return;
            }
            foreach(KeyValuePair<Guid,IWebSocketConnection> item in this.WebSocketConnectionDictionary) {
                if(!item.Value.IsAvailable){return;}
                await item.Value.Send($"{item.Key.ToString()}{this.SplitChar}NotifyStartUnitFailed{this.SplitChar}{unitName}");
            }
            */
        }

        public async void NotifyClientsStopUnitAsync(String unitName) {
            /*
            Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientsStopUnit","通知所有客户端指定单元正在停止");
            if(this.WebSocketConnectionDictionary.Count<1){
                Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientsStopUnit","没有需要通知的客户端");
                return;
            }
            foreach(KeyValuePair<Guid,IWebSocketConnection> item in this.WebSocketConnectionDictionary) {
                if(!item.Value.IsAvailable){return;}
                await item.Value.Send($"{item.Key.ToString()}{this.SplitChar}NotifyStopUnit{this.SplitChar}{unitName}");
            }
            */
        }

        public async void NotifyClientsStopUnitFailedAsync(String unitName) {
            /*
            Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientsStopUnitFailedAsync","通知所有客户端指定单元停止失败");
            if(this.WebSocketConnectionDictionary.Count<1){
                Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientsStopUnitFailedAsync","没有需要通知的客户端");
                return;
            }
            foreach(KeyValuePair<Guid,IWebSocketConnection> item in this.WebSocketConnectionDictionary) {
                if(!item.Value.IsAvailable){return;}
                await item.Value.Send($"{item.Key.ToString()}{this.SplitChar}NotifyStopUnitFailed{this.SplitChar}{unitName}");
            }
            */
        }

        public async void NotifyClientFetchAllUnitsStatusAsync(Guid webSocketConnectionId,String allUnitsStatusJson) {
            /*
            Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientFetchAllUnitsStatusAsync","通知指定客户端获取所有单元状态");
            if(this.WebSocketConnectionDictionary.Count<1 || !this.WebSocketConnectionDictionary.ContainsKey(webSocketConnectionId)){
                Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientFetchAllUnitsStatusAsync","没有需要通知的客户端");
                return;
            }
            await this.WebSocketConnectionDictionary[webSocketConnectionId].Send($"{webSocketConnectionId.ToString()}{this.SplitChar}NotifyFetchAllUnitsStatus{this.SplitChar}{allUnitsStatusJson}");
            */
        }

        public async void NotifyClientFetchUnitStatusAsync(Guid webSocketConnectionId,String unitsStatusJson) {
            /*
            Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientFetchUnitStatusAsync","通知指定客户端获取指定单元状态");
            if(this.WebSocketConnectionDictionary.Count<1 || !this.WebSocketConnectionDictionary.ContainsKey(webSocketConnectionId)){
                Program.LoggerModule.Log("Modules.ControlServerModule.NotifyClientFetchUnitStatusAsync","没有需要通知的客户端");
                return;
            }
            await this.WebSocketConnectionDictionary[webSocketConnectionId].Send($"{webSocketConnectionId.ToString()}{this.SplitChar}NotifyFetchUnitStatus{this.SplitChar}{unitsStatusJson}");
            */
        }
    }
}