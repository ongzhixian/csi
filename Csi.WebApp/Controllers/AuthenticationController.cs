using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Csi.WebApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Csi.WebApp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Csi.WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger logger;

        private readonly CsiSQLiteDbContext db;

        public AuthenticationController(ILogger<AuthenticationController> logger, CsiSQLiteDbContext context)
        {
            this.logger = logger;
            this.db = context;
        }

        public IActionResult Index()
        {
            this.logger.LogInformation("In -- AuthenticationController-Index");

            return View();
        }

        public IActionResult Login()
        {
            this.logger.LogInformation("In -- AuthenticationController-Login");

            // TODO: Leaving the rest as a TODO; do other stuff.
            // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-2.2

            return View();
        }

        // ZX: The "-Async" suffix for asynchronous methods is a convention, not REQUIREMENT
        // https://docs.microsoft.com/en-us/aspnet/mvc/overview/performance/using-asynchronous-methods-in-aspnet-mvc-4
        // https://docs.microsoft.com/en-us/aspnet/web-forms/overview/performance-and-caching/using-asynchronous-methods-in-aspnet-45
        public async Task<IActionResult> ZxLogin()
        {
            this.logger.LogInformation("In -- AuthenticationController-ZxLogin");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "zhixian@hotmail.com"),
                new Claim("FullName", "Zhi Xian Ong"),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);

            return View();
        }

        //[Route("Logout")]
        // LogoutAsync; ZX: The "-Async" suffix is a convention, not REQUIREMENT
        //public async Task<IActionResult> LogoutAsync() 
        public async Task<IActionResult> Logout()
        {
            this.logger.LogInformation("In -- AuthenticationController-Logout");

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return View();
        }

    }
}
