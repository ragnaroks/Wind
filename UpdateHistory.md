### v2.1.20
[new]
- unit file add "HaveChildProcesses" setting to control the unit kill all of unit's processes,it's useful for application like nginx
[other]
- notify web controller when unit stop failed

[新功能]
- 单元配置文件加入"HaveChildProcesses"选项,当停止单元时结束所有进程,此选项在托管类似于nginx的应用程序时有用
[其它]
- 单元停止失败时通知网页控制器

### v2.1.19.1
[fix]
- unit start failed but not report to web controller

[修复问题]
- 单元启动失败但没有回报网页控制器

### v2.1.19
[new]
- unit file add "DaemonProcess" setting to control the unit will auto restart when unit unexpected exit
[fix]
- web controller not update settings when request reload unit

[新功能]
- 单元配置文件加入"DaemonProcess"选项,当单元异常结束后允许wind2自动重启
[修复问题]
- 请求重载单元配置文件后没有更新网页控制器的数据

### v2.1.18
wind2 supports web controller since this version

此版本开始支持网页控制器

### v2.0.15
last old version

最后一个老版本的Wind2
