using System;

namespace Csi.Data
{
    [Obsolete]
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
