using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP_HOSPT.Data;
using Microsoft.AspNetCore.Identity;
using ERP_HOSPT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace ERP_HOSPT.Controllers
{
   
    [Authorize(Roles = "المسجل عام")]
    public class interviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;

        public interviewsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            _context = context;
            _configuration = configuration;


        }

        // GET: interviews

        [AllowAnonymous]
        public async Task<IActionResult> Index(int id=0)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            IConfigurationRoot configuration = builder.AddJsonFile("key.json", false, reloadOnChange: true).Build();

            if (id != 0)
            {

                var applica = _context.Interview.Include(i => i.patient).Include(i => i.physician).Include(i => i.user).Where(a => a.PatientId == id);
                return View(await applica.ToListAsync());
            }
            var usr = userManager.GetUserId(User);

            //ApplicationUser applicationUser = await userManager.GetUserAsync(User);
            //string usrid = User.FindFirst("id").Value;
            //var usr = await userManager.FindByEmailAsync(User.Identity.Name);
            if (User.IsInRole("المسجل عام"))
            {

                var applica = _context.Interview.Include(i => i.patient).Include(i => i.physician).Include(i => i.user).Where(a => a.userId == usr && a.CompanyId== configuration.GetSection("key").Value);
                return View(await applica.ToListAsync());
            }

            if (User.IsInRole("دكتور"))
            {
                var p = _context.Physician.FirstOrDefault(a => a.userId == usr);

                var applica = _context.Interview.Include(i => i.patient).Include(i => i.physician).Include(i => i.user).Where(a => a.PhysicianId == p.PhysicianId && a.CompanyId == configuration.GetSection("key").Value);
                return View(await applica.ToListAsync());
            }


            var applicationDbContext = _context.Interview.Include(i => i.patient).Include(i => i.physician).Include(i => i.user).Where(a => a.userId == usr);
            return View(await applicationDbContext.ToListAsync());


        }


        [AllowAnonymous]

        public async Task<IActionResult> show(int id)
        {
               var interview = await _context.Interview.FindAsync(id);

            IConfigurationBuilder builder = new ConfigurationBuilder();
            IConfigurationRoot configuration = builder.AddJsonFile("key.json", false, reloadOnChange: true).Build();


            var compny = _context.Company.Find(configuration.GetSection("key").Value);
            if (compny != null)
            {
                if (compny.State == 1)
                {
                    ViewBag.key = true;


                    var applica = _context.Interview.Include(i => i.patient).Include(i => i.physician).Include(i => i.user).Where(a => a.PatientId == interview.PatientId);
                    ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", interview.PatientId);
                    ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", interview.PhysicianId);
                    ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName", interview.userId);
                    ViewBag.Interview = await applica.ToListAsync();
                }
                else
                {
                    ViewBag.key = false;

                }

            }
            else
            {
                ViewBag.key = false;
            }

            return View("~/Views/interviews/Edit1.cshtml", interview);
            }

        // GET: interviews/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var interview = await _context.Interview
        //        .Include(i => i.patient)
        //        .Include(i => i.physician)
        //        .Include(i => i.user)
        //        .SingleOrDefaultAsync(m => m.interviewId == id);
        //    if (interview == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(interview);
        //}

        // GET: interviews/Create
        public IActionResult Create(int id = 0)
        {
            if (id == 0)
                ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname");
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name");
            ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName");
            return View(new interview());
        }
       
        // POST: interviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("interviewId,inter_type,inter_date,inter_notes,userId,PatientId,PhysicianId")] interview interview)
        {
            if (ModelState.IsValid)
            {

                if (id == 0)
                {
                   
                    _context.Add(interview);
                    await _context.SaveChangesAsync();
                   
                }
                else
                {
                    return NotFound();
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllinterview", _context.Interview.Include(i => i.patient).Include(i => i.physician).Include(i => i.user).ToList()) });

            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", interview.PatientId);
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", interview.PhysicianId);
            ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName", interview.userId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", interview) });
        }




        [HttpGet]
        // GET: interviews/Edit/5
        public async Task<IActionResult> Edit(int ?id )
        {
            if (id == null)

                return NotFound();
            else
            {
                var interview = await _context.Interview.FindAsync(id);
                if (interview == null)
                {
                    return NotFound();
                }
                ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", interview.PatientId);
                ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", interview.PhysicianId);
                ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName", interview.userId);
                return View(interview);
            }
        }



       
      
      
        // POST: interviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        public async Task<IActionResult> Edit(int id, [Bind("interviewId,inter_type,inter_date,inter_notes,userId,PatientId,PhysicianId")] interview interview)
        {
           
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {

                    return NotFound();
                }
                else
                {
                    try
                    {

                        _context.Update(interview);
                        interview.State = true;
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!interviewExists(interview.interviewId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
              return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllinterview", _context.Interview.Include(i => i.patient).Include(i => i.physician).Include(i => i.user).ToList()) });

            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", interview.PatientId);
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", interview.PhysicianId);
            ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName", interview.userId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", interview) });
        }






       
        [HttpGet]
        [Authorize(Roles = "دكتور")]
        [AllowAnonymous]
        // GET: interviews/Edit/5
        public async Task<IActionResult> Edit1(int id = 0)
        {
            if (id == 0)

                return NotFound();
            else
            {
                var interview = await _context.Interview.FindAsync(id);

                interview.Stateinterview = true;
                _context.Update(interview);
                _context.SaveChanges();

                ViewBag.stas = _context.R_analysis.FirstOrDefault(e => e.interviewId == interview.interviewId && e.State1 == true && e.trboll==false);
                if (interview == null)
                {
                    return NotFound();
                }

                ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", interview.PatientId);
                ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", interview.PhysicianId);
                ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName", interview.userId);
                return View(interview);
            }
        }


        [HttpGet]
        [Authorize(Roles = "دكتور")]
        [AllowAnonymous]
        // GET: interviews/Edit/5
        public async Task<IActionResult> Invoice(int id = 0)
        {
            var r_Analysis = await _context.R_analysis.FindAsync(id);
            ViewBag.Patient = _context.Patient.Find(r_Analysis.PatientId);
            ViewBag.Physician = _context.Physician.Find(r_Analysis.PhysicianId);
            ViewBag.Analysis = _context.Analysis.Find(r_Analysis.AnalysisId);



            return View(r_Analysis);
        }
        [HttpPost]
        [Authorize(Roles = "دكتور")]
        [AllowAnonymous]
        public async Task<IActionResult> Invoice(int id, R_analysis r_ )
        {
            var r_Analysis = await _context.R_analysis.FindAsync(id);
            ViewBag.Patient = _context.Patient.Find(r_Analysis.PatientId); 
            r_Analysis.trboll = true;
            _context.Update(r_Analysis);
            _context.SaveChanges();


            var interview = await _context.Interview.FindAsync(r_Analysis.interviewId);


            var applicationDbContext = _context.R_analysis.Where(r => r.interviewId == r_Analysis.interviewId);
            ViewBag.Analysis = applicationDbContext;
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", interview.PatientId);
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", interview.PhysicianId);
            ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName", interview.userId);
            return View("~/Views/interviews/Edit1.cshtml", interview);

        }

        [HttpGet]
        [Authorize(Roles = "دكتور")]
        [AllowAnonymous]
        public async Task<IActionResult> InvoiceDrug(int id )
        {
            var diagnosis  = await _context.Diagnosis.FirstOrDefaultAsync(d => d.interviewId==id);
            ViewBag.Patient = _context.Patient.Find(diagnosis.PatientId);
            ViewBag.Physician = _context.Physician.Find(diagnosis.PhysicianId);



            return View(diagnosis);


        }
        [HttpGet]
        [Authorize(Roles = "دكتور")]
        [AllowAnonymous]
        public async Task<IActionResult> InvoiceDiagnosis(int id)
        {
            var diagnosis = await _context.Diagnosis.FirstOrDefaultAsync(d => d.interviewId == id);
            ViewBag.Patient = _context.Patient.Find(diagnosis.PatientId);
            ViewBag.Physician = _context.Physician.Find(diagnosis.PhysicianId);



            return View(diagnosis);


        }


        [HttpGet]
        [Authorize(Roles = "دكتور")]
        [AllowAnonymous]
        public async Task<IActionResult> InvoiceDrug1(int id)
        {
            var diagnosis = await _context.Diagnosis.FirstOrDefaultAsync(d => d.interviewId == id);
            ViewBag.Patient = _context.Patient.Find(diagnosis.PatientId);
            ViewBag.Physician = _context.Physician.Find(diagnosis.PhysicianId);



            return View(diagnosis);


        }
        [HttpGet]
        [Authorize(Roles = "دكتور")]
        [AllowAnonymous]
        public async Task<IActionResult> InvoiceDiagnosis1(int id)
        {
            var diagnosis = await _context.Diagnosis.FirstOrDefaultAsync(d => d.interviewId == id);
            ViewBag.Patient = _context.Patient.Find(diagnosis.PatientId);
            ViewBag.Physician = _context.Physician.Find(diagnosis.PhysicianId);



            return View(diagnosis);


        }
        [HttpGet]
        [Authorize(Roles = "دكتور")]
        [AllowAnonymous]
        public async Task<IActionResult> InvoiceDrug2(int id)
        {
            var diagnosis = await _context.Diagnosis.FirstOrDefaultAsync(d => d.interviewId == id);
            ViewBag.Patient = _context.Patient.Find(diagnosis.PatientId);
            ViewBag.Physician = _context.Physician.Find(diagnosis.PhysicianId);



            return View(diagnosis);


        }
        [HttpGet]
        [Authorize(Roles = "دكتور")]
        [AllowAnonymous]
        public async Task<IActionResult> InvoiceDiagnosis2(int id)
        {
            var diagnosis = await _context.Diagnosis.FirstOrDefaultAsync(d => d.interviewId == id);
            ViewBag.Patient = _context.Patient.Find(diagnosis.PatientId);
            ViewBag.Physician = _context.Physician.Find(diagnosis.PhysicianId);



            return View(diagnosis);


        }


        // POST: interviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [Authorize(Roles = "دكتور")]

        [AllowAnonymous]
        public async Task<IActionResult> Edit1(int id, [Bind("interviewId,inter_type,inter_date,inter_notes,userId,PatientId,PhysicianId")] interview interview)
        {

            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {

                    return NotFound();
                }
                else
                {
                    try
                    {
                       interview = await _context.Interview.FindAsync(id);

                        interview.State = true;
                        _context.Update(interview);

                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!interviewExists(interview.interviewId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                var usr = userManager.GetUserId(User);
                                var p = _context.Physician.FirstOrDefault(a => a.userId == usr);


                return View("~/Views/interviews/Index.cshtml", ( _context.Interview.Include(i => i.patient).Include(i => i.physician).Include(i => i.user).Where(a => a.PhysicianId == p.PhysicianId).ToList()));

            }
        

           
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", interview.PatientId);
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", interview.PhysicianId);
            ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName", interview.userId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit1", interview) });
        }











        // GET: interviews/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var interview = await _context.Interview
        //        .Include(i => i.patient)
        //        .Include(i => i.physician)
        //        .Include(i => i.user)
        //        .SingleOrDefaultAsync(m => m.interviewId == id);
        //    if (interview == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(interview);
        //}

        //// POST: interviews/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interview = await _context.Interview.SingleOrDefaultAsync(m => m.interviewId == id);
            _context.Interview.Remove(interview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool interviewExists(int id)
        {
            return _context.Interview.Any(e => e.interviewId == id);
        }
       

    }
}
