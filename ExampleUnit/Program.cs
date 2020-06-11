using System;
using System.Threading;

namespace ExampleUnit {
    public static class Program {
        public static void Main(){
            while(true){
                Console.Beep();
                SpinWait.SpinUntil(()=>false,1000);
            }
        }
    }
}
