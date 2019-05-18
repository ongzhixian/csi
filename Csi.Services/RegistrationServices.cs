using System;

namespace Csi.Services
{
    public interface IRegistrationServices
    {
        bool RegisterProject(string name);
    }

    public class RegistrationServices : IRegistrationServices
    {
        public bool RegisterProject(string name)
        {
            throw new NotImplementedException();
        }
    }
}
