using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Csi.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        // ZX: Reference to what ASP.NET Core Web Host does:
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/web-host?view=aspnetcore-2.1
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("runtime-settings.json", optional: true);
                })
                .ConfigureLogging((hostingContext, config) =>
                {
                    // Uncomment the below to remove the default loggers
                    //config.ClearProviders();
                    // The following are default loggers
                    //config.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    //config.AddConsole();
                    //config.AddDebug();

                    // ZX:  For some strange reason, LogTrace is not available using this method to add Serilog.
                    //      Not sure why. Give up for now :-(
                    //config.AddSerilog
                    //(
                    //    new LoggerConfiguration()
                    //        .ReadFrom.ConfigurationSection(hostingContext.Configuration.GetSection("Serilog"))
                    //        .Enrich.FromLogContext()
                    //        .CreateLogger()
                    //);
                })
                .UseStartup<Startup>()
                // ZX:  This is an alternative way to add logging using Serilog
                //      For some strange reason, using this makes LogTrace available :-)
                //      Not sure why. Give up for now :-(
                .UseSerilog((hostingContext, loggerConfiguration) =>
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()
                )
                ;
    }
}
