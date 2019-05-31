using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Csi.WebApp.Areas.Research
{
    [Area("Research")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}