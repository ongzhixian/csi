using Csi.Loggers;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Tests
{
    public class TraceLoggerProviderTests
    {
        [Test(
            Author="ONG ZHI XIAN", 
            Description = "Constructor (0 params) test",
            TestOf =typeof(PipeDelimitedConsoleLoggerProvider))]
        public void PipeDelimitedConsoleLoggerProviderCstrTest1()
        {
            // Arrange
            ILoggerProvider loggerProvider;

            // Act
            loggerProvider = new TraceLoggerProvider();

            // Assert
            Assert.IsNotNull(loggerProvider);
        }

        [Test(
            Author = "ONG ZHI XIAN",
            Description = "CreateLogger test",
            TestOf = typeof(PipeDelimitedConsoleLoggerProvider))]
        public void CreateLoggerTest1()
        {
            // Arrange
            ILoggerProvider loggerProvider;

            // Act
            loggerProvider = new TraceLoggerProvider();
            ILogger logger = loggerProvider.CreateLogger("TraceLoggerProviderTests");

            // Assert
            Assert.IsNotNull(logger);
        }

    }
}