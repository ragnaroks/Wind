using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace windctl.Helpers {
    public static class CommandHelper {
        /// <summary>
        /// windctl version
        /// </summary>
        public static void Version() {
            Version version=Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine($"Wind2 Daemon Service Command-Line Controller v{version.Major}.{version.Minor}.{version.Build}");
        }

        /// <summary>
        /// windctl help
        /// </summary>
        public static void Help() {
            Console.WriteLine("windctl version              =>  print this tool's version");
            Console.WriteLine("windctl status <unitKey>     =>  get unit's status");
            Console.WriteLine("windctl start <unitKey>      =>  start unit");
            Console.WriteLine("windctl stop <unitKey>       =>  stop unit");
            Console.WriteLine("windctl restart <unitKey>    =>  restart unit");
            Console.WriteLine("windctl load <unitKey>       =>  try load/update unit's settings from file,need restart to apply");
            Console.WriteLine("windctl remove <unitKey>     =>  stop unit and remove it,it can not be start again");
            Console.WriteLine("windctl start-all            =>  start all unit");
            Console.WriteLine("windctl stop-all             =>  stop all unit");
            Console.WriteLine("windctl restart-all          =>  restart all unit");
            Console.WriteLine("windctl load-all             =>  try load/update all units's settings from file,need restart to apply");
            Console.WriteLine("windctl remove-all           =>  stop all unit and remove them,they can not be start again");
            Console.WriteLine("windctl daemon-version       =>  get daemon service's version");
            Console.WriteLine("windctl daemon-status        =>  get daemon service's status");
            Console.WriteLine("windctl daemon-shutdown      =>  shutdown daemon service");
        }
    }
}
