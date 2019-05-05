# Serilog

```appsettings.json
  "Serilog": {
    "Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.RollingFile" ],
    "MinimumLevel": "Debug",
    "WriteTo": [
        {
            "Name": "Console",
            "Args": {
                "outputTemplate": "[{Timestamp:o} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}"
            }

        },
      { "Name": "File", "Args": { "path": ".\\Logs\\app.log" } },
        {
            "Name": "RollingFile",
            "Args": { "pathFormat": "C:/temp/log-{Date}.json" }
        }
    ],
    "Enrich": ["FromLogContext", "WithMachineName", "WithThreadId"],
    "Properties": {
		"Application": "Sample"
    }
  }
```


```Program.cs
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("runtime-settings.json", optional: true);
        })
        .UseStartup<Startup>()
        .UseSerilog((hostingContext, loggerConfiguration) => 
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration))
        ;
```