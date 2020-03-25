using NUnit.Framework;
using Csi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tests
{
    public class IServiceCollectionExtensionsTests
    {
        IConfigurationRoot config;
        ServiceCollection services;
        ServiceProvider sp;
        ILogger log;

        [SetUp]
        public void Setup()
        {
            // 1. Setup config
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // 2. Setup services
            services = new ServiceCollection();
            services
                .AddLogging(builder =>
                {
                    builder.AddConfiguration(config.GetSection("Logging"));

                    //builder.AddConsole();

                    //builder.AddProvider(new Csi.ExtensionsConsole.CsiConsoleLoggerProvider());
                    builder.AddProvider(new Csi.ExtensionsConsole.UnitTestLoggerProvider());
                });


        }

        [Category("ConfigureDependencyInjection")]
        [Test(
            Author="Ong Zhi Xian",
            TestOf=typeof(IServiceCollection) )]
        public void Test1()
        {
            Assert.Throws<System.IO.FileNotFoundException>(() =>
            {
                services.ConfigureDependencyInjection(null);

                // 3. Make service provider
                sp = services.BuildServiceProvider();
            });
            
        }
    }
}