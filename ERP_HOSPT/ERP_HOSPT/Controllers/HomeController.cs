using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP_HOSPT.Models;
using ERP_HOSPT.CollectionViewModels;
using ERP_HOSPT.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP_HOSPT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
           
            _context = context;

        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult interviews()
        {

            return View();
        }

        public IActionResult About()
        {
            var model = new CollectionOfAll
            {
                interviewss = _context.Interview.ToList(),
                Departss=_context.Depart.ToList(),
                   Patientss = _context.Patient.ToList(),
                Physicianss = _context.Physician.ToList(),
                Cityss = _context.City.ToList(),
                Regionss = _context.Region.ToList()





            };
            return View(model);

           
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
