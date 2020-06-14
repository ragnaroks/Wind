using System;
using System.Collections.Generic;
using System.Threading;

namespace ExampleUnit {
    public static class Program {
        public static Int32 LoopCount{get;private set;}=0;

        public static void Main(){
            while(true){
                if(LoopCount>60) {
                    //throw new ApplicationException("throw test");
                    //Environment.Exit(1);
                    LoopCount=0;
                } else {
                    Byte[] bytes=new Byte[LoopCount*1024*1024];
                    Console.WriteLine(LoopCount*1024*1024);
                    Console.Beep();
                    LoopCount++;
                }
                SpinWait.SpinUntil(()=>false,1000);
            }
        }
    }
}
