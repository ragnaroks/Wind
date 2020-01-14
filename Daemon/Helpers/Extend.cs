using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Daemon.Helpers {
    /// <summary>拓展方法</summary>
    public static class Extend {
        #region System.String
        /// <summary>
        /// 获取字符串的SHA1
        /// </summary>
        /// <param name="thisString"></param>
        /// <param name="upCase">是否大写</param>
        /// <returns></returns>
        public static String SHA1(this String thisString,Boolean upCase=false) {
            System.Security.Cryptography.SHA1 sha1=System.Security.Cryptography.SHA1.Create();
            Byte[] bytes=sha1.ComputeHash(Encoding.UTF8.GetBytes(thisString));
            String hash=BitConverter.ToString(bytes).Replace("-","");
            return upCase?hash:hash.ToLower();
        }

        /// <summary>
        /// 获取字符串的MD5
        /// </summary>
        /// <param name="thisString"></param>
        /// <param name="upCase">是否大写</param>
        /// <returns></returns>
        public static String MD5(this String thisString,Boolean upCase=false) {
            System.Security.Cryptography.MD5 md5=System.Security.Cryptography.MD5.Create();
            Byte[] bytes=md5.ComputeHash(Encoding.UTF8.GetBytes(thisString));
            String hash=BitConverter.ToString(bytes).Replace("-","");
            return upCase?hash:hash.ToLower();
        }
        #endregion

        #region System.IO.FileInfo
        /// <summary>
        /// 取文件名的名称部分
        /// </summary>
        /// <param name="thisFileInfo"></param>
        /// <returns></returns>
        public static String GetOriginName(this FileInfo thisFileInfo)=>thisFileInfo.Name.Replace(thisFileInfo.Extension,"");
        #endregion

        #region System.Diagnostics.Process
        /// <summary>
        /// 结束进程树
        /// </summary>
        /// <param name="thisProcess"></param>
        /// <exception cref="Exception"></exception>
        public static void KillTree(this Process thisProcess){
            Int32 thisProcessId=thisProcess.Id;
            try {
                thisProcess.Kill();
            }catch(Exception exception) {
                throw exception;
            }
            //忽略结束子进程时的异常
            Helpers.ProcessHelper.KillChildProcessByParentProcess(thisProcessId);
        }
        #endregion

        #region Protocol.*
        public static Byte[] ToBytes(this Protocol.WebSocketServerResponseAfterOnOpen thisProtobuf) {
            Byte[] bytes=new Byte[thisProtobuf.CalculateSize()];
            Google.Protobuf.CodedOutputStream codedOutputStream=new Google.Protobuf.CodedOutputStream(bytes);
            thisProtobuf.WriteTo(codedOutputStream);
            codedOutputStream.Dispose();
            return bytes;
        }
        public static Byte[] ToBytes(this Protocol.WebSocketServerResponseValidateControlKey thisProtobuf) {
            Byte[] bytes=new Byte[thisProtobuf.CalculateSize()];
            Google.Protobuf.CodedOutputStream codedOutputStream=new Google.Protobuf.CodedOutputStream(bytes);
            thisProtobuf.WriteTo(codedOutputStream);
            codedOutputStream.Dispose();
            return bytes;
        }
        #endregion
    }
}