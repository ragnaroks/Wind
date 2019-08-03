#### 安装
- 框架依赖=>使用管理员权限执行`dotnet Host.dll action:install`
- 独立=>使用管理员权限执行`Host.exe action:install`

#### 卸载
- 框架依赖=>使用管理员权限执行`dotnet Host.dll action:uninstall`
- 独立=>使用管理员权限执行`Host.exe action:uninstall`

#### 单元配置
单元配置是一个JSON文本文件,编码`ASCII(ANSI)`,格式如下
```json
{
    //应用程序绝对路径
    "AbsolutePath": "C:\\Windows\\System32\\ping.exe",
    //应用程序工作目录,部分应用程序需要此项,建议都填写
    "WorkPath": "C:\\Windows\\System32\\",
    //应用程序参数
    "Params": "hello.wind2",
    //应用程序是否自启动
    "AutoStart": true,
    //应用程序自启延迟,单位秒
    "AutoStartDelay": 10,
    //是否控制台应用程序,此项当前无效,详见下方
    "IsConsoleApp": false,
    //是否记录日志,此项当前无效,详见下方
    "EnableLogger": false
}
```
以上配置代表在Wind2初始化完成后,等待10秒,再启动`C:\Windows\System32\ping.exe`,并且设置工作目录`C:\Windows\System32\`  

格式错误或未填写应用程序绝对路径或应用程序文件不存在,则此单元文件会被忽略,单元配置文件都存放于Wind2目录下的Units文件夹中  

- `IsConsoleApp`如果为`true`,则该单元进程**不会有窗口**,且stdIO会被重定向  
- `EnableLogger`如果为`true`,则该单元会记录日志
- 当前不支持托管有图像界面的应用程序