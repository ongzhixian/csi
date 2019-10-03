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
using Microsoft.AspNetCore.Http;

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

        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Test2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test2/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Test2/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Test2/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Test2/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test2/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
