# OAuth with external providers



dotnet add package Microsoft.AspNetCore.Authentication.MicrosoftAccount --version 2.1.2
dotnet add package Microsoft.AspNetCore.Authentication.Google --version 2.1.8


Reference:

Most approach includes ASP.NET Core Identity as an authentication provider.
This sample demonstrates how to use an external authentication provider without ASP.NET Core Identity. This is useful for apps that don't require all of the features of ASP.NET Core Identity, but still require integration with a trusted external authentication provider.

https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/social-without-identity?view=aspnetcore-3.1