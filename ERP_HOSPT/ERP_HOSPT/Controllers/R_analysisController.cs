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

namespace ERP_HOSPT.Controllers
{
    public class R_analysisController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> userManager;


        public R_analysisController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            _context = context;
        }

        // GET: R_analysis
        public async Task<IActionResult> Index(int id)
        {
          

            
            var applicationDbContext = _context.R_analysis.Include(r => r.analysis).Include(r => r.interviews).Include(r => r.patient).Include(r => r.physician).Where(a => a.State1==false);
            return View(await applicationDbContext.ToListAsync());
        }
        [HttpGet]
        [Authorize(Roles = "دكتور")]
        [AllowAnonymous]
        public async Task<IActionResult> show(int id)
        {

            var interview = await _context.Interview.FindAsync(id);


            var applicationDbContext = _context.R_analysis.Where( r => r.interviewId==id);
            ViewBag.Analysis = applicationDbContext;
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", interview.PatientId);
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", interview.PhysicianId);
            ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName", interview.userId);
            return View("~/Views/interviews/Edit1.cshtml", interview);
        }
        [HttpGet]
        [Authorize(Roles = "دكتور")]
        [AllowAnonymous]
        public async Task<IActionResult> show1(int id, int id2)
        {

            var interview = await _context.Interview.FindAsync(id);


            var applicationDbContext = _context.R_analysis.Where(r => r.interviewId == id);
            ViewBag.Analysis = applicationDbContext;
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", interview.PatientId);
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", interview.PhysicianId);
            ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName", interview.userId);
             interview = await _context.Interview.FindAsync(id2);

            return View("~/Views/interviews/Edit1.cshtml", interview);
        }


        // GET: R_analysis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var r_analysis = await _context.R_analysis
                .Include(r => r.analysis)
                .Include(r => r.interviews)
                .Include(r => r.patient)
                .Include(r => r.physician)
                .SingleOrDefaultAsync(m => m.R_analysisId == id);
            if (r_analysis == null)
            {
                return NotFound();
            }

            return View(r_analysis);
        }

        public IActionResult Create(int id )
        {
           
                var interview =  _context.Interview.SingleOrDefault(a=> a.interviewId==id);

              ViewBag.Analysis = _context.Analysis;

            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", interview.PatientId);
                ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", interview.PhysicianId);
                ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName", interview.userId);
            
            return View(interview);
        }

        // POST: R_analysis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("R_analysisId,PatientId,AnalysisId,PhysicianId,interviewId,r_date,r_result,r_describe")] R_analysis r_analysis)
        {
            if (ModelState.IsValid)
            {
                if (id != null)
                {
                   var interview= _context.Interview.Find(id);
                    r_analysis.interviewId = interview.interviewId;
                    r_analysis.PatientId = interview.PatientId;
                    r_analysis.PhysicianId = interview.PhysicianId;
                    r_analysis.r_date  = DateTime.Now.ToString("hh:mm tt dd'-'MM'-'yyyy ");


                    _context.Add(r_analysis);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    return NotFound();
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllr_analysis", _context.R_analysis.Include(r => r.analysis).Include(r => r.interviews).Include(r => r.patient).Include(r => r.physician).ToList()) });

            }
            ViewData["AnalysisId"] = new SelectList(_context.Analysis, "AnalysisId", "a_name", r_analysis.AnalysisId);
            ViewData["interviewId"] = new SelectList(_context.Interview, "interviewId", "interviewId", r_analysis.interviewId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", r_analysis.PatientId);
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", r_analysis.PhysicianId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", r_analysis) });
        }

        // GET: R_analysis/Edit/5
        public async Task<IActionResult> Edit(int id = 0)
        {
            if (id == 0)

                return View(new R_analysis());
            else
            {
                var r_analysis = await _context.R_analysis.SingleOrDefaultAsync(m => m.R_analysisId == id);
                if (r_analysis == null)
                {
                    return NotFound();
                }
                ViewData["AnalysisId"] = new SelectList(_context.Analysis, "AnalysisId", "a_name", r_analysis.AnalysisId);
                ViewData["interviewId"] = new SelectList(_context.Interview, "interviewId", "interviewId", r_analysis.interviewId);
                ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", r_analysis.PatientId);
                ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", r_analysis.PhysicianId);
                return View(r_analysis);
            }
        }

        // POST: R_analysis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("R_analysisId,PatientId,AnalysisId,PhysicianId,interviewId,r_date,r_result,r_describe")] R_analysis r_analysis)
        {
            if (id != r_analysis.R_analysisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                //Insert
                if (id == 0)
                {
                   
                    _context.Add(r_analysis);
                    await _context.SaveChangesAsync();

                }
                //Update(patient);

                else
                {
                    try
                    {
                        r_analysis.State1 = true;
                        _context.Update(r_analysis);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!R_analysisExists(r_analysis.R_analysisId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllr_analysis", _context.R_analysis.Include(r => r.analysis).Include(r => r.interviews).Include(r => r.patient).Include(r => r.physician).Where(a => a.State1 == false).ToList()) });

            }
            ViewData["AnalysisId"] = new SelectList(_context.Analysis, "AnalysisId", "a_name", r_analysis.AnalysisId);
            ViewData["interviewId"] = new SelectList(_context.Interview, "interviewId", "interviewId", r_analysis.interviewId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", r_analysis.PatientId);
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", r_analysis.PhysicianId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", r_analysis) });
        }
        // GET: R_analysis/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var r_analysis = await _context.R_analysis
        //        .Include(r => r.analysis)
        //        .Include(r => r.interviews)
        //        .Include(r => r.patient)
        //        .Include(r => r.physician)
        //        .SingleOrDefaultAsync(m => m.R_analysisId == id);
        //    if (r_analysis == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(r_analysis);
        //}

        //// POST: R_analysis/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var r_analysis = await _context.R_analysis.SingleOrDefaultAsync(m => m.R_analysisId == id);
            _context.R_analysis.Remove(r_analysis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool R_analysisExists(int id)
        {
            return _context.R_analysis.Any(e => e.R_analysisId == id);
        }
    }
}
