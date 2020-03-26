using Microsoft.Extensions.Logging;
using System;

namespace Csi.Loggers
{
    public class PipeDelimitedConsoleLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new PipeDelimitedConsoleLogger(categoryName);
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
        // ~LinearConsoleLoggerProvider() {
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

        ////////////////////////////////////////////////////////////////////////////////
        // Inline classes

        public class PipeDelimitedConsoleLogger : ILogger
        {
            private string categoryName { get; set; }

            public PipeDelimitedConsoleLogger(string categoryName)
            {
                this.categoryName = categoryName;
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null; // [ZX: 2020-03-26] Don't care; We do not support it currently.
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                if (LogLevel.None == logLevel)
                    return false;

                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                if (!IsEnabled(logLevel))
                    return;

                string formmatedString = formatter(state, exception);

                if (string.IsNullOrEmpty(formmatedString))
                    Console.WriteLine($"{logLevel}|{eventId}|{categoryName}");
                else
                    Console.WriteLine($"{logLevel}|{eventId}|{categoryName}|{formmatedString}");
            }
        }
    }
}
