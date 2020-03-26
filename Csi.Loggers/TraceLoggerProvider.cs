using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Csi.Loggers
{
    public class TraceLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new TraceLogger(categoryName);
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
        // ~TraceLoggerProvider() {
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

        public class TraceLogger : ILogger
        {
            private string categoryName { get; set; }
            public TraceLogger(string categoryName)
            {
                this.categoryName = categoryName;
            }
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
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
                    Trace.WriteLine($"{logLevel}|{eventId}|{categoryName}");
                else
                    Trace.WriteLine($"{logLevel}|{eventId}|{categoryName}|{formmatedString}");
            }
        }
    }
}
