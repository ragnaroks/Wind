using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Daemon.Helpers {
    public static class UnitManageModuleHelper {
        /// <summary>
        /// 获取单元名
        /// </summary>
        /// <param name="unitSettingsFileInfo"></param>
        /// <returns></returns>
        public static String GetUnitKey(FileInfo unitSettingsFileInfo){
            if(unitSettingsFileInfo==null){return null;}
            return unitSettingsFileInfo.Name.Replace(unitSettingsFileInfo.Extension,"",StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 解析单元配置
        /// </summary>
        /// <param name="unitSettingsFilePath"></param>
        /// <param name="parsedUnitSettings"></param>
        /// <returns>解析完的单元</returns>
        public static Entities.Common.UnitSettings ParseUnitSettingsFile(FileInfo unitSettingsFileInfo) {
            FileStream fs;
            try {
                fs=unitSettingsFileInfo.Open(FileMode.Open,FileAccess.Read,FileShare.Read);
            }catch(Exception exception){
                LoggerModuleHelper.TryLog("Helpers.UnitManageModuleHelper.ParseUnitSettingsFileAsync[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"解析失败\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return null;
            }
            if(fs==null || fs.Length>4096){
                LoggerModuleHelper.TryLog("Helpers.UnitManageModuleHelper.ParseUnitSettingsFileAsync[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"解析失败,文件字节长度异常");
                return null;
            }
            Byte[] buffer=new Byte[fs.Length];
            fs.Read(buffer,0,(Int32)fs.Length);
            fs.Dispose();
            String jsonString=Encoding.UTF8.GetString(buffer).Trim();
            if(String.IsNullOrWhiteSpace(jsonString)){
                LoggerModuleHelper.TryLog("Helpers.UnitManageModuleHelper.ParseUnitSettingsFileAsync[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"解析失败,文件内容为空");
                return null;
            }
            Entities.Common.UnitSettings unitSettings;
            try {
                unitSettings=JsonSerializer.Deserialize<Entities.Common.UnitSettings>(jsonString);
            }catch(Exception exception){
                LoggerModuleHelper.TryLog("Helpers.UnitManageModuleHelper.ParseUnitSettingsFile[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"解析失败\n异常信息: {exception.Message}\n异常堆栈: {exception.StackTrace}");
                return null;
            }
            if(unitSettings==null) {
                LoggerModuleHelper.TryLog("Helpers.UnitManageModuleHelper.ParseUnitSettingsFile[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"解析失败,文件反序列化失败");
                return null;
            }
            if(String.IsNullOrWhiteSpace(unitSettings.AbsoluteExecutePath)) {
                LoggerModuleHelper.TryLog("Helpers.UnitManageModuleHelper.ParseUnitSettingsFile[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"的\"AbsoluteExecutePath\"配置项为空");
                return null;
            }
            if(!File.Exists(unitSettings.AbsoluteExecutePath)) {
                LoggerModuleHelper.TryLog("Helpers.UnitManageModuleHelper.ParseUnitSettingsFile[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"的\"AbsoluteExecutePath\"配置项无效,可执行文件不存在或没有权限");
                return null;
            }
            if(String.IsNullOrWhiteSpace(unitSettings.AbsoluteWorkDirectory)) {
                LoggerModuleHelper.TryLog("Helpers.UnitManageModuleHelper.ParseUnitSettingsFile[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"的\"AbsoluteWorkDirectory\"配置项为空");
                return null;
            }
            if(!Directory.Exists(unitSettings.AbsoluteWorkDirectory)) {
                Program.LoggerModule.Log("Helpers.UnitManageModuleHelper.ParseUnitSettingsFile[Error]",$"单元\"{unitSettingsFileInfo.FullName}\"的\"AbsoluteWorkDirectory\"配置项无效,可执行文件工作目录不存在或没有权限");
                return null;
            }
            Program.LoggerModule.Log("Helpers.UnitManageModuleHelper.ParseUnitSettingsFile",$"单元\"{unitSettingsFileInfo.FullName}\"解析完成");
            return unitSettings;
        }
    }
}
