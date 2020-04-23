using System;

namespace Csi.Data.Models
{
    // Namespace for common data models used through out Csi

    public static sealed class AppDateTime
    {
        private DateTime appDateTime;

        public DateTime UtcNow 
        {
            get {
                
                appDateTime.
            }
            set {

            }
        }
    }

    public interface IRequest
    {
        string RequestType { get; set; }
        DateTime RequestDate { get; set; }
    }

    public class Request : IRequest
    {
        public string RequestType { get; set; }
        public DateTime RequestDate { get; set; }

        public Request()
        {
            this.RequestType = nameof(Request);
            this.DateTime = DateTime.UtcNow;
        }
    }



    public interface IResult
    {
        bool Ok { get; set; }
        string Message { get; set; }
        Exception Exception { get; set; }
    }

}

namespace Csi.AuthenticationGateway.Models
{
    public interface IResult
    {
        bool Ok { get; set; }
        string Message { get; set; }
        Exception Exception { get; set; }
    }

    public class Result : IResult
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }

    public interface IResult<T> : IResponseResult where T : class
    {
        T Result { get; set; }
    }




    public interface IResponseResult
    {
        bool Ok { get; set; }
        string Message { get; set; }
        Exception Exception { get; set; }
    }

    public class ResponseResult : IResponseResult
    {
        public bool Ok { get; set; }
        public string Message { get; set; }

        public Exception Exception { get; set; }
    }

    public interface IResponseResult<T> : IResponseResult where T : class
    {
        T Result { get; set; }
    }

    public class ResponseResult<T> : IResponseResult<T> where T : class
    {
        public T Result { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Ok { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Exception Exception { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }


}