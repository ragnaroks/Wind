using System;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using wind.Entities.Protobuf;

namespace windctl.Helpers {
    public static class CommandHelper {
        private const String HELP
            ="windctl [Any]                =>  print this help\n"
            +"windctl version              =>  print windctl's version\n"
            +"windctl status <unitKey>     =>  print unit's status\n"
            +"windctl start <unitKey>      =>  start unit\n"
            +"windctl stop <unitKey>       =>  stop unit\n"
            +"windctl restart <unitKey>    =>  restart unit\n"
            +"windctl load <unitKey>       =>  try load/update unit's settings from file,need restart to apply\n"
            +"windctl remove <unitKey>     =>  stop unit and remove it,it can not be start again\n"
            +"windctl logs <unitKey>       =>  print unit's latest logs\n"
            +"windctl attach <unitKey>     =>  attach to unit console host\n"
            +"windctl status-all           =>  print all unit's status(lite)\n"
            +"windctl start-all            =>  start all unit\n"
            +"windctl stop-all             =>  stop all unit\n"
            +"windctl restart-all          =>  restart all unit\n"
            +"windctl load-all             =>  try load/update all units's settings from file,need restart to apply\n"
            +"windctl remove-all           =>  stop all unit and remove them,they can not be start again\n"
            +"windctl daemon-version       =>  get daemon service's version\n"
            +"windctl daemon-status        =>  get daemon service's status\n"
            +"windctl daemon-shutdown      =>  shutdown daemon service\n";
        private const String ERROR_RESPONSE="command execute failed,response is empty or incorrect";

        /// <summary>
        /// 是否远程控制指令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Boolean IsRemoteCommand(String command){
            switch(command){
                case "status":
                case "start":
                case "stop":
                case "restart":
                case "load":
                case "remove":
                case "logs":
                case "attach":
                case "status-all":
                case "start-all":
                case "stop-all":
                case "restart-all":
                case "load-all":
                case "remove-all":
                case "daemon-version":
                case "daemon-status":
                case "daemon-shutdown":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 是否远程控制指令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="unitKey"></param>
        /// <returns></returns>
        public static Boolean RequireUnitKey(String command,String unitKey){
            switch(command){
                case "status":
                case "start":
                case "stop":
                case "restart":
                case "load":
                case "remove":
                case "logs":
                case "attach":
                    break;
                default:return true;
            }
            if(String.IsNullOrWhiteSpace(unitKey) || unitKey.Length>32){return false;}
            Regex regex=new Regex(@"^\S+$",RegexOptions.Compiled);
            if(!regex.IsMatch(unitKey)){return false;}
            return true;
        }

        /// <summary>
        /// windctl help
        /// </summary>
        public static void Help()=>Console.Write(HELP);
        /// <summary>
        /// windctl version
        /// </summary>
        public static void Version()=>Console.WriteLine($"windctl v{Assembly.GetExecutingAssembly().GetName().Version}");

        /// <summary>
        /// windctl status unitKey
        /// </summary>
        public static void Status(StatusResponseProtobuf statusResponseProtobuf){
            if(statusResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!statusResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",statusResponseProtobuf.NoExecuteMessage));
                return;
            }
            UnitProtobuf unitProtobuf=statusResponseProtobuf.UnitProtobuf;
            //第一行
            if(unitProtobuf.State==2){ Console.ForegroundColor=ConsoleColor.Green; }
            Console.Write("● ");
            if(unitProtobuf.State==2){ Console.ResetColor(); }
            UnitSettingsProtobuf unitSettingsProtobuf=unitProtobuf.State==2?unitProtobuf.RunningSettingsProtobuf:unitProtobuf.SettingsProtobuf;
            Console.Write($"{unitSettingsProtobuf.Name} - {unitSettingsProtobuf.Description}");
            //第二行
            Console.Write($"\n     Loaded:  {unitProtobuf.SettingsFilePath}");
            if(unitSettingsProtobuf.AutoStart){ Console.Write(" (auto)"); }
            //第三行
            Console.Write($"\n      State:  ");
            switch(unitProtobuf.State) {
                case 0:Console.Write("stopped");break;
                case 1:Console.Write("starting");break;
                case 2:
                    Console.ForegroundColor=ConsoleColor.Green;
                    Console.Write("running");
                    Console.ResetColor();
                    Console.Write(' ');
                    Console.ForegroundColor=ConsoleColor.Cyan;
                    Console.Write($"#{unitProtobuf.ProcessId}");
                    Console.ResetColor();
                    Console.Write($" (since {unitProtobuf.ProcessStartTime.ToLocalTimestampString()})");
                    break;
                case 3:Console.Write("stopping");break;
                default:Console.Write("unknown");break;
            }
            //第四行
            Console.Write($"\nCommandLine:  {unitSettingsProtobuf.AbsoluteExecutePath}");
            if(unitSettingsProtobuf.HasArguments){ Console.Write($" {unitSettingsProtobuf.Arguments}"); }
            //第五行
            if(unitProtobuf.State==2 && unitSettingsProtobuf.MonitorPerformanceUsage) {
                String cpuValue=String.Format(CultureInfo.InvariantCulture,"{0:N1} %",unitProtobuf.PerformanceCounterCPU/unitProtobuf.ProcessorCount);
                String ramValue=unitProtobuf.PerformanceCounterRAM.FixedByteSize();
                Console.Write($"\nPerformance:  {cpuValue}; {ramValue}");
            }
            //第六行
            if(unitProtobuf.State==2 && unitSettingsProtobuf.MonitorNetworkUsage) {
                String sendSpeed=unitProtobuf.NetworkCounterSendSpeed.FixedByteSize();
                String receiveSpeed=unitProtobuf.NetworkCounterReceiveSpeed.FixedByteSize();
                String totalSent=unitProtobuf.NetworkCounterTotalSent.FixedByteSize();
                String totalReceived=unitProtobuf.NetworkCounterTotalReceived.FixedByteSize();
                Console.Write($"\n    Network:  ↑ {sendSpeed}/s,{totalSent}; ↓ {receiveSpeed}/s,{totalReceived}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// windctl start unitKey
        /// </summary>
        public static void Start(StartResponseProtobuf startResponseProtobuf){
            if(startResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!startResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",startResponseProtobuf.NoExecuteMessage));
                return;
            }
            Console.WriteLine($"command executed,unit {startResponseProtobuf.UnitKey} starting");
        }

        /// <summary>
        /// windctl stop unitKey
        /// </summary>
        public static void Stop(StopResponseProtobuf stopResponseProtobuf){
            if(stopResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!stopResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",stopResponseProtobuf.NoExecuteMessage));
                return;
            }
            Console.WriteLine($"command executed,unit {stopResponseProtobuf.UnitKey} stopping");
        }

        /// <summary>
        /// windctl restart unitKey
        /// </summary>
        public static void Restart(RestartResponseProtobuf restartResponseProtobuf){
            if(restartResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!restartResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",restartResponseProtobuf.NoExecuteMessage));
                return;
            }
            Console.WriteLine($"command executed,unit {restartResponseProtobuf.UnitKey} restarting");
        }

        /// <summary>
        /// windctl load unitKey
        /// </summary>
        public static void Load(LoadResponseProtobuf loadResponseProtobuf){
            if(loadResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!loadResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",loadResponseProtobuf.NoExecuteMessage));
                return;
            }
            Console.WriteLine($"command executed,unit {loadResponseProtobuf.UnitKey} loading");
        }

        /// <summary>
        /// windctl remove unitKey
        /// </summary>
        public static void Remove(RemoveResponseProtobuf removeResponseProtobuf){
            if(removeResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!removeResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",removeResponseProtobuf.NoExecuteMessage));
                return;
            }
            Console.WriteLine($"command executed,unit {removeResponseProtobuf.UnitKey} stopping and removing");
        }

        /// <summary>
        /// windctl logs unitKey
        /// </summary>
        public static void Logs(LogsResponseProtobuf logsResponseProtobuf){
            if(logsResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!logsResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",logsResponseProtobuf.NoExecuteMessage));
                return;
            }
            Console.ForegroundColor=ConsoleColor.Yellow;
            Console.WriteLine($"see more details in {logsResponseProtobuf.LogFilePath}");
            Console.ResetColor();
            for(Int32 i1=0;i1<logsResponseProtobuf.LogLineArray.Count;i1++){ Console.WriteLine(logsResponseProtobuf.LogLineArray[i1]); }
        }

        /// <summary>
        /// windctl attach unitKey
        /// </summary>
        public static void Commandline(CommandlineResponseProtobuf commandlineResponseProtobuf){
            if(commandlineResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!commandlineResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",commandlineResponseProtobuf.NoExecuteMessage));
                return;
            }
            if(commandlineResponseProtobuf.CommandType==9) {
                Program.AttachedUnitKey=null;
            }
        }

        /// <summary>
        /// windctl status-all
        /// </summary>
        public static void StatusAll(StatusAllResponseProtobuf statusallResponseProtobuf){
            if(statusallResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!statusallResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",statusallResponseProtobuf.NoExecuteMessage));
                return;
            }
            if(statusallResponseProtobuf.UnitProtobufArraySize<1) {
                Console.WriteLine("command execute failed,not have any unit");
                return;
            }
            foreach(UnitProtobuf item in statusallResponseProtobuf.UnitProtobufArray) {
                //第一行
                if(item.State==2){ Console.ForegroundColor=ConsoleColor.Green; }
                Console.Write("● ");
                if(item.State==2){ Console.ResetColor(); }
                UnitSettingsProtobuf unitSettingsProtobuf=item.State==2
                    ?item.RunningSettingsProtobuf
                    :item.SettingsProtobuf;
                Console.Write($"{unitSettingsProtobuf.Name} - {unitSettingsProtobuf.Description}");
                //第二行
                Console.Write($"\n     Loaded:  {item.SettingsFilePath}");
                if(unitSettingsProtobuf.AutoStart){ Console.Write(" (auto)"); }
                //第三行
                Console.Write($"\n      State:  ");
                switch(item.State) {
                    case 0:Console.Write("stopped");break;
                    case 1:Console.Write("starting");break;
                    case 2:
                        Console.ForegroundColor=ConsoleColor.Green;
                        Console.Write("running");
                        Console.ResetColor();
                        Console.Write(' ');
                        Console.ForegroundColor=ConsoleColor.Cyan;
                        Console.Write($"#{item.ProcessId}");
                        Console.ResetColor();
                        Console.Write($" (since {item.ProcessStartTime.ToLocalTimestampString()})");
                        break;
                    case 3:Console.Write("stopping");break;
                    default:Console.Write("unknown");break;
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// windctl start-all
        /// </summary>
        public static void StartAll(StartAllResponseProtobuf startallResponseProtobuf){
            if(startallResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!startallResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",startallResponseProtobuf.NoExecuteMessage));
                return;
            }
            Console.WriteLine($"command executed,all units starting");
        }

        /// <summary>
        /// windctl stop-all
        /// </summary>
        public static void StopAll(StopAllResponseProtobuf stopAllResponseProtobuf){
            if(stopAllResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!stopAllResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",stopAllResponseProtobuf.NoExecuteMessage));
                return;
            }
            Console.WriteLine($"command executed,all units stopping");
        }

        /// <summary>
        /// windctl restart-all
        /// </summary>
        public static void RestartAll(RestartAllResponseProtobuf restartAllResponseProtobuf){
            if(restartAllResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!restartAllResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",restartAllResponseProtobuf.NoExecuteMessage));
                return;
            }
            Console.WriteLine($"command executed,all units restarting");
        }

        /// <summary>
        /// windctl load-all
        /// </summary>
        public static void LoadAll(LoadAllResponseProtobuf loadAllResponseProtobuf){
            if(loadAllResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!loadAllResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",loadAllResponseProtobuf.NoExecuteMessage));
                return;
            }
            Console.WriteLine($"command executed,all units loading");
        }

        /// <summary>
        /// windctl remove-all
        /// </summary>
        public static void RemoveAll(RemoveAllResponseProtobuf removeAllResponseProtobuf){
            if(removeAllResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            if(!removeAllResponseProtobuf.Executed) {
                Console.WriteLine(String.Concat("command execute failed,",removeAllResponseProtobuf.NoExecuteMessage));
                return;
            }
            Console.WriteLine($"command executed,all units stopping and removing");
        }
        
        /// <summary>
        /// windctl daemon-version
        /// </summary>
        public static void DaemonVersion(DaemonVersionResponseProtobuf daemonVersionResponseProtobuf){
            if(daemonVersionResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            String versionString=String.Concat(
                "wind v",
                daemonVersionResponseProtobuf.Major,".",
                daemonVersionResponseProtobuf.Minor,".",
                daemonVersionResponseProtobuf.Build,".",
                daemonVersionResponseProtobuf.Revision);
            Version version=Assembly.GetExecutingAssembly().GetName().Version;
            if(version.Major==daemonVersionResponseProtobuf.Major
            && version.Minor==daemonVersionResponseProtobuf.Minor
            && version.Build==daemonVersionResponseProtobuf.Build
            && version.Revision==daemonVersionResponseProtobuf.Revision){
                versionString=String.Concat(versionString,",same version with windctl");
            }
            Console.WriteLine(versionString);
        }

        /// <summary>
        /// windctl daemon-status
        /// </summary>
        public static void DaemonStatus(DaemonStatusResponseProtobuf daemonStatusResponseProtobuf) {
            if(daemonStatusResponseProtobuf==null){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            DaemonProtobuf daemonProtobuf=daemonStatusResponseProtobuf.DaemonProtobuf;
            //第一行
            Console.ForegroundColor=ConsoleColor.Green;
            Console.Write("● ");
            Console.ResetColor();
            Console.Write($"{daemonProtobuf.Name} - {daemonProtobuf.Description}");
            //第二行
            Console.Write($"\n     Loaded:  {daemonProtobuf.AbsoluteWorkDirectory}Data\\AppSettings.json");
            //第三行
            Console.Write($"\n      State:  ");
            Console.ForegroundColor=ConsoleColor.Green;
            Console.Write("running");
            Console.ResetColor();
            Console.Write(' ');
            Console.ForegroundColor=ConsoleColor.Cyan;
            Console.Write($" #{daemonProtobuf.ProcessId}");
            Console.ResetColor();
            Console.Write($" (since {daemonProtobuf.ProcessStartTime.ToLocalTimestampString()})");
            //第四行
            String cpuValue=String.Format(CultureInfo.InvariantCulture,"{0:N1} %",daemonProtobuf.PerformanceCounterCPU);
            String ramValue=daemonProtobuf.PerformanceCounterRAM.FixedByteSize();
            Console.Write($"\nPerformance:  {cpuValue}; {ramValue}");
            //第五行
            String sendSpeed=daemonProtobuf.NetworkCounterSendSpeed.FixedByteSize();
            String receiveSpeed=daemonProtobuf.NetworkCounterReceiveSpeed.FixedByteSize();
            String totalSent=daemonProtobuf.NetworkCounterTotalSent.FixedByteSize();
            String totalReceived=daemonProtobuf.NetworkCounterTotalReceived.FixedByteSize();
            Console.Write($"\n    Network:  ↑ {sendSpeed}/s,{totalSent}; ↓ {receiveSpeed}/s,{totalReceived}");
            Console.WriteLine();
        }

        /// <summary>
        /// windctl daemon-shutdoown
        /// </summary>
        public static void DaemonShutdown(DaemonShutdownResponseProtobuf daemonShutdownResponseProtobuf) {
            if(daemonShutdownResponseProtobuf==null || daemonShutdownResponseProtobuf.Type!=2299){
                Console.WriteLine(ERROR_RESPONSE);
                return;
            }
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine("wind is shutting");
            Console.ResetColor();
        }
    }
}
