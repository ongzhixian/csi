using NUnit.Framework;
using Csi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Csi.Loggers;
using System.Linq;
using Csi.Extensions.Tests.Data;
using System.Text;
using System.IO;
using System.Diagnostics;
using System;

namespace Tests
{
    public class ConfigureDependencyInjectionConfigTests
    {
        IConfigurationRoot config;
        ServiceCollection services;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // 1. Setup config
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }


        [SetUp]
        public void Setup()
        {
            // 2. Setup services
            services = new ServiceCollection();
            services.AddLogging(builder => builder.AddProvider(new TraceLoggerProvider()));
            //services.ConfigureDependencyInjection(config);
            //sp = services.BuildServiceProvider();
        }

        [Category("ConfigureDependencyInjection")]
        [Test(
            Author = "Ong Zhi Xian",
            TestOf = typeof(IServiceCollection))]
        public void NullConfigTest1()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                services.ConfigureDependencyInjection(null);
            });

        }

        [Category("ConfigureDependencyInjection")]
        [Test(
            Author = "Ong Zhi Xian",
            TestOf = typeof(IServiceCollection))]
        public void ConfigTest1()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            StringWriter sw = null;
            TextWriterTraceListener stringTraceListener = null;

            // Act 
            using (sw = new StringWriter(sb))
            using (stringTraceListener = new TextWriterTraceListener(sw))
            {
                stringTraceListener.Filter = new EventTypeFilter(SourceLevels.Information);

                // Clean up - add trace listener
                Trace.Listeners.Add(stringTraceListener);
                
                services.ConfigureDependencyInjection(config);

                // Clean up - remove trace listener
                Trace.Listeners.Remove(stringTraceListener);
            }

            // Assert
            Assert.Pass(); // TODO: Think of some better way to assert this.

        }

    }
}