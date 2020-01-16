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
        private readonly WebSocketServer WebSocketServer;
        private readonly Byte[] PingPacket=new Byte[]{0x07,0x03,0x05,0x05,0x06,0x00,0x08};
        private readonly Timer PingTimer;
        private Boolean PingTimerEnable=true;
        private readonly Timer CleanTimer;
        private Boolean CleanTimerEnable=true;
        private Dictionary<Guid,Entities.WebSocketConnectionWrap> WebSocketConnectionWrapDictionary=new Dictionary<Guid,Entities.WebSocketConnectionWrap>();

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
            Entities.WebSocketConnectionWrap webSocketConnectionWrap =new Entities.WebSocketConnectionWrap {
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
            Entities.WebSocketConnectionWrap webSocketConnectionWrap =this.WebSocketConnectionWrapDictionary[webSocketConnection.ConnectionInfo.Id];
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
            if(packetRequest.ControlKey!=Program.AppSettings.ControlKey) {
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
            await webSocketConnection.Send(packetResponse.ToBytes());
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
            Process process=Process.GetCurrentProcess();
            Protocol.DaemonMeta packetDaemonMeta=new Protocol.DaemonMeta{
                Version=Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                HostCpuCores=(UInt32)Environment.ProcessorCount,
                HostMemorySize=(UInt64)WindowsManagementHelper.GetPhysicalMemorySize(),
                WorkDirectory=Program.AppEnvironment.BaseDirectory,
                ProcessId=(UInt32)process.Id};
            process.Dispose();
            Protocol.WebSocketServerResponseFetchDaemonMeta packetResponse=new Protocol.WebSocketServerResponseFetchDaemonMeta{
                Type=2003,ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),DaemonMeta=packetDaemonMeta};
            await webSocketConnection.Send(packetResponse.ToBytes());
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
                UnitProcessCount=(UInt32)Program.UnitControlModule.GetUnitProcessCount()};
            Protocol.WebSocketServerResponseFetchDaemonStatus packetResponse=new Protocol.WebSocketServerResponseFetchDaemonStatus{
                Type=2004,ClientConnectionGuid=webSocketConnection.ConnectionInfo.Id.ToString(),DaemonStatus=packetDaemonStatus};
            await webSocketConnection.Send(packetResponse.ToBytes());
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
                unitStatus.UnitSettings=new Protocol.UnitSettings{
                    Name=item.UnitSettings.Name,
                    Description=item.UnitSettings.Description,
                    ExecuteAbsolutePath=item.UnitSettings.ExecuteAbsolutePath,
                    WorkAbsoluteDirectory=item.UnitSettings.WorkAbsoluteDirectory,
                    ExecuteParams=item.UnitSettings.ExecuteParams??String.Empty,
                    AutoStart=item.UnitSettings.AutoStart,
                    AutoStartDelay=(UInt32)item.UnitSettings.AutoStartDelay,
                    DaemonProcess=item.UnitSettings.DaemonProcess,
                    HaveChildProcesses=item.UnitSettings.HaveChildProcesses};
                if(item.UnitProcess==null){
                    unitStatus.UnitProcess=new Protocol.UnitProcess{Name=item.UnitSettings.Name,State=1,ProcessId=0};
                } else {
                    unitStatus.UnitProcess=new Protocol.UnitProcess{
                        Name=item.UnitSettings.Name,
                        State=(UInt32)item.UnitProcess.State,
                        ProcessId=(UInt32)item.UnitProcess.ProcessId};
                }
                packetResponse.UnitStatus.Add(unitStatus);
            }
            await webSocketConnection.Send(packetResponse.ToBytes());
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
            unitStatus.UnitSettings=new Protocol.UnitSettings{
                Name=item.UnitSettings.Name,
                Description=item.UnitSettings.Description,
                ExecuteAbsolutePath=item.UnitSettings.ExecuteAbsolutePath,
                WorkAbsoluteDirectory=item.UnitSettings.WorkAbsoluteDirectory,
                ExecuteParams=item.UnitSettings.ExecuteParams??String.Empty,
                AutoStart=item.UnitSettings.AutoStart,
                AutoStartDelay=(UInt32)item.UnitSettings.AutoStartDelay,
                DaemonProcess=item.UnitSettings.DaemonProcess,
                HaveChildProcesses=item.UnitSettings.HaveChildProcesses};
            if(item.UnitProcess==null){
                unitStatus.UnitProcess=new Protocol.UnitProcess{Name=item.UnitSettings.Name,State=1,ProcessId=0};
            } else {
                unitStatus.UnitProcess=new Protocol.UnitProcess{
                    Name=item.UnitSettings.Name,
                    State=(UInt32)item.UnitProcess.State,
                    ProcessId=(UInt32)item.UnitProcess.ProcessId};
            }
            packetResponse.UnitStatus=unitStatus;
            await webSocketConnection.Send(packetResponse.ToBytes());
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
            await webSocketConnection.Send(packetResponse.ToBytes());
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
            await webSocketConnection.Send(packetResponse.ToBytes());
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
            await webSocketConnection.Send(packetResponse.ToBytes());
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
            await webSocketConnection.Send(packetResponse.ToBytes());
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
            await webSocketConnection.Send(packetResponse.ToBytes());
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
            await webSocketConnection.Send(packetResponse.ToBytes());
        }
        
        /// <summary>
        /// 服务端通知所有客户端指定单元被重载
        /// </summary>
        /// <param name="unitName"></param>
        /// <param name="unitSettings"></param>
        public async void SocketSendBinaryWebSocketServerNotifyClientsThatUnitSettingsReloadAsync(String unitName,Entities.UnitSettings unitSettings){
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitSettingsReloadAsync","服务端通知所有客户端指定单元被重载");
            if(this.WebSocketConnectionWrapDictionary.Count<1){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitSettingsReloadAsync","没有需要通知的客户端");
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
                HaveChildProcesses=unitSettings.HaveChildProcesses};
            Protocol.WebSocketServerNotifyClientsThatUnitSettingsReload packetResponse=new Protocol.WebSocketServerNotifyClientsThatUnitSettingsReload{Type=2013,UnitName=unitName,UnitSettings=packetUnitSettings};
            foreach(KeyValuePair<Guid,Entities.WebSocketConnectionWrap> item in this.WebSocketConnectionWrapDictionary) {
                if(!item.Value.Valid || !item.Value.WebSocketConnection.IsAvailable){continue;}
                packetResponse.ClientConnectionGuid=item.Value.WebSocketConnection.ConnectionInfo.Id.ToString();
                await item.Value.WebSocketConnection.Send(packetResponse.ToBytes());
            }
        }

        /// <summary>
        /// 服务端通知所有客户端指定单元已启动
        /// </summary>
        /// <param name="unitName"></param>
        /// <param name="unitProcess"></param>
        public async void SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartedAsync(String unitName,Entities.UnitProcess unitProcess){
            Program.LoggerModule.Log("Modules.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartedAsync","服务端通知所有客户端指定单元已启动");
            if(this.WebSocketConnectionWrapDictionary.Count<1){
                Program.LoggerModule.Log("Modules.ControlServerModule.SocketSendBinaryWebSocketServerNotifyClientsThatUnitStartedAsync","没有需要通知的客户端");
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
                await item.Value.WebSocketConnection.Send(packetResponse.ToBytes());
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
                await item.Value.WebSocketConnection.Send(packetResponse.ToBytes());
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
                await item.Value.WebSocketConnection.Send(packetResponse.ToBytes());
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
                await item.Value.WebSocketConnection.Send(packetResponse.ToBytes());
            }
        }
    }
}