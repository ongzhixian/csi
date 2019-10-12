using System;
using Csi.DemoConsole.SamplePrograms;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace Csi.DemoConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("[PROGRAM START]");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            Logger log = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            Log.Logger = log; // Wired it back to the static Log instance

            Log.Verbose(    "Test Verbose     message");
            Log.Debug(      "Test Debug       message");
            Log.Information("Test Information message");
            Log.Warning(    "Test Warning     message");
            Log.Error(      "Test Error       message");
            Log.Fatal(      "Test Fatal       message");

            Console.WriteLine("Number of arguments: {0}", args.Length);
            
            
            ISampleProgram app = new IrisClustering();
                //new PricePredictor();
                //new IssueClassification();
                //new SentimentAnalysis();
            app.DoWork();

            Console.WriteLine("[PROGRAM END!!]");
        }
    }
}
