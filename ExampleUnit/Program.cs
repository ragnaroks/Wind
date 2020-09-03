using System;
using System.Collections;
using System.Threading;

namespace ExampleUnit {
    public static class Program {
        private static Int32 LoopCount{get;set;}=0;

        public static void Main(){
            while(true){
                LoopCount++;
                try {
                    IDictionary dictionary=Environment.GetEnvironmentVariables();
                    foreach (DictionaryEntry item in dictionary) {
                        Console.WriteLine($"env:{item.Key}=>{item.Value}");
                        if(item.Key.ToString()=="flag" && item.Value.ToString()=="1"){
                            Byte[] bytes=new Byte[LoopCount*1024];
                            Console.WriteLine($"{LoopCount}=>{LoopCount*1024}");
                        }
                        if(item.Key.ToString()=="beep" && item.Value.ToString()=="true"){ Console.Beep(); }
                    }
                }catch(Exception exception) {
                    Console.WriteLine($"[ERROR]{exception.Message}");
                }
                SpinWait.SpinUntil(()=>false,1000);
            }
        }
    }
}
