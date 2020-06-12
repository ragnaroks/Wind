using System;
using System.Threading;

namespace ExampleUnit {
    public static class Program {
        public static Int32 LoopCount{get;private set;}=0;

        public static void Main(){
            while(true){
                if(LoopCount>60) {
                    //throw new ApplicationException("throw test");
                    Environment.Exit(1);
                } else {
                    Console.Beep();
                    LoopCount++;
                }
                SpinWait.SpinUntil(()=>false,1000);
            }
        }
    }
}
