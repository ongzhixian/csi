using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Csi.WebApp.Models;
using Microsoft.Extensions.Logging;

namespace Csi.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            this.logger.LogInformation("##### Hello world from Index() [INF]");
            this.logger.LogCritical("##### Hello world from Index() [CRT]");
            this.logger.LogDebug("##### Hello world from Index() [DBG]");
            this.logger.LogError("##### Hello world from Index() [ERR]");
            this.logger.LogWarning("##### Hello world from Index() [WRN]");
            this.logger.LogTrace("##### Hello trace message");

            
            using (Serilog.Context.LogContext.PushProperty("A", 1))
            {
                this.logger.LogInformation("Carries property A = 1");

                using (Serilog.Context.LogContext.PushProperty("A", 2))
                using (Serilog.Context.LogContext.PushProperty("B", 1))
                {
                    this.logger.LogInformation("Carries A = 2 and B = 1");
                }

                this.logger.LogInformation("Carries property A = 1, again");
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
