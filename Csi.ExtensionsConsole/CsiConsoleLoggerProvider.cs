using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Csi.ExtensionsConsole
{
    public class CsiConsoleLoggerProvider : ILoggerProvider
    {
        
        public ILogger CreateLogger(string categoryName)
        {
            return new CsiConsoleLogger(categoryName);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CsiConsoleLoggerProvider() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }


        #endregion
        public class CsiConsoleLogger : ILogger
        {
            private string categoryName;

            public bool DisableColors { get; set; }


            public CsiConsoleLogger(string categoryName)
            {
                this.categoryName = categoryName;
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                if (logLevel == LogLevel.None)
                {
                    return false;
                }

                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                if (!IsEnabled(logLevel))
                {
                    return;
                }

                (ConsoleColor fg, ConsoleColor bg, string abbr) = GetLogLevelFormat(logLevel);

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"{DateTime.Now:O} ");

                Console.BackgroundColor = bg;
                Console.ForegroundColor = fg;
                Console.Write(abbr);

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($": [{eventId}] {categoryName}; {formatter(state, exception)}");

            }

            private (ConsoleColor, ConsoleColor, string) GetLogLevelFormat(LogLevel logLevel)
            {
                switch (logLevel)
                {
                    case LogLevel.Critical:
                        return (ConsoleColor.White, ConsoleColor.Red, "crit");
                    case LogLevel.Error:
                        return (ConsoleColor.Black, ConsoleColor.Red, "fail");
                    case LogLevel.Warning:
                        return (ConsoleColor.Yellow, ConsoleColor.Black, "warn");
                    case LogLevel.Information:
                        return (ConsoleColor.DarkGreen, ConsoleColor.Black, "info");
                    case LogLevel.Debug:
                        return (ConsoleColor.Gray, ConsoleColor.Black, "dbug");
                    case LogLevel.Trace:
                        return (ConsoleColor.Gray, ConsoleColor.Black, "trce");
                    default:
                        return (ConsoleColor.Gray, ConsoleColor.Black, string.Empty);
                }
            }


        }
    }

}
