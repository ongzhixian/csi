# Setup

## Set version of .NET Core SDK to use

REM Create global.json file; this sets the version of .NET Core SDK to use
dotnet new globaljson --sdk-version 2.1.603

## Git setup

git config user.name "xxx xxxx"
git config user.email "xxx@xxx.xxx"

## omnisharp.json

[info]: OmniSharp.MSBuild.Discovery.MSBuildLocator
        Located 3 MSBuild instance(s)
            1: DeveloperConsole 15.9.11 -                     "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\Current\Bin"
            2: Visual Studio Enterprise 2017 15.9.28307.586 - "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\Current\Bin"
            3: StandAlone 15.0 - "C:\Users\zhixian\.vscode\extensions\ms-vscode.csharp-1.19.0\.omnisharp\1.32.18\.msbuild\Current\Bin"
...
OmniSharp.MSBuild.ProjectManager
        Failed to load project file 'd:\src\github.com\ongzhixian\csi\Csi.WebApp\Csi.WebApp.csproj'.
d:\src\github.com\ongzhixian\csi\Csi.WebApp\Csi.WebApp.csproj(1,1)
System.IO.FileNotFoundException: Could not load file or assembly 'Microsoft.Build, Version=15.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a' or one of its dependencies. The system cannot find the file specified.
File name: 'Microsoft.Build, Version=15.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
   at OmniSharp.MSBuild.ProjectFile.ProjectFileInfo.Load(String filePath, ProjectIdInfo projectIdInfo, ProjectLoader loader)
   at OmniSharp.MSBuild.ProjectManager.LoadOrReloadProject(String projectFilePath, Func`1 loader) in C:\projects\omnisharp-roslyn\src\OmniSharp.MSBuild\ProjectManager.cs:line 306


See:
https://github.com/OmniSharp/omnisharp-vscode/issues/1727
https://github.com/OmniSharp/omnisharp-roslyn/wiki/Configuration-Options#global-omnisharpjson

## dev-certs

dotnet dev-certs https --trust

ZX: On Windows, this will install a "localhost" cert to Personal cert store 
    Under Personal > Certificates

An example of cert details:

```
Subject : localhost
Issuer : Self Issued
Time Validity : Friday, September 14, 2018 through Saturday, September 14, 2019
Serial Number : 40837A55 43BDF4BB
Thumbprint (sha1) : D6B16AD6 2545A93A 57AD8656 A6D9A6B2 269F29FA
Thumbprint (md5) : 211216D8 F5DE79CF 0AD6FA7D 43BD5138
```

In case, you receive the following exception message while running application, 
try removing the cert with:

dotnet dev-certs https --clean

Before installing the cert again.

```
[2019-05-08T22:07:24.3785541+08:00 DBG] Failed to authenticate HTTPS connection. HttpsConnectionAdapter
System.Security.Authentication.AuthenticationException: Authentication failed, see inner exception. ---> System.ComponentModel.Win32Exception: An unknown error occurred while processing the certificate
   --- End of inner exception stack trace ---
   at System.Net.Security.SslState.StartSendAuthResetSignal(ProtocolToken message, AsyncProtocolRequest asyncRequest, ExceptionDispatchInfo exception)
   at System.Net.Security.SslState.CheckCompletionBeforeNextReceive(ProtocolToken message, AsyncProtocolRequest asyncRequest)
   at System.Net.Security.SslState.StartSendBlob(Byte[] incoming, Int32 count, AsyncProtocolRequest asyncRequest)
   at System.Net.Security.SslState.ProcessReceivedBlob(Byte[] buffer, Int32 count, AsyncProtocolRequest asyncRequest)
   at System.Net.Security.SslState.StartReadFrame(Byte[] buffer, Int32 readBytes, AsyncProtocolRequest asyncRequest)
   at System.Net.Security.SslState.PartialFrameCallback(AsyncProtocolRequest asyncRequest)
--- End of stack trace from previous location where exception was thrown ---
   at System.Net.Security.SslState.ThrowIfExceptional()
   at System.Net.Security.SslState.InternalEndProcessAuthentication(LazyAsyncResult lazyResult)
   at System.Net.Security.SslState.EndProcessAuthentication(IAsyncResult result)
   at System.Net.Security.SslStream.EndAuthenticateAsServer(IAsyncResult asyncResult)
   at System.Net.Security.SslStream.<>c.<AuthenticateAsServerAsync>b__51_1(IAsyncResult iar)
   at System.Threading.Tasks.TaskFactory`1.FromAsyncCoreLogic(IAsyncResult iar, Func`2 endFunction, Action`1 endAction, Task`1 promise, Boolean requiresSynchronization)
```
