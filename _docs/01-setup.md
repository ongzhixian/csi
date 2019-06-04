# Setup

## Set version of .NET Core SDK to use

REM Create global.json file; this sets the version of .NET Core SDK to use
dotnet new globaljson --sdk-version 2.1.603

## Git setup

git config user.name "xxx xxxx"
git config user.email "xxx@xxx.xxx"

## omnisharp.path

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

To fix in VS Code, add the following to VS Code's "settings.json":

```
"omnisharp.path": "1.32.3-beta.9"
```

The path that the above setting is referring to can be found at:

```
C:\Users\zhixian\.vscode\extensions\ms-vscode.csharp-1.19.1\.omnisharp
```

See:
https://github.com/OmniSharp/omnisharp-roslyn/issues/1094#issuecomment-420355477

See:
https://github.com/OmniSharp/omnisharp-vscode/issues/1727
https://github.com/OmniSharp/omnisharp-roslyn/wiki/Configuration-Options#global-omnisharpjson

## SET MSBuildSDKsPath

This is an issue that is related to Omnisharp.

Sometimes Omnisharp will report error loading projects and the following error message is given:
"The SDK 'Microsoft.Net.Sdk' specified could not be found."

The way to fix is is to manually(?) specify the SDK using the `DOTNET_MSBUILD_SDK_RESOLVER_SDKS_DIR` environment variable.

For example:

```
SET MSBuildSDKsPath=C:\Program Files\dotnet\sdk\<sdk-version>\Sdks

SET MSBuildSDKsPath=C:\Program Files\dotnet\sdk\2.1.603\Sdks

SET MSBuildSDKsPath=C:\Program Files\dotnet\sdk\3.0.100-preview4-011223\Sdks
```

ZX: It maybe desirable to set an environment variable to the underlying SDK path.
    For example set it to `DOTNET_SDK`, then the above can be re-written as:

```
    SET DOTNET_SDK=C:\Program Files\dotnet\sdk
    SET MSBuildSDKsPath=%DOTNET_SDK%\2.1.603\Sdks
```

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
