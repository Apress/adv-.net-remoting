copy ..\server\bin\debug\server.exe .
soapsuds -ia:server -oa:generated_metadata.dll
del server.exe