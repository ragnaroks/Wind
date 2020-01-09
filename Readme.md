### 关于
由于种种原因,我需要将某个可执行文件作为服务运行,并且能随时控制运行与否  
这期间找了一些解决方案,比如srvany/nssm/srvWrap等,但是都不满意  
于是我产生了写一个类似于linux下的systemd的项目的想法  

### 项目
- `Host`Wind2服务主机,用于托管应用程序(不再维护),内存使用越8M
- `Controller`Wind2桌面普通控制端,链接到服务主机后即可远程控制(不再维护)
- `Daemon`Wind2服务主机,用dotnet core 3.1重新实现并针对以前有缺陷的地方进行优化,内存使用约12M
- `WebController`Wind2网页控制端 [demo(只能管理本机)](http://w2c.ragnaroks.org/)
**推荐使用`Daemon`+`WebController`的组合**
![WebController](https://i.imgur.com/c0XZAUp.png)

### 安装
- 框架依赖=>使用管理员权限执行`dotnet Daemon.dll action:install`
- 独立=>使用管理员权限执行`Daemon.exe action:install`
- 可能需要手动去服务控制面板(services.msc)启用 Wind2 服务

### 卸载
- 框架依赖=>使用管理员权限执行`dotnet Daemon.dll action:uninstall`
- 独立=>使用管理员权限执行`Daemon.exe action:uninstall`

### 单元配置
**这是`Daemon`项目的单元配置,`Host`项目的单元配置请参考发布的压缩包内的示例**
单元配置文件都存放于Wind2目录下的Units文件夹中,单元配置是一个编码UTF-8的JSON文本文件,格式如下
```json
{
    //单元描述,非必须
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
    //守护进程,若为true,则应用程序不是被Wind2结束的情况下会被重新启动,某些应用程序会自行退出(比如检测到配置异常),且退出代码不等于0,可能导致无限循环启动
    "DaemonProcess": false
}
```
以上配置代表在Wind2初始化完成后,等待10秒,再启动`C:\DaemonServices\aria2-1.34.0-win-64bit-build1\aria2c.exe --conf-path="C:\DaemonServices\config.conf"`,并且设置工作目录`C:\DaemonServices\aria2-1.34.0-win-64bit-build1\`

### 全局配置
**这是`Daemon`项目的全局配置,`Host`项目的全局配置请参考发布的压缩包内的示例**
全局配置是一个名为**AppSettings.json**的JSON文本文件,编码UTF-8,位于Wind2根目录下,格式如下
```json
{
    //是否启用远程控制,高阶用户不建议启用
    "ControlEnable": false,
    //远程控制监听地址,其中'localhost'=='127.0.0.1','any'=='0.0.0.0'
    "ControlAddress": "localhost",
    //远程控制端口,不可小于1024,默认25565
    "ControlPort": 25565,
    //远程控制密钥,任意长度任意字符,建议16字节以上,被控和控制端使用AES加密通信
    "ControlKey": "https://github.com/ragnaroks/Wind2"
}
```
**如果AppSettings.json文件不存在或格式错误,则会使用如上文本作为默认配置使用**

### 注意事项
- 被托管的应用程序不支持**交互**
- 当前不支持托管有图像界面的应用程序(可以启动,但不保证正常使用)
- 被托管的应用程序默认为"LOCAL SYSTEM"权限,建议只托管**受信任**的应用程序,后面考虑加入使用指定用户权限运行
- 如果Wind2意外退出,可能导致单元失去托管(毕竟是在服务里面开服务...),这种情况下目前只能手动结束单元进程
- **强烈建议注册为系统服务运行,停止服务后被托管的单元会跟随停止运行,若作为普通控制台应用运行,被托管的单元将失去托管,需要用户自行关闭**
- 托管单元格式错误/应用程序绝对路径无法访问/工作目录不存在 的情况下,单元配置文件会被忽略
- 托管单元配置文件中,务必使用绝对路径,Wind2当前不支持相对路径

### 已知兼容列表
- [aria2](https://github.com/aria2/aria2)
- [v2ray](https://github.com/v2ray/v2ray-core)

### 已知不兼容列表
- 需要"交互"的应用程序,控制台应用程序的话就是独占stdIn的(比如MC服务端),以及大多数图形界面应用程序
- [nginx](https://github.com/nginx/nginx) 此应用程序使用多进程(调度进程+工作进程)模式,结束主进程并不会像一般多进程应用程序那样让子进程一同退出 (未测试与srvany/nssm的兼容性)
- [webd](https://webd.cf/) 此应用程序可以被Wind2启动,但是会立刻自主退出,退出代码为**1**,原因不明,且若Wind2不作为服务直接作为控制台应用运行,则可以成功托管webd (另经过测试,srvany/nssm均无法托管此应用程序,都是立刻退出)
