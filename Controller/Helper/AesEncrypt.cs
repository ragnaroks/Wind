using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Controller.Helper {
    public class AesEncrypt {
        private static readonly Byte[] IV=new Byte[16]{55,51,48,99,115,103,111,55,51,53,53,54,48,56,99,52};//"730csgo7355608c4"
        
        public static Boolean Encrypt(String _key,String _text,out Byte[] _data) {
            var aes=AesManaged.Create();
            //aes.IV=IV;
            aes.BlockSize=128;
            aes.KeySize=128;
            aes.Key=Encoding.UTF8.GetBytes(_key.MD5().Substring(16));
            aes.Mode=CipherMode.ECB;
            aes.Padding=PaddingMode.Zeros;
            ICryptoTransform crypto=aes.CreateEncryptor(aes.Key,aes.IV);
            Byte[] _bytes = Encoding.UTF8.GetBytes(_text);
            _data=crypto.TransformFinalBlock(_bytes,0,_bytes.Length);
            crypto.Dispose();
            aes.Dispose();
            return true;
        }

        public static Boolean Decrypt(String _key,Byte[] _bytes,out String _data) {
            var aes=AesManaged.Create();
            //aes.IV=IV;
            aes.BlockSize=128;
            aes.KeySize=128;
            aes.Key=Encoding.UTF8.GetBytes(_key.MD5().Substring(16));
            aes.Mode=CipherMode.ECB;
            aes.Padding=PaddingMode.Zeros;
            ICryptoTransform crypto=aes.CreateDecryptor(aes.Key,aes.IV);
            Byte[] result=null;
            try {
                result=crypto.TransformFinalBlock(_bytes,0,_bytes.Length);
            }catch{
                crypto.Dispose();
                aes.Dispose();
                _data=null;
                return false;
            }
            crypto.Dispose();
            aes.Dispose();
            _data=Encoding.UTF8.GetString(result);
            return true;
        }
    }
}
