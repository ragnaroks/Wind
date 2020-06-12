> Wind2 is a daemon service  
> this project designed to create a `systemd` for windows

### Projects
- `Daemon` is Wind2's windows service
- `DaemonController` is Wind2's local command-line controller based pipeline
- `ExampleUnit` is an example unit to test functionality

****

### Install
0. require [dotnet core runtime 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
1. download released package file and unzip to local disk,i suggest `C:\ProgramData\Wind2\`
2. open an **Administrator** privilege command window
3. execute `Daemon.exe action:install`
4. create a unit file into `.\Units\` directory,example is down below
5. execute `sc.exe start Wind2` to start Wind2 daemon service
6. under normal circumstances,your unit has been started

**if you be worry about high level privilege,you can use `services.msc` to change to low level privilege,but maybe some thing wrong**

****

### Uninstall
1. open an **Administrator** privilege command window
2. execute `sc.exe stop Wind2` to stop Wind2 daemon service and all units
3. change directory into Wind2 directory
4. execute `Daemon.exe action:uninstall`
5. delete Wind2 directory

****

### Unit settings file example
```json
{
    // unit display name,must set up
    "Name": "Example Unit",
    // unit display description,must set up
    "Description": "Example Unit Description",
    // unit type,must set up
    // 0 is simple,1 is fork
    "Type": 0,
    // unit executeable file path,must set up
    "AbsoluteExecutePath": "D:\\Projects\\Wind2\\ExampleUnit\\bin\\Debug\\netcoreapp3.1\\ExampleUnit.exe",
    // unit work directory,must set up
    "AbsoluteWorkDirectory": "D:\\Projects\\Wind2\\ExampleUnit\\bin\\Debug\\netcoreapp3.1",
    // unit execute arguments
    "Arguments": null,
    // unit will start after Wind2 daemon service started
    "AutoStart": true,
    // unit start delay seconds,only for auto start
    "AutoStartDelay": 3,
    // unit will restart when unit exit by exception
    "RestartWhenException": false,
    // monitor unit network usage
    // 0 is not monitor,1 is only download,2 is only upload,3 is both
    "MonitorNetworkUsage": 0
}
```

### Wind2 settings
> `.\Data\AppSettings.json`
```json
{
    // enable remote control
    "EnableRemoteControl": false,
    // remote control listen address,only IPv4
    "RemoteControlListenAddress": "localhost",
    // remote control listen port,1024 < PORT < 65535
    "RemoteControlListenPort": 3721,
    // remote control key,use by validate websocket connections
    "RemoteControlKey": "https://github.com/ragnaroks/Wind2"
}
```

****

### Attention
- unit will inherited Wind2 daemon service privilege,only host your trust application
- if Wind2 exit by excetion,maybe not stop all units,you should stop them manual
- if add an unit file,but not did what you want,check in `.\Logs\`
- no plan to use `wss://`,you can use nginx proxy

****

### Compatible
- [iPEX](https://github.com/ragnaroks/ipex)
- [aria2](https://github.com/aria2/aria2)
- [nginx](https://github.com/nginx/nginx)
- [v2ray](https://github.com/v2ray/v2ray-core)
- [kcptun](https://github.com/xtaci/kcptun)
- [frp](https://github.com/fatedier/frp)

### Imcompatible
- GUI application
- any hold std-in application (like minecraft server)
- [webd](https://webd.cf/)
