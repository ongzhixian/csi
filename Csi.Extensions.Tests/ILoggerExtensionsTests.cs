using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Csi.Extensions;
using System.IO;
using System;
using Csi.ExtensionsConsole;

namespace Tests
{
    public class ILoggerExtensionsTests
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

            // 3. Make service provider
            sp = services.BuildServiceProvider();

            // 4. Do common work
            // Setup a logger
            log = sp
                .GetService<ILoggerFactory>()
                .CreateLogger<ILoggerExtensionsTests>();
        }

        [Test]
        public void Test1()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (System.Diagnostics.TextWriterTraceListener stringTraceListener = new System.Diagnostics.TextWriterTraceListener(sw))
            {
                // Clean up - add trace listener
                System.Diagnostics.Trace.Listeners.Add(stringTraceListener);

                //log.HelloWorld();
                log.Log(LogLevel.Information, "Hello world");

                Assert.AreEqual("Information|0|Tests.ILoggerExtensionsTests|Hello world\r\n", sw.ToString());

                // Clean up - remove trace listener
                System.Diagnostics.Trace.Listeners.Remove(stringTraceListener);
            }
        }

        [Test]
        public void __Test__()
        {
            //IUnitTestLogger p = log as IUnitTestLogger;
            //UnitTestLoggerProvider.UnitTestLogger q = log as UnitTestLoggerProvider.UnitTestLogger;
            //System.Diagnostics.Debugger.Break();

            // ZX: Due to the way the types are resolved in dependency injection, 
            // we cannot Microsoft.Extensions.Logging.Logger to our custom type.

            // ZX: The next best way is to implement a logger that writes to facility common to all.
            // such as System.Console, System.Diagnostics.Trace

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (System.Diagnostics.TextWriterTraceListener stringTraceListener = new System.Diagnostics.TextWriterTraceListener(sw))
            {
                // Clean up - add trace listener
                System.Diagnostics.Trace.Listeners.Add(stringTraceListener);

                //log.HelloWorld();
                log.Log(LogLevel.Information, "Hello world");

                Assert.AreEqual("Information|0|Tests.ILoggerExtensionsTests|Hello world\r\n", sw.ToString());

                // If performing other operatoins
                //sb.Length = 0; // Clear buffers (otherwise, previous log statements will be in buffer)
                //log.HelloWorld();
                //System.Diagnostics.Debugger.Break();

                // Clean up - remove trace listener
                System.Diagnostics.Trace.Listeners.Remove(stringTraceListener);
            }



            // Implementation using BinaryFormatter
            // Result: Does not work well
            // EventId and State does not serialized
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //using (StringWriter sw = new StringWriter(sb))
            //using (System.Diagnostics.TextWriterTraceListener stringTraceListener = new System.Diagnostics.TextWriterTraceListener(sw))
            //{
            //    System.Diagnostics.Trace.Listeners.Add(stringTraceListener);
            //    log.HelloWorld();
            //    System.IO.MemoryStream ms = new MemoryStream(Convert.FromBase64String(sw.ToString()));
            //    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //    LogEntryState logEntryState = binaryFormatter.Deserialize(ms) as LogEntryState;
            //    System.Diagnostics.Debugger.Break();
            //    // Cleanup 
            //    System.Diagnostics.Trace.Listeners.Remove(stringTraceListener);
            //}


            // Implementation trying to use serialized XML as medium of passing message
            // Result: Does not work well
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //using (StringWriter sw = new StringWriter(sb))
            //using (System.Diagnostics.TextWriterTraceListener stringTraceListener = new System.Diagnostics.TextWriterTraceListener(sw))
            //{
            //    System.Diagnostics.Trace.Listeners.Add(stringTraceListener);

            //    log.HelloWorld();

            //    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(LogEntryState));

            //    using (System.IO.StringReader sr = new StringReader(sw.ToString()))
            //    {
            //        LogEntryState logEntryState = x.Deserialize(sr) as LogEntryState;
            //        System.Diagnostics.Debugger.Break();
            //    }

            //    // Cleanup 
            //    System.Diagnostics.Trace.Listeners.Remove(stringTraceListener);
            //}


            // Implementation using System.Diagnostics.Trace
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //using (StringWriter sw = new StringWriter(sb))
            //using (System.Diagnostics.TextWriterTraceListener stringTraceListener = new System.Diagnostics.TextWriterTraceListener(sw))
            //{
            //    System.Diagnostics.Trace.Listeners.Add(stringTraceListener);
            //    log.HelloWorld();
            //    System.Diagnostics.Debugger.Break();
            //    sb.Length = 0;
            //    log.HelloWorld();
            //    System.Diagnostics.Debugger.Break();
            //    System.Diagnostics.Trace.Listeners.Remove(stringTraceListener);
            //}



            // Implementation using Console
            //System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            //using (StringWriter sw = new StringWriter(sb2))
            //{
            //    TextWriter backupConsoleOut = Console.Out;

            //    Console.SetOut(sw);
            //    log.HelloWorld();
            //    System.Diagnostics.Debugger.Break();
            //    sb2.Length = 0; // Clear previous
            //    log.HelloWorld();
            //    System.Diagnostics.Debugger.Break();

            //    // All done; restore previous Console.Out
            //    Console.SetOut(backupConsoleOut);
            //}

            // ZX:  Although Console works, its maybe a bad idea to muck around with it.
            //      especially, if you are a 


            //using (StringWriter sw = new StringWriter())
            //{
            //    Console.SetOut(sw);
            //    //ConsoleUser cu = new ConsoleUser();
            //    //cu.DoWork();
            //    Assert.Pass();
            //    //string expected = string.Format("Ploeh{0}", Environment.NewLine);
            //    //Assert.AreEqual<string>(expected, sw.ToString());
            //}

        }
    }
}