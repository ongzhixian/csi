using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace Csi.AuthenticationGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ZX: KIV isService; To be implemented
            // bool isService = 
            //     (!System.Environment.UserInteractive) 
            //     || (!System.Diagnostics.Debugger.IsAttached)
            //     || (args.Contains("--console"));

            Logger log = null;

            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("runtime-settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            try
            {
                log = NLog.Web.NLogBuilder.ConfigureNLog("nlog-config.xml").GetCurrentClassLogger();
                //log.Debug("init main");
                
                log.Trace("Test Trace", (key: "Time", value: DateTime.Now.ToString("U")), (key: "TimeZone", value: System.TimeZoneInfo.Local.StandardName));
                log.Debug("Test Debug", (key: "Time", value: DateTime.Now.ToString("U")), (key: "TimeZone", value: System.TimeZoneInfo.Local.StandardName));
                log.Info("Test Info", (key: "Time", value: DateTime.Now.ToString("U")), (key: "TimeZone", value: System.TimeZoneInfo.Local.StandardName));
                log.Warn("Test Warning", (key: "Time", value: DateTime.Now.ToString("U")), (key: "TimeZone", value: System.TimeZoneInfo.Local.StandardName));
                log.Error("Test Error", (key: "Time", value: DateTime.Now.ToString("U")), (key: "TimeZone", value: System.TimeZoneInfo.Local.StandardName));
                log.Fatal("Test Fatal", (key: "Time", value: DateTime.Now.ToString("U")), (key: "TimeZone", value: System.TimeZoneInfo.Local.StandardName));

                IWebHost host = CreateWebHostBuilder(config).Build();

                host.Run();
                
            }
            catch (Exception ex)
            {
                if (log != null)
                {
                    log.Error(ex, "Stopped program because of exception");
                }
                
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(IConfiguration config) =>
            new WebHostBuilder()
            .UseConfiguration(config)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            })
            .UseKestrel()
            .UseStartup<Startup>()
            .UseNLog();

    }
}
