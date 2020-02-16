# WebHost.CreateDefaultBuilder()

What it does under the hood

CreateDefaultBuilder()

https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.webhost.createdefaultbuilder?view=aspnetcore-3.1

The following defaults are applied to the returned WebHostBuilder: 
1.  use Kestrel as the web server and configure it using the application's configuration providers, 
2.  set the Microsoft.Extensions.Hosting.IHostEnvironment.ContentRootPath to the result of GetCurrentDirectory(), 
3.  load IConfiguration from 'appsettings.json' and 
4.  load 'appsettings.[Microsoft.Extensions.Hosting.IHostEnvironment.EnvironmentName].json', 
5.  load IConfiguration from User Secrets when Microsoft.Extensions.Hosting.IHostEnvironment.EnvironmentName is 'Development' using the entry assembly, 
6,  load IConfiguration from environment variables, 
7.  configure the Microsoft.Extensions.Logging.ILoggerFactory to log to the console and debug output, 
8.  adds the HostFiltering middleware, 
9.  adds the ForwardedHeaders middleware if ASPNETCORE_FORWARDEDHEADERS_ENABLED=true, and 
10. enable IIS integration.



https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/web-host?view=aspnetcore-3.1


```Reference https://github.com/aspnet/MetaPackages/blob/f245512f6e68d65309b65528d479f32b34c67718/src/Microsoft.AspNetCore/WebHost.cs
public static IWebHostBuilder CreateDefaultBuilder(string[] args)
{
    var builder = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            var env = hostingContext.HostingEnvironment;

            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            if (env.IsDevelopment())
            {
                var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                if (appAssembly != null)
                {
                    config.AddUserSecrets(appAssembly, optional: true);
                }
            }

            config.AddEnvironmentVariables();

            if (args != null)
            {
                config.AddCommandLine(args);
            }
        })
        .ConfigureLogging((hostingContext, logging) =>
        {
            logging.UseConfiguration(hostingContext.Configuration.GetSection("Logging"));
            logging.AddConsole();
            logging.AddDebug();
        })
        .UseIISIntegration()
        .UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
        })
        .ConfigureServices(services =>
        {
            services.AddTransient<IConfigureOptions<KestrelServerOptions>, KestrelServerOptionsSetup>();
        });

    return builder;
}
```