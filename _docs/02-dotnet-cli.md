# dotnet-cli

## Create global.json file to set the version of .NET Core SDK to use

```dotnet
dotnet new globaljson --sdk-version 2.1.603
```

ZX: VS2017 does not seem to play well with this file.
    If you have this file in the root, you will not be able 
    to load projects or add projects into solution file.
    No errors or warnings are displayed when loading projects.
    BUT if you try to add project to the solution file, you get
    a dialog that states:
    """
	Project file is incomplete. Expected imports are missing.
    """
    The quick solution to simply rename the global.json to something else
    before loading/adding the project to the solution.

## Creation

### Create new solution

```dotnet
dotnet new sln -n Csi
```

## Create new website

```dotnet
dotnet new mvc -n Csi.WebApp

-- Adds authentication and browserlink
dotnet new mvc -n Csi.WebApp --auth Individual --use-browserlink true

```

## Create new library

```dotnet
dotnet new classlib -n Csi.Models
```

### Adding project to solution

Assumes the solution file is in the directory where the command is executed and
that the project file is in a sub-directory call Csi.WebApp

```dotnet
dotnet sln Csi.sln add Csi.WebApp/Csi.WebApp.csproj
```

### Packages to install

Because we a target .NET Core 2.1, we have to specify version

```dotnet
REM Required for dotnet aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 2.1.9

REM Identity as a nuget package
dotnet add package Microsoft.AspNetCore.Identity.UI --version 2.1.6

dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 2.1.6

dotnet add package Microsoft.Extensions.Configuration --version 2.1.1
dotnet add package Microsoft.Extensions.Configuration.Json --version 2.1.1

REM Required for dotnet ef
dotnet add package Microsoft.EntityFrameworkCore.Design --version 2.1.8

REM Entity Framework Providers for Sqlite and MySql
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 2.1.8
dotnet add package MySql.Data.EntityFrameworkCore 

- - - - - - - - - - - - -

dotnet ef migrations add CreateIdentitySchema -c Csi.WebApp.Data.CsiDbContext
dotnet ef database update -c Csi.WebApp.Data.CsiDbContext

- - - - - - - - - - - - -

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package MySql.Data.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.App
dotnet add package Microsoft.AspNetCore.Authentication.Cookies 
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
dotnet add package Microsoft.AspNetCore.Mvc
?? dotnet add package Microsoft.AspNetCore.Razor.Design


Microsoft.EntityFrameworkCore.Sqlite
MySql.Data.EntityFrameworkCore
Oracle.EntityFrameworkCore
IBM.EntityFrameworkCore
Npgsql.EntityFrameworkCore.PostgreSQL
Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json

```


## Add reference to a project 

```
dotnet add Csi.Data.Tests.csproj reference ..\Csi.Data\Csi.Data.csproj
```


### Scaffolding tool

#### Install

```dotnet
dotnet tool install -g dotnet-aspnet-codegenerator
```

#### List available code generators

```dotnet
dotnet aspnet-codegenerator
```

The above may show the following if you have the Microsoft.VisualStudio.Web.CodeGeneration.Design nuget package.

Available generators:
  area      : Generates an MVC Area.
  controller: Generates a controller.
  identity  : Generates an MVC Area with controllers and
  razorpage : Generates RazorPage(s).
  view      : Generates a view.

Use `dotnet aspnet-codegenerator identity -h` to see help options for each generator

#### Generating new controllers

```dotnet
dotnet aspnet-codegenerator controller -name HomeController --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

dotnet aspnet-codegenerator controller -name BlogsController -m Blog -dc BloggingContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries


```

#### Generating views

```dotnet
dotnet aspnet-codegenerator view Index Empty -udl -outDir Views/Home

dotnet aspnet-codegenerator view _LoginPartial Empty -udl -outDir Views/Shared
```

#### Scaffolding identity pages

After adding the nuget package Microsoft.AspNetCore.Identity.UI --version 2.1.6,
you can add the default UI pages into the project by running the below:

```dotnet
dotnet aspnet-codegenerator identity --useDefaultUI
```

## Reference

* Other tools
  https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x