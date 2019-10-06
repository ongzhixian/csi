dotnet add package Microsoft.Data.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Sqlite

REM Required for dotnet ef
dotnet add package Microsoft.EntityFrameworkCore.Design --version 2.1.8

REM Entity Framework Providers for Sqlite and MySql
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 2.1.8
dotnet add package MySql.Data.EntityFrameworkCore 
