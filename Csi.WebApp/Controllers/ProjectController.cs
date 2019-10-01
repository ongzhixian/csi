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
    public class ProjectController : Controller
    {
        private readonly ILogger logger;

        private readonly CsiSQLiteDbContext db;

        public ProjectController(ILogger<ProjectController> logger, CsiSQLiteDbContext context)
        {
            this.logger = logger;
            this.db = context;
            
        }

        public IActionResult Index()
        {
            this.logger.LogInformation("In -- ProjectController-Index");

            using (var db = new CsiSQLiteDbContext())
            {
            }

            Csi.Data.SimpleProject project = new Csi.Data.SimpleProject(
                string.Format("TestProc{0}", DateTime.Now.ToString("yyyyMMddHHmmss"))
                );
            this.db.Projects.Add(project);

            this.db.SaveChanges();
            // db.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
            // var count = db.SaveChanges();
            // Console.WriteLine("{0} records saved to database", count);

            // Console.WriteLine();
            // Console.WriteLine("All blogs in database:");
            // foreach (var blog in db.Blogs)
            // {
            //     Console.WriteLine(" - {0}", blog.Url);
            // }
            

            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

    }
}
