@echo off
copy ..\server\bin\debug\server.exe .
soapsuds -ia:server -nowp -oa:generated_meta.dll
del server.exe
