using System;
using System.Collections.Generic;
using System.Threading;

namespace ExampleUnit {
    public static class Program {
        private static Int32 LoopCount{get;set;}=0;

        public static void Main(){
            while(true){
                LoopCount++;
                Byte[] bytes=new Byte[LoopCount*1024];
                Console.WriteLine($"{LoopCount}=>{LoopCount*1024}");
                Console.Beep();
                SpinWait.SpinUntil(()=>false,1000);
            }
        }
    }
}
