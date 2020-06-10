@echo off
sc.exe create Wind2Daemon binpath= %~dp0Daemon.exe
sc.exe start Wind2Daemon