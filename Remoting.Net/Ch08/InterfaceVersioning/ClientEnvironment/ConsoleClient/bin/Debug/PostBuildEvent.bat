@echo off
copy D:\Workshop\RemotingClients\ConsoleClient\\Remote*.config D:\Workshop\RemotingClients\ConsoleClient\bin\Debug\
if errorlevel 1 goto CSharpReportError
goto CSharpEnd
:CSharpReportError
echo Project error: A tool returned an error code from the build event
exit 1
:CSharpEnd