using System;
using System.Collections.Generic;
using System.Text;
using wind.Entities.Protobuf;

namespace windctl.Helpers {
    public static class NotifyHelper {
        /// <summary>
        /// 单元启动
        /// </summary>
        /// <param name="startNotifyProtobuf"></param>
        public static void Start(StartNotifyProtobuf startNotifyProtobuf) {
            if(startNotifyProtobuf==null){return;}
            //do nothing
            return;
        }
        /// <summary>
        /// 单元停止
        /// </summary>
        /// <param name="stopNotifyProtobuf"></param>
        public static void Stop(StopNotifyProtobuf stopNotifyProtobuf) {
            if(stopNotifyProtobuf==null){return;}
            if(!String.IsNullOrWhiteSpace(Program.AttachedUnitKey) && stopNotifyProtobuf.UnitKey==Program.AttachedUnitKey) {
                Program.AttachedUnitKey=null;
                if(Program.InAction){ Program.InAction=false; }
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("dettach from unit,because unit stopped");
                Console.ResetColor();
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// 收到服务端日志通知
        /// </summary>
        /// <param name="logsNotifyProtobuf"></param>
        public static void Logs(LogsNotifyProtobuf logsNotifyProtobuf) {
            if(logsNotifyProtobuf==null){return;}
            if(!String.IsNullOrWhiteSpace(Program.AttachedUnitKey) && logsNotifyProtobuf.UnitKey==Program.AttachedUnitKey) {
                Console.WriteLine(logsNotifyProtobuf.LogLine);
            }
        }
    }
}
