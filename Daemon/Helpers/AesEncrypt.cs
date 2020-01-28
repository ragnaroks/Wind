using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Daemon.Helpers{
    /// <summary>AES 加解密</summary>
    public static class AesEncrypt {
        /*
        /// <summary>IV=>"730csgo7355608c4"</summary>
        private static readonly Byte[] IV=new Byte[16]{55,51,48,99,115,103,111,55,51,53,53,54,48,56,99,52};
        */

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Byte[] Encrypt(String key,String text) {
            var aes=AesManaged.Create();
            //aes.IV=IV;
            aes.BlockSize=128;
            aes.KeySize=128;
            aes.Key=Encoding.UTF8.GetBytes(key.MD5().Substring(16));
            aes.Mode=CipherMode.ECB;
            aes.Padding=PaddingMode.Zeros;
            ICryptoTransform crypto=aes.CreateEncryptor(aes.Key,aes.IV);
            aes.Dispose();
            Byte[] bytes = Encoding.UTF8.GetBytes(text);
            Byte[] result=crypto.TransformFinalBlock(bytes,0,bytes.Length);
            crypto.Dispose();
            return result;
        }


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design","CA1031:不捕获常规异常类型",Justification = "<挂起>")]
        public static String Decrypt(String key,Byte[] bytes) {
            if(bytes==null || bytes.Length<1){return null;}
            var aes=AesManaged.Create();
            //aes.IV=IV;
            aes.BlockSize=128;
            aes.KeySize=128;
            aes.Key=Encoding.UTF8.GetBytes(key.MD5().Substring(16));
            aes.Mode=CipherMode.ECB;
            aes.Padding=PaddingMode.Zeros;
            ICryptoTransform crypto=aes.CreateDecryptor(aes.Key,aes.IV);
            aes.Dispose();
            Byte[] result;
            try {
                result=crypto.TransformFinalBlock(bytes,0,bytes.Length);
            }catch(Exception exception){
                Program.LoggerModule.Log("Helpers.AesEncrypt.Decrypt[Error]",$"数据解密异常,{exception.Message},{exception.StackTrace}");
                ConsoleColor cc=Console.ForegroundColor;
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine($"Helpers.AesEncrypt.Decrypt => {exception.Message} | {exception.StackTrace}");
                Console.ForegroundColor=cc;
                return null;
            } finally {
                crypto.Dispose();
            }
            return Encoding.UTF8.GetString(result);
        }
    }
}
