using System;
using System.Collections.Generic;
using System.Text;

namespace wind.Helpers {
    public static class LoggerModuleHelper {
        /// <summary>
        /// 尝试使用日志模块记录,若日志模块不可用则使用控制台输出
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        public static void TryLog(String title,String text){
            if(String.IsNullOrWhiteSpace(title) || String.IsNullOrWhiteSpace(text)){return;}
            //日志模块可用并且不是开发环境
            if(Program.LoggerModule.Useable && !Program.AppEnvironment.DevelopMode) {
                Program.LoggerModule.Log(title,text);
                return;
            }
            //日志模块不可用或是开发环境
            if(title.Contains("[Error]",StringComparison.Ordinal)) {
                Console.ForegroundColor=ConsoleColor.Red;
                Console.Write("[ERROR]");
                Console.ResetColor();
                Console.WriteLine($": {title} => {text}");
            }else if(title.Contains("[Warning]",StringComparison.Ordinal)) {
                Console.ForegroundColor=ConsoleColor.DarkYellow;
                Console.Write("[WARNING]");
                Console.ResetColor();
                Console.WriteLine($": {title} => {text}");
            }else{
                Console.ForegroundColor=ConsoleColor.Cyan;
                Console.Write("[INFO]");
                Console.ResetColor();
                Console.WriteLine($": {title} => {text}");
            }
        }
    }
}
