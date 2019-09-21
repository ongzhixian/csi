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

namespace Csi.WebApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ILogger logger;

        private readonly CsiSQLiteDbContext db;

        public TaskController(ILogger<TaskController> logger, CsiSQLiteDbContext context)
        {
            this.logger = logger;
            this.db = context;
        }

        public IActionResult Index()
        {
            this.logger.LogInformation("In -- TaskController-Index");

            return View();
        }

    }
}
