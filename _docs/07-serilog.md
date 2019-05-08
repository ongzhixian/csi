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


Level       Usage
Verbose     Verbose is the noisiest level, rarely (if ever) enabled for a production app.
            This is Serilog's equivalent of Trace level (not to be confused with TraceListener)
Debug	    Debug is used for internal system events that are not necessarily observable from the outside, 
            but useful when determining how something happened.
Information Information events describe things happening in the system that correspond to its responsibilities and functions.
            Generally these are the observable actions the system can perform.
Warning	    When service is degraded, endangered, or may be behaving outside of its expected parameters, 
            Warning level events are used.
Error	    When functionality is unavailable or expectations broken, an Error event is used.
Fatal	    The most critical level, Fatal events demand immediate attention.