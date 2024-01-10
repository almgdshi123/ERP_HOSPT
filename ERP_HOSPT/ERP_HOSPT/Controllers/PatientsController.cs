using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP_HOSPT.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ERP_HOSPT.Models;
using Microsoft.Extensions.Configuration;

namespace ERP_HOSPT.Controllers
{
    [Authorize(Roles = "المسجل عام")]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;


        public PatientsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;

            _context = context;
        }
        [AllowAnonymous]

        public async Task<IActionResult> show(int id)
        {
            var applicationDbContext = _context.Patient.Include(p => p.Region).Include(p => p.city).Where(a => a.PatientId == id);
            
                var applica = _context.Interview.Include(i => i.patient).Include(i => i.physician).Include(i => i.user).Where(a => a.PatientId == id);
                ViewBag.Interview = await applica.ToListAsync();

            return View("~/Views/Patients/Index.cshtml", await applicationDbContext.ToListAsync());



        }

        [Authorize(Roles = "مدير")]
        [AllowAnonymous]

        // GET: Patients
        public async Task<IActionResult> Index([Bind("PatientId,Patientname,pa_nat,pa_addr,pa_job,pa_data,pa_sex,pa_mobile,pa_phone,pa_email,pa_note,cityId,RegionId")] Patient patient)
        {
            int id =0;
          
            if (patient.PatientId == 2)
            {
                id = int.Parse(patient.Patientname);
            }
            IConfigurationBuilder builder = new ConfigurationBuilder();
            IConfigurationRoot configuration = builder.AddJsonFile("key.json", false, reloadOnChange: true).Build();
           

            var compny = _context.Company.Find(configuration.GetSection("key").Value);
            if (compny != null)
            {
                if (compny.State == 1)
                {

                    

                    var applicationDbContext = _context.Patient.Include(p => p.Region).Include(p => p.city).Where(a => a.Patientname == patient.Patientname || a.pa_mobile == patient.Patientname || a.pa_nat == patient.Patientname || a.PatientId == id);
                    if (ViewData["data"] != null)
                    {
                        int ii =int.Parse(ViewData["data"].ToString());
                        var applica = _context.Interview.Include(i => i.patient).Include(i => i.physician).Include(i => i.user).Where(a => a.PatientId ==ii );
                        ViewBag.Interview = await applica.ToListAsync();
                    }
                    return View(await applicationDbContext.ToListAsync());
                }
                ViewBag.key = false;
                return View();


            }
            else
            {
                ViewBag.key = false;
                return View();
            }

        }



        // GET: Patients/Create
        public IActionResult Create(int id = 0)
        {
            if (id == 0)
                ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "reg_name");
            ViewData["cityId"] = new SelectList(_context.City, "cityId", "cit_name");
            return View(new Patient());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("PatientId,Patientname,pa_nat,pa_addr,pa_job,pa_data,pa_sex,pa_mobile,pa_phone,pa_email,pa_note,cityId,RegionId")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {

                    _context.Add(patient);
                    await _context.SaveChangesAsync();

                }
                else
                {
                   return NotFound();
                 }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllPatients", _context.Patient.Include(p => p.Region).Include(p => p.city).Where(a => a.Patientname == patient.Patientname).ToList()) });
            }

            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "reg_name", patient.RegionId);
            ViewData["cityId"] = new SelectList(_context.City, "cityId", "cit_name", patient.cityId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", patient) });
        }





        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int id = 0)
        {
            if (id == 0)

                return View(new Patient());
            else
            {

                var patient = await _context.Patient.FindAsync(id);
                if (patient == null)
                {
                    return NotFound();
                }
                ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "reg_name", patient.RegionId);
                ViewData["cityId"] = new SelectList(_context.City, "cityId", "cit_name", patient.cityId);
                return View(patient);
            }
        }






        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,Patientname,pa_nat,pa_addr,pa_job,pa_data,pa_sex,pa_mobile,pa_phone,pa_email,pa_note,cityId,RegionId")] Patient patient)
        {
         

            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {

                    _context.Add(patient);
                    await _context.SaveChangesAsync();

                }
                //Update(patient);

                else
                {
                    try
                    {
                        _context.Update(patient);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PatientExists(patient.PatientId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllPatients", _context.Patient.Include(p => p.Region).Include(p => p.city).ToList()) });
            }
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "reg_name", patient.RegionId);
            ViewData["cityId"] = new SelectList(_context.City, "cityId", "cit_name", patient.cityId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", patient) });
        }




        // GET: Patients/Edit/5
        public async Task<IActionResult> CreateInterview(int id )
        {

            IConfigurationBuilder builder = new ConfigurationBuilder();
            IConfigurationRoot configuration = builder.AddJsonFile("key.json", false, reloadOnChange: true).Build();
            var patient = await _context.Patient.FindAsync(id);
            ViewBag.id = id;
            ViewBag.name = patient.Patientname;
            ViewData["PhysicianId"] = new SelectList(_context.Physician.Where(a => a.CompanyId == configuration.GetSection("key").Value), "PhysicianId", "phy_name");
            ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName");
            return View(new interview());
        }






        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInterview(int id, [Bind("interviewId,inter_type,inter_date,inter_notes,userId,PatientId,PhysicianId")]interview interview)
        {
            interview.PatientId = id;
            interview.inter_date = interview.inter_date;
            var usr = userManager.GetUserId(User);
            interview.userId = usr;

            IConfigurationBuilder builder = new ConfigurationBuilder();
            IConfigurationRoot configuration = builder.AddJsonFile("key.json", false, reloadOnChange: true).Build();
            interview.CompanyId = configuration.GetSection("key").Value;


            _context.Add(interview);
            await _context.SaveChangesAsync();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllPatients", _context.Patient.Include(p => p.Region).Include(p => p.city).Where(a => a.PatientId == interview.PatientId). ToList()) });


        }
















        // GET: Patients/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var patient = await _context.Patient
        //        .Include(p => p.Region)
        //        .Include(p => p.city)
        //        .SingleOrDefaultAsync(m => m.PatientId == id);
        //    if (patient == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(patient);
        //}

        //// POST: Patients/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patient.SingleOrDefaultAsync(m => m.PatientId == id);
            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.PatientId == id);
        }
    


      





        //// POST: Patients/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("PatientId,Patientname,pa_nat,pa_addr,pa_job,pa_data,pa_sex,pa_mobile,pa_phone,pa_email,pa_note,cityId,RegionId")] Patient patient)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(patient);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "reg_name", patient.RegionId);
        //    ViewData["cityId"] = new SelectList(_context.City, "cityId", "cit_name", patient.cityId);
        //    return View(patient);
        //}
        // GET: Patients/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var patient = await _context.Patient
        //        .Include(p => p.Region)
        //        .Include(p => p.city)
        //        .SingleOrDefaultAsync(m => m.PatientId == id);
        //    if (patient == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(patient);
        //}
    }
}
