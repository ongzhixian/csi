
```
dotnet tool install –global dotnet-dev-certs
```

```
dotnet dev-certs https -ep <path_to_certficate>/certificate.pfx -p <certificate_password>

dotnet dev-certs https –trust
```

```
openssl req -new -x509 -newkey rsa:2048 -keyout dev-certificate.key -out dev-certificate.cer -days 365 -subj /CN=localhost
openssl pkcs12 -export -out dev-certificate.pfx -inkey dev-certificate.key -in dev-certificate.cer
```

Reference:
https://pradeeploganathan.com/aspnetcore/https-in-asp-net-core-31/
