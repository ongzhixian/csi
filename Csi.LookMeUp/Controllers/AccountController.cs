using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Csi.LookMeUp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace Csi.LookMeUp.Controllers
{
    public class AccountController : Controller
    {
        ILogger<AccountController> log = null;

        public AccountController(ILogger<AccountController> log)
        {
            this.log = log;
        }
        
        public IActionResult Login()
        {
            log.LogInformation("Login");

            //System.Web.HttpContext.
            // System.Web.Mvc.HttpContext.Sess


            return View();
        }

        //[Route("sign-out")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);



            // var props = new AuthenticationProperties();
            // await HttpContext.SignOutAsync("oidc", props);

            //await httpContext.SignOutAsync("Cookies");
            



            // options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            // options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            
            //WebSecurity.Logout();

            //await AuthenticationManager.SignOutAsync(DefaultAuthenticationTypes.ApplicationCookie);

            //await Context.Authentication.SignOutAsync(OpenIdConnectAuthenticationDefaults.AuthenticationScheme);

            //Context.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            // foreach (var cookie in Request.Cookies.Keys)
            // {
            //     //if (cookie == ".AspNetCore.Session")
            //         Response.Cookies.Delete(cookie);
            // }
            
            return View();

        }
    }
}
