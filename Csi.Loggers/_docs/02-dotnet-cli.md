# dotnet CLI


## Solution

dotnet new sln Csi.Loggers

## Projects

dotnet new classlib Csi.Loggers
dotnet sln Csi.Loggers/Csi.Loggers.sln add Csi.Loggers/Csi.Loggers.csproj
dotnet add Csi.Loggers/Csi.Loggers.csproj package Microsoft.Extensions.Logging --version 2.2.0


dotnet new nunit -o Csi.Loggers.Tests
dotnet sln Csi.Loggers/Csi.Loggers.sln add Csi.Loggers.Tests/Csi.Loggers.Tests.csproj
dotnet add Csi.Loggers.Tests reference Csi.Loggers


## Pack

dotnet pack Csi.Loggers

dotnet nuget push Csi.Loggers/bin/Debug/Csi.Loggers.1.0.0.nupkg -k <Csi-ApiKey> -s https://api.nuget.org/v3/index.json
