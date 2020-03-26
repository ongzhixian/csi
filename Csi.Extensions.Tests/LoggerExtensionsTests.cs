using Csi.Loggers;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Csi.Extensions;
using System.IO;
using System.Diagnostics;

namespace Csi.Extensions.Tests
{
    public class ILoggerExtensionsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LogLevelTest1()
        {
            // Arrange
            StringWriter sw = null;
            TextWriterTraceListener stringTraceListener = null;
            StringBuilder sb = new StringBuilder();
            ILoggerProvider loggerProvider = new TraceLoggerProvider();
            ILogger logger = loggerProvider.CreateLogger("ILoggerExtensionsTests");


            // Act
            using (sw = new StringWriter(sb))
            using (stringTraceListener = new TextWriterTraceListener(sw))
            {
                // Clean up - add trace listener
                Trace.Listeners.Add(stringTraceListener);

                logger.LogLevelTest();

                // Clean up - remove trace listener
                Trace.Listeners.Remove(stringTraceListener);
            }

            // Assert
            Assert.AreEqual("Trace|0|ILoggerExtensionsTests|LogLevel Trace         (trce) test.\r\nDebug|0|ILoggerExtensionsTests|LogLevel Debug         (dbug) test.\r\nInformation|0|ILoggerExtensionsTests|LogLevel Information   (info) test.\r\nWarning|0|ILoggerExtensionsTests|LogLevel Warning       (warn) test.\r\nError|0|ILoggerExtensionsTests|LogLevel Error         (fail) test.\r\nCritical|0|ILoggerExtensionsTests|LogLevel Critical      (crit) test.\r\n",
                sw.ToString()
            );

        }
    }
}
