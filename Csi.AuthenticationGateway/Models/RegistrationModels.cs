using System;

namespace Csi.AuthenticationGateway.Models
{
    public interface ICredential
    {
        string Username { get; set; }
        string Password { get; set; }
    }

    public class CredentialRegistrationRequest : SecureRequest, ICredential
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool HasEmptyFields()
        {
            if (string.IsNullOrWhiteSpace(this.Username))
                return true;
            if (string.IsNullOrWhiteSpace(this.Password))
                return true;
            if (string.IsNullOrWhiteSpace(this.SecurityKey))
                return true;

            return false;
        }
    }

    public class CredentialRegistrationResult : ResponseResult
    {
    }



}