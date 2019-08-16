using Controller.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Controller.ViewModel {
    public class MainWindowViewModel:INotifyPropertyChanged{
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string _PropertyName)=>PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(_PropertyName));
        #endregion

        private String _InputString;
        private StringBuilder _OutputString=new StringBuilder();
        private String _Address="127.0.0.1";
        private String _Key="https://github.com/ragnaroks/Wind2";
        private UInt16 _Port=27015;
        private Int32 _ByteSent=0;
        private Int32 _ByteRecv=0;
        private String _UnitName;
        
        /// <summary>
        /// Window
        /// </summary>
        public Window Window{get;set;}
        /// <summary>
        /// UdpSocketClient
        /// </summary>
        private UdpSocketClient UdpSocketClient{get;set;}
        /// <summary>
        /// 输入
        /// </summary>
        public String InputString{
            get{return this._InputString;}
            set{this._InputString=value;RaisePropertyChanged("InputString");}
        }
        /// <summary>
        /// 输出
        /// </summary>
        public String OutputString {
            get{return this._OutputString.ToString();}
            set{
                if(this._OutputString.Length>65535){this._OutputString.Clear();}
                this._OutputString.AppendLine(value);
                RaisePropertyChanged("OutputString");
            }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public String Address{
            get{return this._Address;}
            set{this._Address=value;RaisePropertyChanged("Address");}
        }
        /// <summary>
        /// 链接端口
        /// </summary>
        public UInt16 Port{
            get{return this._Port;}
            set{this._Port=value;RaisePropertyChanged("Port");}
        }
        /// <summary>
        /// 链接密钥
        /// </summary>
        public String Key{
            get{return this._Key;}
            set{this._Key=value;RaisePropertyChanged("Key");}
        }
        /// <summary>
        /// 发送流量
        /// </summary>
        public Int32 ByteSent {
            get{return _ByteSent;}
            set{_ByteSent=value;RaisePropertyChanged("ByteSent");}
        }
        /// <summary>
        /// 接收流量
        /// </summary>
        public Int32 ByteRecv {
            get{return _ByteRecv;}
            set{_ByteRecv=value;RaisePropertyChanged("ByteRecv");}
        }
        /// <summary>
        /// 单元名称
        /// </summary>
        public String UnitName {
            get{return _UnitName;}
            set{_UnitName=value;RaisePropertyChanged("UnitName");}
        }
        
        /// <summary>
        /// 提交输入
        /// </summary>
        public ViewCommand.MainWindowCommand SubmitInputCommand=>new ViewCommand.MainWindowCommand(()=>{this.OutputString=this.InputString;},()=>!String.IsNullOrWhiteSpace(this.InputString));
        /// <summary>
        /// 清空输入
        /// </summary>
        public ViewCommand.MainWindowCommand CleanInputCommand=>new ViewCommand.MainWindowCommand(()=>{this.InputString=String.Empty;},()=>!String.IsNullOrWhiteSpace(this.InputString));
        /// <summary>
        /// 清空输入
        /// </summary>
        public ViewCommand.MainWindowCommand CleanOutputCommand=>new ViewCommand.MainWindowCommand(()=>{this._OutputString.Clear();RaisePropertyChanged("OutputString");},()=>true);
        /// <summary>
        /// 链接
        /// </summary>
        public ViewCommand.MainWindowCommand ConnectCommand=>new ViewCommand.MainWindowCommand(
            ()=>{
                this.OutputString="套接字初始化";
                this.UdpSocketClient=new UdpSocketClient(this);
            },
            ()=>{
                if(this.UdpSocketClient!=null && this.UdpSocketClient.Connected){return false;}
                if(String.IsNullOrWhiteSpace(this.Address) || String.IsNullOrWhiteSpace(this.Key) || this.Port<1024 || this.Port>UInt16.MaxValue){return false;}
                Regex regex=new Regex(@"^[a-f\d\:\.]{3,39}$",RegexOptions.Compiled);
                if (this.Address!="localhost") {
                    if(!regex.IsMatch(this.Address)){return false;}
                    if(!IPAddress.TryParse(this.Address,out IPAddress _ip)){return false;}
                }
                return true;
            }
        );
        /// <summary>
        /// 断开链接
        /// </summary>
        public ViewCommand.MainWindowCommand DisconnectCommand=>new ViewCommand.MainWindowCommand(
            ()=>{
                this.UdpSocketClient.Dispose();
                this.Window.Dispatcher.InvokeAsync(()=>{CommandManager.InvalidateRequerySuggested();},System.Windows.Threading.DispatcherPriority.DataBind);
            },
            ()=>this.UdpSocketClient!=null && this.UdpSocketClient.Connected
        );
        /// <summary>
        /// 获取服务主机信息
        /// </summary>
        public ViewCommand.MainWindowCommand GetHostVersionCommand=>new ViewCommand.MainWindowCommand(
            ()=>this.UdpSocketClient.SendAsync("{\"ActionId\":2,\"ActionName\":\"GetHostVersion\"}"),
            ()=>this.UdpSocketClient!=null && this.UdpSocketClient.Connected
        );
        /// <summary>
        /// 获取所有单元信息
        /// </summary>
        public ViewCommand.MainWindowCommand FetchUnitsCommand=>new ViewCommand.MainWindowCommand(
            ()=>this.UdpSocketClient.SendAsync("{\"ActionId\":1001,\"ActionName\":\"FetchUnits\"}"),
            ()=>this.UdpSocketClient!=null && this.UdpSocketClient.Connected
        );
        /// <summary>
        /// 启动所有单元
        /// </summary>
        public ViewCommand.MainWindowCommand StartAllUnitsCommand=>new ViewCommand.MainWindowCommand(
            ()=>this.UdpSocketClient.SendAsync("{\"ActionId\":1002,\"ActionName\":\"StartAllUnits\"}"),
            ()=>this.UdpSocketClient!=null && this.UdpSocketClient.Connected
        );
        /// <summary>
        /// 停止所有单元
        /// </summary>
        public ViewCommand.MainWindowCommand StopAllUnitsCommand=>new ViewCommand.MainWindowCommand(
            ()=>this.UdpSocketClient.SendAsync("{\"ActionId\":1004,\"ActionName\":\"StopAllUnits\"}"),
            ()=>this.UdpSocketClient!=null && this.UdpSocketClient.Connected
        );
        /// <summary>
        /// 启动指定单元
        /// </summary>
        public ViewCommand.MainWindowCommand StartUnitCommand=>new ViewCommand.MainWindowCommand(
            ()=>this.UdpSocketClient.SendAsync("{\"ActionId\":1003,\"ActionName\":\"StartUnit\",\"UnitName\":\""+this.UnitName+"\"}"),
            ()=>this.UdpSocketClient!=null && this.UdpSocketClient.Connected
        );
        /// <summary>
        /// 停止指定单元
        /// </summary>
        public ViewCommand.MainWindowCommand StopUnitCommand=>new ViewCommand.MainWindowCommand(
            ()=>this.UdpSocketClient.SendAsync("{\"ActionId\":1005,\"ActionName\":\"StopUnit\",\"UnitName\":\""+this.UnitName+"\"}"),
            ()=>this.UdpSocketClient!=null && this.UdpSocketClient.Connected
        );

        public MainWindowViewModel() {
            this.OutputString="界面已初始化";
        }
    }
}
