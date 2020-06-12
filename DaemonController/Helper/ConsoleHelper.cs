using System;
using System.Collections.Generic;
using System.Text;

namespace DaemonController.Helper {
    public static class ConsoleHelper {
        private static ConsoleColor[] ConsoleColorArray{get;}=new ConsoleColor[]{
            ConsoleColor.Black,ConsoleColor.DarkBlue,ConsoleColor.DarkGreen,ConsoleColor.DarkCyan,
            ConsoleColor.DarkRed,ConsoleColor.DarkMagenta,ConsoleColor.DarkYellow,ConsoleColor.Gray,
            ConsoleColor.DarkGray,ConsoleColor.Blue,ConsoleColor.Green,ConsoleColor.Cyan,
            ConsoleColor.Red,ConsoleColor.Magenta,ConsoleColor.Yellow,ConsoleColor.White};

        public static void ColorWrite(String text){
            if(String.IsNullOrWhiteSpace(text)){return;}
            if(text.IndexOf('§',StringComparison.OrdinalIgnoreCase)<0){
                Console.Write(text);
                return;
            }
            Boolean flag=false;
            ConsoleColor consoleColor=Console.ForegroundColor;
            for(Int32 i1=0;i1<text.Length;i1++){
                if(text[i1]=='§') {
                    flag=true;
                    continue;
                }
                if(flag){
                    switch(text[i1]) {
                        case '0':Console.ForegroundColor=ConsoleColorArray[0];break;
                        case '1':Console.ForegroundColor=ConsoleColorArray[1];break;
                        case '2':Console.ForegroundColor=ConsoleColorArray[2];break;
                        case '3':Console.ForegroundColor=ConsoleColorArray[3];break;
                        case '4':Console.ForegroundColor=ConsoleColorArray[4];break;
                        case '5':Console.ForegroundColor=ConsoleColorArray[5];break;
                        case '6':Console.ForegroundColor=ConsoleColorArray[6];break;
                        case '7':Console.ForegroundColor=ConsoleColorArray[7];break;
                        case '8':Console.ForegroundColor=ConsoleColorArray[8];break;
                        case '9':Console.ForegroundColor=ConsoleColorArray[9];break;
                        case 'a':Console.ForegroundColor=ConsoleColorArray[10];break;
                        case 'b':Console.ForegroundColor=ConsoleColorArray[11];break;
                        case 'c':Console.ForegroundColor=ConsoleColorArray[12];break;
                        case 'd':Console.ForegroundColor=ConsoleColorArray[13];break;
                        case 'e':Console.ForegroundColor=ConsoleColorArray[14];break;
                        case 'f':Console.ForegroundColor=ConsoleColorArray[15];break;
                        case '|':Console.ForegroundColor=consoleColor;break;
                    }
                    flag=false;
                    continue;
                }
                Console.Write(text[i1]);
            }
        }
    }
}
