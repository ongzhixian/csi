using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Csi.LookMeUp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateWebHostBuilder(args).Build().Run();
            //bool isService = !(System.Diagnostics.Debugger.IsAttached || args.Contains("--console"));

            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("runtime-settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            IWebHost host = CreateWebHostBuilder(config).Build();

            host.Run();
        }

        // public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //     WebHost.CreateDefaultBuilder(args)
        //         .UseStartup<Startup>();
        public static IWebHostBuilder CreateWebHostBuilder(IConfiguration config) =>
            new WebHostBuilder()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseConfiguration(config)
            .UseKestrel()
            .UseStartup<Startup>();
    }
}
