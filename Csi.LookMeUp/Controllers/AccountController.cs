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

            return View();
        }

        //[Route("sign-out")]
        public async Task<IActionResult> Logout()
        {
            AuthenticationProperties prop = new AuthenticationProperties();

            prop.RedirectUri = "https://www.google.com/accounts/Logout";
            await HttpContext.SignOutAsync(prop);
            return View();

        }
    }
}
