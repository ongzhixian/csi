using System;

namespace Csi.AuthenticationGateway.Models
{    
    public interface IRequest
    {
    }

    public class SecureRequest : IRequest
    {
        public string SecurityKey { get; set; }
    }
}