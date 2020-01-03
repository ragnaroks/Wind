### 关于
由于种种原因,我需要将某个可执行文件作为服务运行,并且能随时控制运行与否  
这期间找了一些解决方案,比如srvany/nssm/srvWrap等,但是都不满意  
于是我产生了写一个类似于linux下的systemd的项目的想法

这个想法转化为代码后,就是Wind项目了,Wind是一个Win32GUI应用程序,作为第一启动项启动后,拉起其它被托管的应用程序,并且可以在图形界面上随时控制

但是,实际使用一年多以后,发现还是有很多问题,比如类似v2ray的应用程序,我想要它在登录会话之前就启动,又比如syncthing,我想要它在开机一定时间之后再启动,以免过早占用大量磁盘IO

于是有了Wind2,只有控制台应用程序,(强烈建议)可注册为Windows服务运行,达到了开机就托管的目的,且加入了自启延迟,可简单的对托管单元进行排序,比如开机3秒后启动v2ray,然后30秒后启动syncthing,syncthing就可以使用v2ray提供的代理进行同步

对于高阶用户,Wind2已经够用了,但是很多使用windows操作系统的用户,根本不会改配置文件,设置系统服务等操作,于是我又写了个控制端,在图形界面下提供对Wind2服务主机的控制(PS 欢迎有能力的扣德尔实现类似单用户多服务器的控制端)

### 项目
- `Host`Wind2服务主机,用于托管应用程序(不再维护)
- `Controller`Wind2控制端,链接到服务主机后即可远程控制(不再维护,且有缺陷)
- `Daemon`Wind2服务主机,算是用dotnet core 3.1重新实现了一版+优化
- `WebController`Wind2网页控制端(未开始)
求稳定的用户可使用`Host`+`Controller`的老搭配,运行1年多非常稳定,但`Controller`有部分缺陷未处理  
临时尝鲜可以直接使用`Daemon`,作为单机进程托管应该是完全够用了

### 安装
- 框架依赖=>使用管理员权限执行`dotnet Host.dll action:install`
- 独立=>使用管理员权限执行`Host.exe action:install`
- 可能需要手动去服务控制面板(services.msc)启用 Wind2 服务

### 卸载
- 框架依赖=>使用管理员权限执行`dotnet Host.dll action:uninstall`
- 独立=>使用管理员权限执行`Host.exe action:uninstall`

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
    "AutoStartDelay": 10
}
```
以上配置代表在Wind2初始化完成后,等待10秒,再启动`C:\DaemonServices\aria2-1.34.0-win-64bit-build1\aria2c.exe --conf-path="C:\DaemonServices\config.conf"`,并且设置工作目录`C:\DaemonServices\aria2-1.34.0-win-64bit-build1\`

部分应用程序支持在参数里面使用相对路径(Wind2不支持使用相对路径),建议都使用绝对路径

格式错误或未填写应用程序绝对路径或应用程序文件不存在,则此单元文件会被忽略

### 全局配置
**这是`Daemon`项目的全局配置,`Host`项目的全局配置请参考发布的压缩包内的示例**
全局配置是一个名为**AppSettings.json**的JSON文本文件,编码UTF-8,位于Wind2根目录下,格式如下
```json
{
    //日志级别,当前无效
    "LogLevel": 0,
    //是否启用远程控制,高阶用户不建议启用
    "ControlEnable": false,
    //远程控制监听IPv4地址,其中'localhost'=='127.0.0.1','any'=='0.0.0.0'
    "ControlAddressV4": "localhost",
    //远程控制监听IPv6地址,其中'localhost'=='::1','any'=='::',当前无效
    "ControlAddressV6": "localhost",
    //远程控制端口,不可小于1024,默认25565
    "ControlPort": 25565,
    //远程控制密钥,任意长度任意字符,建议16字节以上,被控和控制端使用AES加密通信
    "ControlKey": "https://github.com/ragnaroks/Wind2"
}
```
如果AppSettings.json文件不存在或格式错误,则会使用如上文本作为默认配置使用

### 注意事项
- 被托管的应用程序不支持**交互**
- 当前不支持托管有图像界面的应用程序(可以启动,但不保证正常使用)
- 被托管的应用程序默认为"LOCAL SYSTEM"权限,建议只托管**受信任**的应用程序,后面考虑加入使用指定用户权限运行
- 如果Wind2意外退出,可能导致单元失去托管(毕竟是在服务里面开服务...),这种情况下目前只能手动结束单元进程
- **强烈建议注册为系统服务运行,停止服务后被托管的单元会跟随停止运行,若作为普通控制台应用运行,被托管的单元将失去托管,需要用户自行关闭**
