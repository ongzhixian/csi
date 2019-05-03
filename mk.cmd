GOTO PHASE1

REM NOTE: Remember to start with an empty database with only __EFMigrationsHistory
REM ================================================================================
REM START PHASE 1
:PHASE1

:REMOVE_OLD
ECHO REMOVE OLD VERSION
RMDIR /S /Q Csi.WebApp
dotnet sln Csi.sln remove Csi.WebApp/Csi.WebApp.csproj

:MAKE_SITE
ECHO MAKE NEW MVC SITE --auth Individual 
dotnet new mvc -n Csi.WebApp --use-browserlink true

:ADD_PACKAGE
cd Csi.WebApp
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 2.1.9
dotnet add package Microsoft.Extensions.Configuration --version 2.1.1
dotnet add package Microsoft.Extensions.Configuration.Json --version 2.1.1
dotnet add package Microsoft.EntityFrameworkCore.Design --version 2.1.8
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 2.1.8
dotnet add package Microsoft.AspNetCore.Identity.UI --version 2.1.6
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 2.1.6
dotnet add package MySql.Data.EntityFrameworkCore 
cd ..

:INIT_BUILD
cd Csi.WebApp
dotnet restore
dotnet build
cd ..

ECHO.
ECHO TODO: Update appsettings.json
ECHO.

GOTO END

REM END   PHASE 1

REM ================================================================================
REM START PHASE 2

:PHASE2

:MAKE_DBCTX
cd Csi.WebApp
dotnet ef dbcontext scaffold "name=CsiDatabase" MySql.Data.EntityFrameworkCore -d -c CsiDbContext -o Data
cd ..

:ADD_IDENTITY
cd Csi.WebApp
dotnet aspnet-codegenerator identity 
dotnet aspnet-codegenerator identity -dc Csi.WebApp.Data.ApplicationDbContext -u CsiUser -f
dotnet aspnet-codegenerator identity -u Csi.WebApp.Data.CsiUser -f
REM--useDefaultUI --userClass Csi.WebApp.Data.CsiUser
cd ..

:ADD_EFTABLES
dotnet ef dbcontext list
dotnet ef dbcontext info --context Csi.WebApp.Data.CsiDbContext

dotnet ef migrations add CreateIdentitySchema --context Csi.WebApp.Data.CsiDbContext
dotnet ef database update --context Csi.WebApp.Data.CsiDbContext

dotnet ef migrations add CreateIdentityClaimSchema --context Csi.WebApp.Data.CsiDbContext
dotnet ef database update --context Csi.WebApp.Data.CsiDbContext

dotnet ef migrations add CorrectIdentityClaimSchema --context Csi.WebApp.Data.CsiDbContext
dotnet ef database update --context Csi.WebApp.Data.CsiDbContext

GOTO END

REM END   PHASE 2

:END

