# Secret Manager

The Secret Manager tool abstracts away the implementation details, such as where and how the values are stored. 
You can use the tool without knowing these implementation details. The values are stored in a JSON configuration file in a system-protected user profile folder on the local machine:

## Location

```
%APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json
~/.microsoft/usersecrets/<user_secrets_id>/secrets.json
```

## csproj modifications

```
<PropertyGroup>
  <TargetFramework>netcoreapp2.1</TargetFramework>
  <UserSecretsId>79a3edd0-2092-40a2-a04d-dcb46d5ca9ed</UserSecretsId>
</PropertyGroup>
```

# Reference
https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=linux