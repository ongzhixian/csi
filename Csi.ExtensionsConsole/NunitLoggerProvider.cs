using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Csi.ExtensionsConsole
{
    public interface IUnitTestLogger : ILogger 
    {
        string CategoryName { get; set; }
        LogLevel LogLevel { get; set; }
        EventId EventId { get; set; }
        object State { get; set; }
        Exception Exception { get; set; }
        string FormattedString { get; set; }
    }

    [Serializable]
    public class LogEntryState
    {
        public string CategoryName { get; set; }
        public LogLevel LogLevel { get; set; }
        public EventId EventId { get; set; }
        public object State { get; set; }
        public Exception Exception { get; set; }
        public string FormattedString { get; set; }
    }

    public class UnitTestLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new UnitTestLogger(categoryName);
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
        // ~NunitLoggerProvider() {
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

        public class UnitTestLogger : IUnitTestLogger
        {
            public string CategoryName { get; set; }
            public LogLevel LogLevel { get; set; }
            public EventId EventId { get; set; }
            public object State { get; set; }
            public Exception Exception { get; set; }
            public string FormattedString { get; set; }

            public UnitTestLogger(string categoryName)
            {
                this.CategoryName = categoryName;
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                if (logLevel == LogLevel.None)
                    return false;

                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                string formmatedString = formatter(state, exception);

                if (string.IsNullOrEmpty(formmatedString))
                {
                    System.Diagnostics.Trace.WriteLine($"{logLevel}|{eventId}|{CategoryName}");
                }
                else
                {
                    System.Diagnostics.Trace.WriteLine($"{logLevel}|{eventId}|{CategoryName}|{formmatedString}");
                }


                //System.Diagnostics.Trace.WriteLine($"{logLevel}|{eventId}|{state}|{exception}|{formatter(state, exception)}");


                // Tried passing serialized XML via XmlSerializer
                // Result: Does not work well.
                // Remarks: EventId and State cannot be serialized
                //LogEntryState logEntryState = new LogEntryState
                //{
                //    LogLevel = logLevel,
                //    EventId = eventId,
                //    State = state,
                //    Exception = new Exception("SAM"),
                //    FormattedString = formatter(state, exception),
                //    CategoryName = this.CategoryName
                //};
                //System.IO.StringWriter sw = new System.IO.StringWriter();
                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //binaryFormatter.Serialize(ms, logEntryState);
                //System.Diagnostics.Trace.WriteLine(Convert.ToBase64String(ms.ToArray()));



                // Tried passing serialized XML via XmlSerializer
                // Result: Does not work well.
                // Remarks: State and Exception cannot be serialized
                //System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(LogEntryState));
                //LogEntryState logEntryState = new LogEntryState
                //{
                //    LogLevel = logLevel,
                //    EventId = eventId,
                //    State = state,
                //    Exception = exception,
                //    FormattedString = formatter(state, exception),
                //    CategoryName = this.CategoryName
                //};
                //using (System.IO.StringWriter sw = new System.IO.StringWriter())
                //{
                //    x.Serialize(sw, logEntryState);
                //    System.Diagnostics.Trace.WriteLine(sw.ToString());
                //}



                // Works ok
                //System.Diagnostics.Trace.WriteLine(logLevel, "LogLevel");
                //System.Diagnostics.Trace.WriteLine(eventId, "EventId");
                //System.Diagnostics.Trace.WriteLine(state, "State");
                //System.Diagnostics.Trace.WriteLine(exception, "Exception");
                //System.Diagnostics.Trace.WriteLine(formatter(state, exception), "FormattedString");

                // Nope does not work
                //this.LogLevel = logLevel;
                //this.EventId = eventId;
                //this.State = state;
                //this.Exception= exception;
                //this.FormattedString = formatter(state, exception);
            }
        }
    }
}
