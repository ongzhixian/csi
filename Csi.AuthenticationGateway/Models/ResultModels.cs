using System;

namespace Csi.Data.Models
{
    // Namespace for common data models used through out Csi

    public static class AppDateTime2
    {
        private static Func<DateTime> nowFunc = () => DateTime.Now;
        private static Func<DateTime> utcNowFunc = () => DateTime.UtcNow;


        public static DateTime Now => nowFunc();

        public static DateTime UtcNow => utcNowFunc();

        public static void Reset()
        {
            nowFunc = () => DateTime.Now;
            utcNowFunc = () => DateTime.UtcNow;
        }

        public static void Set(DateTime dateTime)
        {
            nowFunc = () => dateTime;
            utcNowFunc = () => dateTime.ToUniversalTime();
        }


        //public static DateTime UtcNow
        //{
        //    get
        //    {
        //        return dateTime.HasValue ? dateTime.Value.ToUniversalTime() : DateTime.UtcNow;
        //    }
        //    set
        //    {
        //        dateTime = value;
        //    }
        //}

        //public static void Reset()
        //{
        //    dateTime = null;
        //}

    }

    public static class AppDateTime
    {
        private static DateTime? dateTime = null;
        
        public static DateTime Now {
            get {
                return dateTime.HasValue ? dateTime.Value : DateTime.Now;
            }
            set {
                dateTime = value;
            }
        }

        public static DateTime UtcNow {
            get {
                return dateTime.HasValue ? dateTime.Value.ToUniversalTime() : DateTime.UtcNow;
            }
            set {
                dateTime = value;
            }
        }

        public static void Reset()
        {
            dateTime = null;
        }

    }

    // public static class SystemTime
    // {
    //     /// <summary> Normally this is a pass-through to DateTime.Now, but it can be overridden with SetDateTime( .. ) for testing or debugging.
    //     /// </summary>
    //     public static Func<DateTime> Now = () => DateTime.Now;
    //
    //     /// <summary> Set time to return when SystemTime.Now() is called.
    //     /// </summary>
    //     public static void SetDateTime(DateTime dateTimeNow)
    //     {
    //         Now = () => dateTimeNow;
    //     }
    //
    //     /// <summary> Resets SystemTime.Now() to return DateTime.Now.
    //     /// </summary>
    //     public static void ResetDateTime()
    //     {
    //         Now = () => DateTime.Now;
    //     }
    // }

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