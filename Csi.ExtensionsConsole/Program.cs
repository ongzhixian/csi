using Csi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Csi.ExtensionsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Non-host console should always perform the following sequences:
            // 1. Setup config
            // 2. Setup services
            // 3. Make service provider
            // 4. Do work

            // 1. Setup config
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // 2. Setup services
            ServiceCollection services = new ServiceCollection();
            services
                .AddLogging(builder =>
                {
                    builder.AddConfiguration(config.GetSection("Logging"));

                    //builder.AddConsole();

                    builder.AddProvider(new CsiConsoleLoggerProvider());
                })
                .ConfigureDependencyInjection(config);

            // ZX note: The above AddLogging can re-written as:
            //.AddLogging(builder => builder.AddConfiguration(config.GetSection("Logging")).AddConsole())


            // 3. Make service provider
            ServiceProvider sp = services.BuildServiceProvider();


            // 4. Do work

            // Setup a logger
            ILogger log = sp
                .GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            log.LogInformation("[START PROGRAM]");
            
            IAnimal dog = sp.GetRequiredService<IAnimal>();
            log.LogInformation(dog.MakeSound());

            log.LogLevelTest();
            //log.Log(LogLevel.None, "Test None");   // Severity: - No log output
            //log.Log(LogLevel.Trace, "Test trace");  // Severity: 1
            //log.Log(LogLevel.Debug, "Test debug");  // Severity: 2
            //log.Log(LogLevel.Information, "Test Info");   // Severity: 3
            //log.Log(LogLevel.Warning, "Test warn");   // Severity: 4
            //log.Log(LogLevel.Error, "Test err");    // Severity: 5
            //log.Log(LogLevel.Critical, "Test crit");   // Severity: 6


            log.LogInformation("[END   PROGRAM]");
            log.LogInformation("Press <ENTER> to continue.");
            Console.ReadLine();
        }
    }

}
