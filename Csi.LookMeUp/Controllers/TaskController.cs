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
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Csi.LookMeUp.Controllers
{
    //[Authorize]
    public class TaskController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<TaskController> log = null;
        private readonly string connectionString;

        public TaskController(IConfiguration configuration, ILogger<TaskController> log)
        {
            this.configuration = configuration;
            this.log = log;
            this.connectionString = configuration["SQLite:LookMeUp"];
        }

        public IActionResult Index()
        {
            Services.SQLiteService svc = new Services.SQLiteService(this.connectionString);
            svc.CreateAppUserTableIfNotExists();

            IDbCommand cmd = svc.GetCommand();
            cmd.CommandText = "INSERT INTO app_user (display_name, provider_name, name_identifier) VALUES (?, ?, ?);";
            
            cmd.Parameters.Add(new System.Data.SQLite.SQLiteParameter { Value = "zxc" });
            cmd.Parameters.Add(new System.Data.SQLite.SQLiteParameter { Value = "google" });
            cmd.Parameters.Add(new System.Data.SQLite.SQLiteParameter { Value = "983274" });

            svc.Execute(cmd);
            
            // using (var transaction = connection.BeginTransaction())
            //     {
            //         var insertCmd = connection.CreateCommand();

            //         insertCmd.CommandText = "INSERT INTO favorite_beers VALUES('LAGUNITAS IPA')";
            //         insertCmd.ExecuteNonQuery();

            //         insertCmd.CommandText = "INSERT INTO favorite_beers VALUES('JAI ALAI IPA')";
            //         insertCmd.ExecuteNonQuery();

            //         insertCmd.CommandText = "INSERT INTO favorite_beers VALUES('RANGER IPA')";
            //         insertCmd.ExecuteNonQuery();

            //         transaction.Commit();
            //     }


            return View();
        }
        

        

    }
}
