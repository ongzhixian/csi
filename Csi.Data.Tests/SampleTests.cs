using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace Csi.Data.Tests
{
    [TestClass]
    public class SampleTests
    {
        static IConfiguration configuration = null;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile("runtime-settings.json", optional: true, reloadOnChange: true)
                .Build();

            Logger.LogMessage("[{0}] configuration keys found.",
                configuration.AsEnumerable().Count()
            );
            foreach (KeyValuePair<string, string> kvp in configuration.AsEnumerable())
            {
                Logger.LogMessage("Key:[{0}], Value:[{1}]", kvp.Key, kvp.Value);
            }
            
            Logger.LogMessage("Value of dummy_clientId1 is [{0}]", configuration["dummy_clientId1"]);
            Logger.LogMessage("CsiDatabase connectionString is: [{0}]", configuration.GetConnectionString("CsiDatabase"));
        }

        [TestMethod]
        public void OutputTest()
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
