# aspnet-codegenerator

ZX: Maybe its faster to copy and paste.

## Install 

```
dotnet tool install -g dotnet-aspnet-codegenerator
```

## List available code generators

```
dotnet aspnet-codegenerator
```

The above may show the following if you have the `Microsoft.VisualStudio.Web.CodeGeneration.Design` nuget package.

Available generators:
  area      : Generates an MVC Area.
  controller: Generates a controller.
  identity  : Generates an MVC Area with controllers and
  razorpage : Generates RazorPage(s).
  view      : Generates a view.

Use `dotnet aspnet-codegenerator <generator> -h` to see help options for each generator.

For example:

```
dotnet aspnet-codegenerator identity -h
```


## Scaffolding identity pages

After adding the nuget package `Microsoft.AspNetCore.Identity.UI`,
you can add the default UI pages into the project by running the below:

```
dotnet aspnet-codegenerator identity --useDefaultUI --dbContext CsiDbContext
```

This will create the `Area/Identity` and other related artifacts.

## Help: area

Selected Code Generator: area

Generator Arguments:
  Name : Name of the Area to generate

## Help: controller

Selected Code Generator: controller

Generator Options:
  --controllerName|-name              : Name of the controller
  --useAsyncActions|-async            : Switch to indicate whether to generate async controller actions
  --noViews|-nv                       : Switch to indicate whether to generate CRUD views
  --restWithNoViews|-api              : Specify this switch to generate a Controller with REST style API, noViews is assumed and any view related options are ignored
  --readWriteActions|-actions         : Specify this switch to generate Controller with read/write actions when a Model class is not used
  --model|-m                          : Model class to use
  --dataContext|-dc                   : DbContext class to use
  --referenceScriptLibraries|-scripts : Switch to specify whether to reference script libraries in the generated views
  --layout|-l                         : Custom Layout page to use
  --useDefaultLayout|-udl             : Switch to specify that default layout should be used for the views
  --force|-f                          : Use this option to overwrite existing files
  --relativeFolderPath|-outDir        : Specify the relative output folder path from project where the file needs to be generated, if not specified, file will be generated in the project folder
  --controllerNamespace|-namespace    : Specify the name of the namespace to use for the generated controller

Example call:

dotnet aspnet-codegenerator controller --controllerName Csi.WebApp.ProjectController --model Csi.Data.Project --dataContext Csi.WebApp.Data.CsiDbContext --referenceScriptLibraries

dotnet aspnet-codegenerator controller --controllerName HomeController --controllerNamespace Csi.WebApp.Sample --referenceScriptLibraries

dotnet aspnet-codegenerator controller --controllerName ProjectController --controllerNamespace Csi.WebApp.Api.Controllers --referenceScriptLibraries --restWithNoViews --useAsyncActions 

dotnet aspnet-codegenerator controller --controllerName ProjectController --controllerNamespace Csi.WebApp.Api.Controllers --referenceScriptLibraries --restWithNoViews --useAsyncActions --model Csi.Data.Project --dataContext Csi.WebApp.Data.CsiDbContext

dotnet aspnet-codegenerator controller --controllerName EchoController --controllerNamespace Csi.WebApp.Api.Controllers --referenceScriptLibraries --restWithNoViews --useAsyncActions --readWriteActions

# Use below to create a Controller with basic CRUD without model; does not create views
dotnet aspnet-codegenerator controller --controllerName Test2Controller --readWriteActions --controllerNamespace Csi.WebApp.Controllers --relativeFolderPath Controllers --referenceScriptLibraries

dotnet aspnet-codegenerator controller --controllerName SimpleProjectController --model Csi.Data.SimpleProject --dataContext Csi.WebApp.Data.CsiSQLiteDbContext --controllerNamespace Csi.WebApp.Controllers --relativeFolderPath Controllers --referenceScriptLibraries


## Help: identity

Selected Code Generator: identity

Generator Options:
  --dbContext|-dc      : Name of the DbContext to use, or generate (if it does not exist).
  --files|-fi          : List of semicolon separated files to scaffold. Use the --list-files option to see the available options.
  --listFiles|-lf      : Lists the files that can be scaffolded by using the '--files' option.
  --userClass|-u       : Name of the User class to generate.
  --useSqLite|-sqlite  : Flag to specify if DbContext should use SQLite instead of SQL Server.
  --force|-f           : Use this option to overwrite existing files.
  --useDefaultUI|-udui : Use this option to setup identity and to use Default UI.
  --layout|-l          : Specify a custom layout file to use.
  --generateLayout|-gl : Use this option to generate a new _Layout.cshtml


## Help: razorpage

Selected Code Generator: razorpage

Generator Arguments:
  razorPageName : Name of the Razor Page
  templateName  : The template to use, supported view templates: 'Empty|Create|Edit|Delete|Details|List'

Generator Options:
  --model|-m                          : Model class to use
  --dataContext|-dc                   : DbContext class to use
  --referenceScriptLibraries|-scripts : Switch to specify whether to reference script libraries in the generated views
  --layout|-l                         : Custom Layout page to use
  --useDefaultLayout|-udl             : Switch to specify that default layout should be used for the views
  --force|-f                          : Use this option to overwrite existing files
  --relativeFolderPath|-outDir        : Specify the relative output folder path from project where the file needs to be generated, if not specified, file will be generated in the project folder
  --namespaceName|-namespace          : Specify the name of the namespace to use for the generated PageModel
  --partialView|-partial              : Generate a partial view, other layout options (-l and -udl) are ignored if this is specified
  --noPageModel|-npm                  : Switch to not generate a PageModel class for Empty template



## Help: view

Selected Code Generator: view

Generator Arguments:
  viewName     : Name of the view
  templateName : The view template to use, supported view templates: 'Empty|Create|Edit|Delete|Details|List'

Generator Options:
  --model|-m                          : Model class to use
  --dataContext|-dc                   : DbContext class to use
  --referenceScriptLibraries|-scripts : Switch to specify whether to reference script libraries in the generated views
  --layout|-l                         : Custom Layout page to use
  --useDefaultLayout|-udl             : Switch to specify that default layout should be used for the views
  --force|-f                          : Use this option to overwrite existing files
  --relativeFolderPath|-outDir        : Specify the relative output folder path from project where the file needs to be generated, if not specified, file will be generated in the project folder
  --controllerNamespace|-namespace    : Specify the name of the namespace to use for the generated controller
  --partialView|-partial              : Generate a partial view, other layout options (-l and -udl) are ignored if this is specified

dotnet aspnet-codegenerator view Index Empty --controllerNamespace Csi.WebApp.Sample --relativeFolderPath Areas/Sample/Views/Home
