using System;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Csi.LookMeUp.Models
{
    [Obsolete]
    public partial class Constants
    {
        public const string CookieAuthScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }

    
}