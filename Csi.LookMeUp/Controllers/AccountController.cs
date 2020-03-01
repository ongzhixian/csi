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
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Csi.LookMeUp.Controllers
{
    public interface IAccountManagement
    {
        void Register(string username, string password);
        
        
    }

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

            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync([FromForm]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                log.LogInformation("Username={0}|Password={1}",model.Username, model.Password);

                //model.Username
                //model.Password
                if (model.Username.Equals("zong", StringComparison.InvariantCultureIgnoreCase))
                {
                    IEnumerable<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>
                    {
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, model.Username),
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.GivenName, model.Username),
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "Member")
                    };

                    await HttpContext.SignInAsync(
                        new System.Security.Claims.ClaimsPrincipal(
                            new System.Security.Claims.ClaimsIdentity(
                                claims,
                                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                                System.Security.Claims.ClaimTypes.Name,
                                System.Security.Claims.ClaimTypes.Role
                            )
                        ), 
                        new AuthenticationProperties {  
                            RedirectUri = "/Home/Index"
                        });

                    //return View("LoginOK", model);
                }
                    
                // else
                // {
                //     return View("LoginNG", model);

                // }

                return RedirectToAction("Index", "Home");
                    
            }

            return View(model);

        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.Google.GoogleDefaults.AuthenticationScheme)]
        public IActionResult GoogleSignIn()
        {
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Register()
        {
            log.LogInformation("Register");

            return View(new RegisterAccountViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([FromForm]RegisterAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
            }

            return View(model);

        }


        public IActionResult LoginOK(LoginViewModel model)
        {
            log.LogInformation("Login OK");

            return View(model);
        }

        public IActionResult LoginNG(LoginViewModel model)
        {
            log.LogInformation("Login NG");

            return View(model);
        }

        //[Route("sign-out")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                // prop.RedirectUri = "https://www.google.com/accounts/Logout";
                //RedirectUri = "https://localhost:35001/Home/Index"
                //RedirectUri = "https://accounts.google.com/Logout?continue=https://localhost:35001/Home/Index"
                // https://accounts.google.com/Logout?continue=https://localhost:35001/Home/Index
            });

            AuthenticationProperties prop = new AuthenticationProperties();
            prop.RedirectUri = "/Home/Index";
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, prop);
            return View();

        }
    }
}
