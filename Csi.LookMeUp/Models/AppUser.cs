using System;

namespace Csi.LookMeUp.Models
{
    public interface IAppUser
    {
        string displayName;
        string AuthenticationType;

        string DisplayName { get; set; }
    } 

    public class AppUserModel : IAppUser
    {
        public string DisplayName;

        public string AuthenticationType;

        
    }
}