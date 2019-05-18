# Code coverage using coverlet

## Installing tool

```
dotnet tool install --global coverlet.console
```

Note: Trying to install this under .NET Core 2.1 will result in the following error message:

```
error NU1202: Package coverlet.console 1.5.1 is not compatible with netcoreapp2.1 (.NETCoreApp,Version=v2.1) / any. Package coverlet.console 1.5.1 supports: netcoreapp2.2 (.NETCoreApp,Version=v2.2) / any
The tool package could not be restored.
Tool 'coverlet.console' failed to install. This failure may have been caused by:

* You are attempting to install a preview release and did not use the --version option to specify the version.
* A package by this name was found, but it was not a .NET Core tool.
* The required NuGet feed cannot be accessed, perhaps because of an Internet connection problem.
* You mistyped the name of the tool.
```

## Usage

1. Add `coverlet.msbuild` package to your test projects
2. Run `dotnet test /p:CollectCoverage=true`
3. 
```
dotnet add package coverlet.msbuild 
```

## Exclusion

Exclude using attributes
dotnet test /p:CollectCoverage=true /p:ExcludeByAttribute="Obsolete,GeneratedCodeAttribute,CompilerGeneratedAttribute"

Exclude using file
dotnet test /p:CollectCoverage=true /p:ExcludeByFile=\"../dir1/class1.cs,../dir2/*.cs,../dir3/**/*.cs\"

Exclude using filters
dotnet test /p:CollectCoverage=true /p:Exclude="[coverlet.*]Coverlet.Core.Coverage"

## Reference
https://github.com/tonerdo/coverlet
