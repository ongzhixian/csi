using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace Csi.Data.Tests
{
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            // Act
            // Assert

            System.Console.WriteLine("Console WriteLine message");

            System.Diagnostics.Debug.WriteLine("Debug WriteLine message");

            System.Diagnostics.Trace.WriteLine("Trace WriteLine message");

            Logger.LogMessage("A log message");

        }
    }
}
