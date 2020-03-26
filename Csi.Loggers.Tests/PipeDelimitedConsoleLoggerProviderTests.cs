using Csi.Loggers;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Tests
{
    public class PipeDelimitedConsoleLoggerProviderTests
    {
        ILoggerProvider loggerProvider;

        [Test(
            Author="ONG ZHI XIAN", 
            Description = "Constructor (0 params) test",
            TestOf =typeof(PipeDelimitedConsoleLoggerProvider))]
        public void PipeDelimitedConsoleLoggerProviderCstrTest1()
        {
            // Arrange
            ILoggerProvider loggerProvider;

            // Act
            loggerProvider = new PipeDelimitedConsoleLoggerProvider();

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
            loggerProvider = new PipeDelimitedConsoleLoggerProvider();
            ILogger logger = loggerProvider.CreateLogger("PipeDelimitedConsoleLoggerProviderTests");

            // Assert
            Assert.IsNotNull(logger);
        }

    }
}