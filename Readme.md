#### 进度
- [ ] Udp协议被控 => WIP
- [ ] Udp协议控制端
- [ ] 单元可选是否需要Wind2的日志
- [ ] WebSocket协议控制(控制端无计划)
- [ ] 单元指定权限(用户)运行

#### 安装
- 框架依赖=>使用管理员权限执行`dotnet Host.dll action:install`
- 独立=>使用管理员权限执行`Host.exe action:install`
- 可能需要手动去服务控制面板(services.msc)启用 Wind2 服务

#### 卸载
- 框架依赖=>使用管理员权限执行`dotnet Host.dll action:uninstall`
- 独立=>使用管理员权限执行`Host.exe action:uninstall`

#### 单元配置
单元配置是一个JSON文本文件,编码`ASCII(ANSI)`,格式如下
```json
{
    //应用程序绝对路径
    "AbsolutePath": "C:\\Windows\\System32\\nslookup.exe",
    //应用程序工作目录,部分应用程序需要此项,建议都填写
    "WorkPath": "C:\\Windows\\System32\\",
    //应用程序参数
    "Params": "-qt=A www.ragnaroks.org",
    //应用程序是否自启动
    "AutoStart": true,
    //应用程序自启延迟,单位秒
    "AutoStartDelay": 10,
    //是否记录日志,此项当前无效,详见下方
    "EnableLogger": false
}
```
以上配置代表在Wind2初始化完成后,等待10秒,再启动`C:\Windows\System32\nslookup.exe -qt=A www.ragnaroks.org`,并且设置工作目录`C:\Windows\System32\`  

格式错误或未填写应用程序绝对路径或应用程序文件不存在,则此单元文件会被忽略,单元配置文件都存放于Wind2目录下的Units文件夹中  

`EnableLogger`如果为`true`,则该单元会记录**来自Wind2的**日志,和应用程序自身日志功能无关,**当前强制记录**


#### 全局配置
全局配置是一个名为**AppSettings.json**的JSON文本文件,编码`ASCII(ANSI)`,位于二进制根目录下,格式如下
```json
{
    //日志级别,当前无效
    "LogLevel": 0,
    //是否启用被控,高阶用户不建议启用
    "ControlEnable": false,
    //被控监听IPv4地址,其中'localhost'只监听环回,'any'监听所有
    "ControlAddress": "localhost",
    //被控端口,不可小于1024
    "ControlPort": 27015,
    //被控密钥,任意长度任意字符,建议16字节以上,被控和控制端使用AES加密通信
    "ControlKey": "https://github.com/ragnaroks/Wind2"
}
```

#### 注意事项
- 被托管的应用程序默认不支持**交互**,需要自行在`services.msc`启用
- 当前不支持托管有图像界面的应用程序
- 被托管的应用程序默认为LOCAL SYSTEM权限,建议只托管**受信任**的应用程序