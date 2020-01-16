### 关于
由于种种原因,我需要将某个可执行文件作为服务运行,并且能随时控制运行与否  
这期间找了一些解决方案,比如srvany/nssm/srvWrap等,但是都不满意  
于是我产生了写一个类似于linux下的systemd的项目的想法  

### 子项目
- ~~`Host`Wind2服务主机,用于托管应用程序(不再维护)~~
- ~~`Controller`Wind2桌面普通控制端,链接到服务主机后即可远程控制(不再维护)~~
- `Daemon`Wind2服务主机
- `WebController`Wind2网页控制端 [offical(当前只能管理本机)](http://w2c.ragnaroks.org/)

![WebController](https://i.imgur.com/rYLQ2f2.png)

### 安装
如果主机已安装[dotnet core runtime 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)则可以直接在[release](https://github.com/ragnaroks/Wind2/releases)下载最新发布包,并用管理员权限指向命令行`Daemon.exe action:install`或`dotnet Daemon.dll action:install`,完成后可能需要手动去服务控制面板(services.msc)启用Wind2服务  
如果本机还未安装运行时或不想安装,可以自行拉取源码以独立模式编译发布,可直接运行不依赖运行时

### 卸载
使用管理员权限执行`Daemon.exe action:uninstall`或`dotnet Daemon.dll action:uninstall`

### 单元配置
单元配置文件都存放于Wind2目录下的Units文件夹中,单元配置是一个编码UTF-8的JSON文本文件,单元名称就是文件名,格式如下
```json
{
    //单元描述,必须
    "Description": "aria2c v1.34.0 x64"
    //单元可执行文件绝对路径,必须
    "ExecuteAbsolutePath": "C:\\DaemonServices\\aria2-1.34.0-win-64bit-build1\\aria2c.exe",
    //单元可执行文件工作绝对目录,必须
    "WorkAbsoluteDirectory": "C:\\DaemonServices\\aria2-1.34.0-win-64bit-build1\\",
    //单元可执行文件参数,如果参数中涉及引号建议仔细校对,非必须
    "ExecuteParams": "--conf-path=\"C:\\Program Files\\aria2\\config.conf\"",
    //应用程序是否自启动,非必须,若不提供则默认不自启
    "AutoStart": false,
    //应用程序自启延迟,单位秒,非必须,若不提供则默认10秒
    "AutoStartDelay": 10,
    //守护进程,若为true,则应用程序不是被Wind2结束的情况下会被重新启动,某些应用程序会自行退出(比如检测到配置异常),且退出代码不等于0,可能导致无限循环启动,除非应用程序本身设计有误,否则不建议设置为true
    "DaemonProcess": false,
    //应用程序是否会派生子进程(比如nginx),若为true则在停止单元时会连同子进程一起结束
    "HaveChildProcesses": false
}
```
以上配置代表在Wind2初始化完成后,等待10秒,再启动`C:\DaemonServices\aria2-1.34.0-win-64bit-build1\aria2c.exe --conf-path="C:\DaemonServices\config.conf"`,并且设置工作目录`C:\DaemonServices\aria2-1.34.0-win-64bit-build1\`

### 全局配置
全局配置是一个名为**AppSettings.json**且编码为UTF-8的JSON文本文件,位于Wind2根目录下,格式如下
```json
{
    //是否启用远程控制,高阶用户/高安全需求用户不建议启用
    "ControlEnable": false,
    //远程控制监听地址,其中'localhost'=='127.0.0.1','any'=='0.0.0.0'
    "ControlAddress": "localhost",
    //远程控制端口,不可小于1024,默认25565
    "ControlPort": 25565,
    //远程控制密钥,任意长度任意字符,建议16字节以上
    "ControlKey": "https://github.com/ragnaroks/Wind2"
}
```
**如果AppSettings.json文件不存在或格式错误,则会使用如上文本作为默认配置使用**

### 注意事项
- 被托管的应用程序继承Wind2的用户权限,默认为"LOCAL SYSTEM"权限,建议只托管**受信任**的应用程序或修改Wind2服务的用户权限,暂未考虑自定义单元用户权限
- 如果Wind2意外退出,可能导致单元失去托管(毕竟是在服务里面开服务),这种情况下目前只能手动结束单元进程
- 强烈建议注册为系统服务运行,停止服务后被托管的单元会跟随停止运行,若作为普通控制台应用运行,被托管的单元将失去托管,需要用户自行关闭
- 托管单元格式错误/应用程序绝对路径无法访问/工作目录不存在 的情况下,单元配置文件会被忽略,若发现托管单元有丢失,请查看日志
- 托管单元配置文件中,务必使用绝对路径,Wind2当前不支持相对路径
- **如果需要加密信道流量,建议使用nginx反代websocket并安装证书**,暂不考虑直接在Wind2 Daemon上直接监听wss

### 已知兼容列表
- [aria2](https://github.com/aria2/aria2)
- [v2ray](https://github.com/v2ray/v2ray-core)
- [nginx](https://github.com/nginx/nginx)

### 已知不兼容列表
- 任意图形界面应用程序
- 任意独占stdin的控制台应用程序(比如minecraft server)
- [webd](https://webd.cf/) 可以被Wind2启动,但是会立刻自主退出,退出代码为**1**,原因不明,且若Wind2不注册服务直接作为控制台应用运行,则可以成功托管webd (另经过测试,srvany/nssm均无法托管此应用程序,都是立刻退出)
