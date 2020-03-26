using Csi.Loggers;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using static Csi.Loggers.PipeDelimitedConsoleLoggerProvider;

namespace Tests
{
    public class PipeDelimitedConsoleLoggerTests
    {
        ILoggerProvider loggerProvider;
        ILogger logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            loggerProvider = new PipeDelimitedConsoleLoggerProvider();
            logger = loggerProvider.CreateLogger("PipeDelimitedConsoleLoggerTests");
        }

        [SetUp]
        public void Setup()
        {
            // Uncomment below if we want a "fresh" logger for each test
            //logger = loggerProvider.CreateLogger("PipeDelimitedConsoleLoggerTests");
        }

        // Critical ///////////////////////////

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log critical message (0 params)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogCriticalZeroParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.Critical, "Hello world");

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Critical|0|PipeDelimitedConsoleLoggerTests|Hello world\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log critical message (0 params)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogCriticalZeroParams2()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.LogCritical("Hello world");

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Critical|0|PipeDelimitedConsoleLoggerTests|Hello world\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log crtitical message (1 param)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogCriticalWithParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.Critical, "Hello world {0}", DateTime.MaxValue.ToString("s"));

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Critical|0|PipeDelimitedConsoleLoggerTests|Hello world 9999-12-31T23:59:59\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log critical message (1 param)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogCriticalWithParams2()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.LogCritical("Hello world {0}", DateTime.MaxValue.ToString("s"));

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Critical|0|PipeDelimitedConsoleLoggerTests|Hello world 9999-12-31T23:59:59\r\n", sw.ToString());
        }

        // Error ///////////////////////////////

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log error message (0 params)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogErrorZeroParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.Error, "Hello world");

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Error|0|PipeDelimitedConsoleLoggerTests|Hello world\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log error message (0 params)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogErrorZeroParams2()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.LogError("Hello world");

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Error|0|PipeDelimitedConsoleLoggerTests|Hello world\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log error message (1 param)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogErrorWithParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.Error, "Hello world {0}", DateTime.MaxValue.ToString("s"));

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Error|0|PipeDelimitedConsoleLoggerTests|Hello world 9999-12-31T23:59:59\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log error message (1 param)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogErrorWithParams2()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.LogError("Hello world {0}", DateTime.MaxValue.ToString("s"));

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Error|0|PipeDelimitedConsoleLoggerTests|Hello world 9999-12-31T23:59:59\r\n", sw.ToString());
        }

        // Warning /////////////////////////////

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log warning message (0 params)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogWarningZeroParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.Warning, "Hello world");

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Warning|0|PipeDelimitedConsoleLoggerTests|Hello world\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log warning message (0 params)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogWarningZeroParams2()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.LogWarning("Hello world");

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Warning|0|PipeDelimitedConsoleLoggerTests|Hello world\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log warning message (1 param)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogWarningWithParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.Warning, "Hello world {0}", DateTime.MaxValue.ToString("s"));

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Warning|0|PipeDelimitedConsoleLoggerTests|Hello world 9999-12-31T23:59:59\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log warning message (1 param)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogWarningWithParams2()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.LogWarning("Hello world {0}", DateTime.MaxValue.ToString("s"));

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Warning|0|PipeDelimitedConsoleLoggerTests|Hello world 9999-12-31T23:59:59\r\n", sw.ToString());
        }

        // Information /////////////////////////

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log information message (0 params)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogInformationZeroParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.Information, "Hello world");

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Information|0|PipeDelimitedConsoleLoggerTests|Hello world\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log information message (0 params)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogInformationZeroParams2()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.LogInformation("Hello world");

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Information|0|PipeDelimitedConsoleLoggerTests|Hello world\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log information message (1 param)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogInformationWithParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.Information, "Hello world {0}", DateTime.MaxValue.ToString("s"));

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Information|0|PipeDelimitedConsoleLoggerTests|Hello world 9999-12-31T23:59:59\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log information message (1 param)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogInformationWithParams2()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.LogInformation("Hello world {0}", DateTime.MaxValue.ToString("s"));

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Information|0|PipeDelimitedConsoleLoggerTests|Hello world 9999-12-31T23:59:59\r\n", sw.ToString());
        }


        // Debug ///////////////////////////////

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log debug message (0 params)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogDebugZeroParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.Debug, "Hello world");

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Debug|0|PipeDelimitedConsoleLoggerTests|Hello world\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log debug message (0 params)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogDebugZeroParams2()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.LogDebug("Hello world");

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Debug|0|PipeDelimitedConsoleLoggerTests|Hello world\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log debug message (1 param)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogDebugWithParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.Debug, "Hello world {0}", DateTime.MaxValue.ToString("s"));

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Debug|0|PipeDelimitedConsoleLoggerTests|Hello world 9999-12-31T23:59:59\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log debug message (1 param)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogDebugWithParams2()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.LogDebug("Hello world {0}", DateTime.MaxValue.ToString("s"));

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Debug|0|PipeDelimitedConsoleLoggerTests|Hello world 9999-12-31T23:59:59\r\n", sw.ToString());
        }


        // Trace ///////////////////////////////


        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log trace message (0 params)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogTraceZeroParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.Trace, "Hello world");

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Trace|0|PipeDelimitedConsoleLoggerTests|Hello world\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log trace message (0 params)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogTraceZeroParams2()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.LogTrace("Hello world");

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Trace|0|PipeDelimitedConsoleLoggerTests|Hello world\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log trace message (1 param)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogTraceWithParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.Trace, "Hello world {0}", DateTime.MaxValue.ToString("s"));

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Trace|0|PipeDelimitedConsoleLoggerTests|Hello world 9999-12-31T23:59:59\r\n", sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log trace message (1 param)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogTraceWithParams2()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.LogTrace("Hello world {0}", DateTime.MaxValue.ToString("s"));

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            Assert.AreEqual("Trace|0|PipeDelimitedConsoleLoggerTests|Hello world 9999-12-31T23:59:59\r\n", sw.ToString());
        }


        ////////////////////////////////////////
        // None ////////////////////////////////


        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log (none) message (0 params)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogNoneZeroParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.None, "Hello world");

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            //Assert.AreEqual("None|0|PipeDelimitedConsoleLoggerTests|Hello world\r\n", sw.ToString());
            Assert.AreEqual(string.Empty, sw.ToString());
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "Log (none) message (1 param)",
            TestOf = typeof(PipeDelimitedConsoleLogger))]
        public void LogNoneWithParams1()
        {
            // Arrange
            StringWriter sw = null;
            StringBuilder sb = new StringBuilder();

            // Act
            using (sw = new StringWriter(sb))
            {
                TextWriter backupConsoleOut = Console.Out;

                Console.SetOut(sw);

                logger.Log(LogLevel.None, "Hello world {0}", DateTime.MaxValue.ToString("s"));

                // All done; restore previous Console.Out
                Console.SetOut(backupConsoleOut);
            }

            // Assert
            //Assert.AreEqual("None|0|PipeDelimitedConsoleLoggerTests|Hello world 9999-12-31T23:59:59\r\n", sw.ToString());
            Assert.AreEqual(string.Empty, sw.ToString());
        }


    }
}