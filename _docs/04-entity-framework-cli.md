# Entity Framework Core CLI

ZX: Always create a dbcontext first!

dotnet ef migrations add CreateIdentitySchema -c Csi.WebApp.Data.CsiDbContext


##

dotnet ef dbcontext scaffold

Usage: dotnet ef dbcontext scaffold [arguments] [options]

Arguments:
  <CONNECTION>  The connection string to the database.
  <PROVIDER>    The provider to use. (E.g. Microsoft.EntityFrameworkCore.SqlServer)

Options:
  -d|--data-annotations                  Use attributes to configure the model (where possible). If omitted, only the fluent API is used.
  -c|--context <NAME>                    The name of the DbContext.
  --context-dir <PATH>                   The directory to put DbContext file in. Paths are relative to the project directory.
  -f|--force                             Overwrite existing files.
  -o|--output-dir <PATH>                 The directory to put files in. Paths are relative to the project directory.
  --schema <SCHEMA_NAME>...              The schemas of tables to generate entity types for.
  -t|--table <TABLE_NAME>...             The tables to generate entity types for.
  --use-database-names                   Use table and column names directly from the database.
  --json                                 Show JSON output.
  -p|--project <PROJECT>                 The project to use.
  -s|--startup-project <PROJECT>         The startup project to use.
  --framework <FRAMEWORK>                The target framework.
  --configuration <CONFIGURATION>        The configuration to use.
  --runtime <RUNTIME_IDENTIFIER>         The runtime to use.
  --msbuildprojectextensionspath <PATH>  The MSBuild project extensions path. Defaults to "obj".
  --no-build                             Don't build the project. Only use this when the build is up-to-date.
  -h|--help                              Show help information
  -v|--verbose                           Show verbose output.
  --no-color                             Don't colorize output.
  --prefix-output                        Prefix output with level.

```
dotnet ef dbcontext scaffold "name=CsiDatabase" MySql.Data.EntityFrameworkCore -d -c CsiDbContext -o Data
```

dotnet ef dbcontext list

dotnet ef dbcontext info --context Csi.WebApp.Data.CsiDbContext

dotnet ef migrations add CreateIdentitySchema --context Csi.WebApp.Data.CsiDbContext

info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 2.1.8-servicing-32085 initialized 'CsiDbContext' using provider 'MySql.Data.EntityFrameworkCore' with options: None
Done. To undo this action, use 'ef migrations remove'

dotnet ef database update --context Csi.WebApp.Data.CsiDbContext

dotnet ef migrations list --context Csi.WebApp.Data.CsiDbContext

dotnet ef database update 20190501052953_CreateIdentitySchema --context Csi.WebApp.Data.CsiDbContext

dotnet ef migrations add CreateIdentitySchema2 --context Csi.WebApp.Data.CsiDbContext
dotnet ef database update --context Csi.WebApp.Data.CsiDbContext

## Sql script to create __EFMigrationsHistory

```
CREATE TABLE `__EFMigrationsHistory` 
( 
    `MigrationId` nvarchar(150) NOT NULL, 
    `ProductVersion` nvarchar(32) NOT NULL, 
     PRIMARY KEY (`MigrationId`) 
);
```

## Entity framework annotations

System.ComponentModel.DataAnnotations Attributes

Attribute           Description
Key                 Can be applied to a property to specify a key property in an entity and make the corresponding column a 
                    PrimaryKey column in the database.
Timestamp	        Can be applied to a property to specify the data type of a corresponding column in the database as rowversion.
ConcurrencyCheck    Can be applied to a property to specify that the corresponding column should be included in the optimistic 
                    concurrency check.
Required	        Can be applied to a property to specify that the corresponding column is a NotNull column in the database.
MinLength	        Can be applied to a property to specify the minimum string length allowed in the corresponding column in the                         database.
MaxLength	        Can be applied to a property to specify the maximum string length allowed in the corresponding column in the                         database.
StringLength	    Can be applied to a property to specify the maximum string length allowed in the corresponding column in the                         database.

System.ComponentModel.DataAnnotations.Schema Attributes

Attribute           Description
Table	            Can be applied to an entity class to configure the corresponding table name and schema in the database.
Column	            Can be applied to a property to configure the corresponding column name, order and data type in the database.
Index	            Can be applied to a property to configure that the corresponding column should have an Index in the database. 
                    (EF 6.1 onwards only)
ForeignKey	        Can be applied to a property to mark it as a foreign key property.
NotMapped	        Can be applied to a property or entity class which should be excluded from the model and should not generate a 
                    corresponding column or table in the database.
DatabaseGenerated	Can be applied to a property to configure how the underlying database should generate the value for the 
                    corresponding column e.g. identity, computed or none.
InverseProperty	    Can be applied to a property to specify the inverse of a navigation property that represents the other end of
                    the same relationship.
ComplexType	        Marks the class as complex type in EF 6. EF Core 2.0 does not support this attribute.