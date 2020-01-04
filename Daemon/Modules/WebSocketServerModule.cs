using Fleck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Daemon.Modules {
    public class WebSocketServerModule:IDisposable {
        private readonly WebSocketServer WebSocketServer;
        private readonly Dictionary<Guid,IWebSocketConnection> WebSocketConnectionDictionary=new Dictionary<Guid,IWebSocketConnection>();
        private readonly Byte[] PingPacket=new Byte[]{0x07,0x03,0x05,0x05,0x06,0x00,0x08};
        private readonly Timer PingTimer;
        private Boolean CanPingTimerState=true;

        public WebSocketServerModule(Int16 port,String address){
            if(!this.Check(address)){
                Program.LoggerModule.Log("Modules.WebSocketServerModule.WebSocketServerModule[Warning]","取消创建WebSocketServer,参数错误");
                return;
            }
            if(address=="localhost"){address="127.0.0.1";}
            if(address=="any"){address="0.0.0.0";}
            Program.LoggerModule.Log("Modules.WebSocketServerModule.WebSocketServerModule","尝试创建WebSocketServer");
            try {
                this.WebSocketServer=new WebSocketServer($"ws://{address}:{port}",true);
                this.WebSocketServer.ListenerSocket.NoDelay=true;
            }catch(Exception exception){
                ConsoleColor cc=Console.ForegroundColor;
                Console.ForegroundColor=ConsoleColor.Green;
                Console.WriteLine($"Modules.WebSocketServerModule.WebSocketServerModule[Error] => 创建WebSocketServer异常 | {exception.Message} | {exception.StackTrace}");
                Console.ForegroundColor=cc;
                Program.LoggerModule.Log("Modules.WebSocketServerModule.WebSocketServerModule[Error]",$"创建WebSocketServer异常,{exception.Message},{exception.StackTrace}");
            }
            this.PingTimer=new Timer(new TimerCallback(OnPingTimerCallback),null,10000,5000);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: 释放托管状态(托管对象)。
                    if(this.WebSocketServer!=null){this.WebSocketServer.Dispose();}
                    if(this.WebSocketServer!=null){this.WebSocketServer.Dispose();}
                    if(this.PingTimer!=null){this.PingTimer.Dispose();}
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~WebSocketServerModule()
        // {
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
        /// 检查地址
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private Boolean Check(String address){
            if(String.IsNullOrWhiteSpace(address)){
                Program.LoggerModule.Log("Modules.WebSocketServerModule.Check[Warning]","地址不能为空");
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
            Program.LoggerModule.Log("Modules.WebSocketServerModule.OnPingTimerCallback","开始ping所有客户端");
            if(!this.CanPingTimerState || this.WebSocketConnectionDictionary.Count<1){
                Program.LoggerModule.Log("Modules.WebSocketServerModule.OnPingTimerCallback","当前无任何客户端");
                return;
            }
            this.CanPingTimerState=false;
            foreach(KeyValuePair<Guid,IWebSocketConnection> item in this.WebSocketConnectionDictionary){
                this.SocketRequestPingAsync(item.Value);
            }
            this.CanPingTimerState=true;
            Program.LoggerModule.Log("Modules.WebSocketServerModule.OnPingTimerCallback","ping所有客户端介素");
        }

        /// <summary>
        /// 如果客户端链接已失效则会踢出
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <returns></returns>
        private Boolean IsAvailableCheck(IWebSocketConnection webSocketConnection){
            Program.LoggerModule.Log("Modules.WebSocketServerModule.IsAvailableCheck",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"开始检查");
            if(webSocketConnection.IsAvailable){return true;}
            if(this.WebSocketConnectionDictionary.ContainsKey(webSocketConnection.ConnectionInfo.Id)){
                Program.LoggerModule.Log("Modules.WebSocketServerModule.IsAvailableCheck",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"因会话失效而被踢出");
                this.WebSocketConnectionDictionary.Remove(webSocketConnection.ConnectionInfo.Id);
            }
            return false;
        }

        /// <summary>
        /// ping客户端,如果客户端无响应则会踢出
        /// </summary>
        /// <param name="webSocketConnection"></param>
        private async void SocketRequestPingAsync(IWebSocketConnection webSocketConnection){
            Program.LoggerModule.Log("Modules.WebSocketServerModule.SocketRequestPingAsync",$"ping客户端\"{webSocketConnection.ConnectionInfo.Id}\"开始");
            if(!this.IsAvailableCheck(webSocketConnection)){return;}
            await webSocketConnection.SendPing(this.PingPacket);
            Program.LoggerModule.Log("Modules.WebSocketServerModule.SocketRequestPingAsync",$"ping客户端\"{webSocketConnection.ConnectionInfo.Id}\"完成");
        }

        /// <summary>
        /// 接收到来自客户端打开链接请求
        /// </summary>
        /// <param name="webSocketConnection"></param>
        private async void SocketOnOpenAsync(IWebSocketConnection webSocketConnection){
            Program.LoggerModule.Log("Modules.WebSocketServerModule.SocketOnOpenAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求链接");
            /*
             * if not allow
             * code here
             */
            this.WebSocketConnectionDictionary.Add(webSocketConnection.ConnectionInfo.Id,webSocketConnection);
            Program.LoggerModule.Log("Modules.WebSocketServerModule.SocketOnOpenAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"允许链接并加入列表");
            await webSocketConnection.Send("{}");
        }

        /// <summary>
        /// 接收到来自客户端关闭链接请求
        /// </summary>
        /// <param name="webSocketConnection"></param>
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        private async void SocketOnCloseAsync(IWebSocketConnection webSocketConnection){
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
            Program.LoggerModule.Log("Modules.WebSocketServerModule.SocketOnCloseAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"断开链接");
            if(!this.IsAvailableCheck(webSocketConnection)){return;}            
            this.WebSocketConnectionDictionary.Remove(webSocketConnection.ConnectionInfo.Id);
        }

        /// <summary>
        /// 接收到来自客户端异常
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="exception"></param>
        private async void SocketOnErrorAsync(IWebSocketConnection webSocketConnection,Exception exception) {
            Program.LoggerModule.Log("Modules.WebSocketServerModule.SocketOnErrorAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"链接异常,{exception.Message},{exception.StackTrace}");
            if(!this.IsAvailableCheck(webSocketConnection)){return;}
            await webSocketConnection.Send("SocketOnErrorAsync => exception "+exception.Message);
        }

        /// <summary>
        /// 接收到来自客户端ping请求
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="bytes"></param>
        private async void SocketOnPingAsync(IWebSocketConnection webSocketConnection,Byte[] bytes){
            Program.LoggerModule.Log("Modules.WebSocketServerModule.SocketOnPingAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"请求Ping");
            //if(!this.IsAvailableCheck(webSocketConnection)){return;}
            await webSocketConnection.SendPong(bytes);
        }

        /// <summary>
        /// 接收到来自客户端pong响应
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="bytes"></param>
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        private async void SocketOnPongAsync(IWebSocketConnection webSocketConnection,Byte[] bytes) {
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
            Program.LoggerModule.Log("Modules.WebSocketServerModule.SocketOnPongAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"回复Pong");
            if(!this.IsAvailableCheck(webSocketConnection)){return;}
        }

        /// <summary>
        /// 接收到来自客户端的文本消息
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="message"></param>
        private async void SocketOnMessageAsync(IWebSocketConnection webSocketConnection,String message) {
            Program.LoggerModule.Log("Modules.WebSocketServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"发来文本消息");
            if(!this.IsAvailableCheck(webSocketConnection)){return;}
            await webSocketConnection.Send("SocketOnMessageAsync => you send "+message);
        }

        /// <summary>
        /// 接收到来自客户端的字节数组消息
        /// </summary>
        /// <param name="webSocketConnection"></param>
        /// <param name="bytes"></param>
        private async void SocketOnBinaryAsync(IWebSocketConnection webSocketConnection,Byte[] bytes) {
            Program.LoggerModule.Log("Modules.WebSocketServerModule.SocketOnMessageAsync",$"客户端\"{webSocketConnection.ConnectionInfo.Id}\"发来字节数组消息");
            if(!this.IsAvailableCheck(webSocketConnection)){return;}
            await webSocketConnection.Send(Encoding.UTF8.GetBytes("SocketOnBinaryAsync => you send "+bytes.Length+"byte"));
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
                webSocketConnection.OnMessage=(message)=>this.SocketOnMessageAsync(webSocketConnection,message);
                webSocketConnection.OnBinary=(bytes)=>this.SocketOnBinaryAsync(webSocketConnection,bytes);
            });
        }
    }
}