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