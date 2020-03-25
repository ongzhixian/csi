using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Csi.Extensions
{
    public static class ILoggerExtensions
    {
        public static void LogLevelTest(this ILogger log)
        {
            log.Log(LogLevel.None,          "LogLevel None          (    ) test."); // Severity: - No log output
            log.Log(LogLevel.Trace,         "LogLevel Trace         (trce) test."); // Severity: 1
            log.Log(LogLevel.Debug,         "LogLevel Debug         (dbug) test."); // Severity: 2
            log.Log(LogLevel.Information,   "LogLevel Information   (info) test."); // Severity: 3
            log.Log(LogLevel.Warning,       "LogLevel Warning       (warn) test."); // Severity: 4
            log.Log(LogLevel.Error,         "LogLevel Error         (fail) test."); // Severity: 5
            log.Log(LogLevel.Critical,      "LogLevel Critical      (crit) test."); // Severity: 6
        }

        public static void HelloWorld(this ILogger log)
        {
            log.Log(LogLevel.Information, "Hello world");
        }
    }
}
