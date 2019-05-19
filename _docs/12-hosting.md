# Hosting notes


## Generate publish files

```
dotnet publish
dotnet publish --configuration Release
```

Output defaults to: 

1.  `./bin/[configuration]/[framework]/publish/`            for a framework-dependent deployment 
or 
2.  `./bin/[configuration]/[framework]/[runtime]/publish/`  for a self-contained deployment.


## Ubuntu 14.04 hosting

### Setup

```
cd /var/www
mkdir csi
sudo chown -R www-data:www-data csi
```

[ACTIONS]   Copy files to `/var/www/csi`

```
cd /etc/nginx/sites-available
sudo cp xanco-data-repo csi
sudo pico csi
```

[ACTIONS]   Change root location
[ACTIONS]   Change server_name
[ACTIONS]   Change proxy_pass

A sample may look like:

```
server {
        listen <nginx-port; for example: 80>;
        listen [::]:<nginx-port; for example: 80>;

        root /<some-location>/csi/wwwroot;
        index index.html index.htm;

        server_name csi.plato.emptool.com www.csi.plato.emptool.com;

        location / {
        proxy_pass http://localhost:<port-used; for example: 8019>;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}
```

``` 
sudo ln -s /etc/nginx/sites-available/csi /etc/nginx/sites-enabled/csi
cd /<some-location>/supervisor/conf.d
sudo cp xanco-data-repo.conf csi.conf
sudo pico csi.conf
```

[ACTIONS]   Change command
[ACTIONS]   Change directory
[ACTIONS]   Change stderr_logfile
[ACTIONS]   Change stdout_logfile
[ACTIONS]   Change environment

An example of csi.conf

```
[program:csi]
command=/usr/bin/dotnet /<some-location>/csi/Csi.WebApp.dll
directory=/<some-location>/csi/
autostart=true
autorestart=true
stderr_logfile=/var/log/csi.err.log
stdout_logfile=/var/log/csi.out.log
environment=HOME=/<some-location>/csi/,ASPNETCORE_ENVIRONMENT=Production
user=www-data
stopsignal=INT
stopasgroup=true
killasgroup=true
```

sudo pico haproxy.cfg





## Reference
https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish?tabs=netcore21
https://docs.microsoft.com/en-us/dotnet/core/deploying/deploy-with-cli


Kestrel web server implementation in ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-2.1

Configure ASP.NET Core to work with proxy servers and load balancers
https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-2.1

Enforce HTTPS in ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.1&tabs=visual-studio

