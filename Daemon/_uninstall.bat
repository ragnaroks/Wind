@echo off
net session >nul 2>&1
IF %errorLevel% EQU 0 (
    %~dp0Daemon.exe action:uninstall
) ELSE (
    echo require Administrator
)
pause