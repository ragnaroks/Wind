using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security","CA5350:不要使用弱加密算法",Justification = "<挂起>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1308:将字符串规范化为大写",Justification = "<挂起>")]
        public static String SHA1(this String thisString,Boolean upCase=false) {
            System.Security.Cryptography.SHA1 sha1=System.Security.Cryptography.SHA1.Create();
            Byte[] bytes=sha1.ComputeHash(Encoding.UTF8.GetBytes(thisString));
            sha1.Dispose();
            String hash=BitConverter.ToString(bytes).Replace("-","",StringComparison.OrdinalIgnoreCase);
            return upCase?hash:hash.ToLowerInvariant();
        }


        /// <summary>
        /// 获取字符串的MD5
        /// </summary>
        /// <param name="thisString"></param>
        /// <param name="upCase">是否大写</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security","CA5351:不要使用损坏的加密算法",Justification = "<挂起>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization","CA1308:将字符串规范化为大写",Justification = "<挂起>")]
        public static String MD5(this String thisString,Boolean upCase=false) {
            System.Security.Cryptography.MD5 md5=System.Security.Cryptography.MD5.Create();
            Byte[] bytes=md5.ComputeHash(Encoding.UTF8.GetBytes(thisString));
            md5.Dispose();
            String hash=BitConverter.ToString(bytes).Replace("-","",StringComparison.OrdinalIgnoreCase);
            return upCase?hash:hash.ToLowerInvariant();
        }
        #endregion

        #region System.IO.FileInfo
        /// <summary>
        /// 取文件名的名称部分
        /// </summary>
        /// <param name="thisFileInfo"></param>
        /// <returns></returns>
        public static String GetOriginName(this FileInfo thisFileInfo){
            if(thisFileInfo==null){return String.Empty;}
            return thisFileInfo.Name.Replace(thisFileInfo.Extension,"",StringComparison.OrdinalIgnoreCase);
        }
        #endregion

        #region System.Diagnostics.Process
        /// <summary>
        /// 结束进程树
        /// </summary>
        /// <param name="thisProcess"></param>
        /// <exception cref="Exception"></exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage","CA2200:再次引发以保留堆栈详细信息。",Justification = "<挂起>")]
        public static void KillTree(this Process thisProcess){
            if(thisProcess==null){return;}
            Int32 thisProcessId=thisProcess.Id;
            try {
                thisProcess.Kill();
            }catch(Exception exception) {
                throw exception;
            }
            //这里用异步去结束子进程,避免子进程冒泡到主线程,导致多个结束通知
            Task.Run(()=>{
                Helpers.ProcessHelper.KillChildProcessByParentProcess(thisProcessId);
            });
        }
        #endregion

        #region Protocol.*
        public static Byte[] ToBytes<T>(this T thisProtobuf) where T:Google.Protobuf.IMessage{
            Google.Protobuf.IMessage protobuf=(Google.Protobuf.IMessage)thisProtobuf;
            if(protobuf==null){return null;}
            Byte[] bytes=new Byte[protobuf.CalculateSize()];
            Google.Protobuf.CodedOutputStream codedOutputStream=new Google.Protobuf.CodedOutputStream(bytes);
            protobuf.WriteTo(codedOutputStream);
            codedOutputStream.Dispose();
            return bytes;
        }
        #endregion
    }
}