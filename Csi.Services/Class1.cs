using System;

namespace Csi.Services
{
    public class Result 
    {
        public bool Success {get;set;}
        

    }

    public interface IRegistrationServices
    {
        bool RegisterOrganization(string name);
        bool RegisterProject(string name);
        bool RegisterRequest(string name);
    }

    public class RequestTrackerServices
    {

    }
}
