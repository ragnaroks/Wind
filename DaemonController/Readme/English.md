> Wind daemon service command-line controller

### Install
0. require [dotnet core runtime 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
1. download released package file and unzip to local disk,i suggest `C:\ProgramData\WindController\`
2. add install directory to environment variable
3. open an **Administrator** privilege command window
4. execute `windctl.exe` without any argument

### Uninstall
just delete files

### Commands
> **windctl**'s version must equal daemon service's version

- windctl version
  > print this tool's version
- windctl status **\<unitKey\>**
  > get unit's status
- windctl start **\<unitKey\>**
  > start unit
- windctl stop **\<unitKey\>**
  > stop unit
- windctl restart **\<unitKey\>**
  > restart unit
- windctl load **\<unitKey\>**
  > try load/update unit's settings from file
- windctl remove **\<unitKey\>**
  > stop unit and remove it,it can not be start again
- windctl start-all
  > start all unit
- windctl stop-all
  > stop all unit
- windctl restart-all
  > restart all unit
- windctl load-all
  > try load/update all units's settings from file
- windctl remove-all
  > stop all unit and remove them,they can not be start again
- windctl daemon-version
  > get daemon service's version
- windctl daemon-shutdown
  > shutdown daemon service

### Showcase
![windctl status <unitKey>](status.png)