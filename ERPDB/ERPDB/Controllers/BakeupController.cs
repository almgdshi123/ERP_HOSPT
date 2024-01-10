using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Configuration;
namespace ERPDB.Controllers
{

    public class BakeupController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        private readonly IHostingEnvironment env;
        public BakeupController(IHostingEnvironment env)
        {
            this.env = env;
        }


        public IActionResult Index()
        {
            string uploads = Path.Combine(env.WebRootPath, "Backup");
            string [] backupfiles = new DirectoryInfo(uploads).GetFiles().Select(o=> o.Name).ToArray();

            ViewBag.backup = backupfiles;
            return View();
            
        }
        [HttpPost]

        public IActionResult backup(string name)
        {
            con.ConnectionString = @"Server=.;database=ERPB_DB;Integrated Security=true;";

            string backupDIR = env.WebRootFileProvider.GetFileInfo(@"\Backup")?.PhysicalPath; 
            
           
            
                con.Open();
                cmd = new SqlCommand("backup database ERPB_DB to disk='" + backupDIR + "\\" +name+DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".Bak'", con);
                cmd.ExecuteNonQuery();
                con.Close();

            string uploads = Path.Combine(env.WebRootPath, "Backup");
            string[] backupfiles = new DirectoryInfo(uploads).GetFiles().Select(o => o.Name).ToArray();

            ViewBag.backup = backupfiles;
            return View("~/Views/Bakeup/Index.cshtml");

        }
        [HttpGet]
        [AllowAnonymous]

        public IActionResult backupDelete(int id)
        {
            string uploads = Path.Combine(env.WebRootPath, "Backup");
            string[] dilte = Directory.GetFiles(uploads);

            System.IO.File.Delete(dilte[id]);

            string[] backupfiles = new DirectoryInfo(uploads).GetFiles().Select(o => o.Name).ToArray();

            ViewBag.backup = backupfiles;

            return View("~/Views/Bakeup/Index.cshtml");


        }
        [HttpGet]


        public IActionResult restore(int id)
        {
            con.ConnectionString = @"Server=.;database=ERPB_DB;Integrated Security=true;";

            

                string backupDIR = env.WebRootFileProvider.GetFileInfo(@"\Backup")?.PhysicalPath;
                string[] dilte = Directory.GetFiles(backupDIR);

                string sqlStmt = string.Format("Restore database ERPB_DB from disk='{0}'  "+ " WITH REPLACE", dilte[id]);

                con.Open();
                con.ChangeDatabase("master");
            sqlStmt = string.Format("alter database ERPB_DB set SINGLE_USER WITH ROLLBACK IMMEDIATE");
            cmd = new SqlCommand(sqlStmt, con);
            cmd.ExecuteNonQuery();
                SqlCommand UseMasterCommand = new SqlCommand("DROP DATABASE ERPB_DB;", con);

                UseMasterCommand.ExecuteNonQuery();


                string Restore = string.Format("Restore database ERPB_DB from disk='{0}'", dilte[id]);
                SqlCommand RestoreCmd = new SqlCommand(Restore, con);
                RestoreCmd.ExecuteNonQuery();
                con.Close();


                string[] backupfiles = new DirectoryInfo(backupDIR).GetFiles().Select(o => o.Name).ToArray();


                ViewBag.backup = backupfiles;
            
            return View("~/Views/Bakeup/Index.cshtml");


        }




    }
}