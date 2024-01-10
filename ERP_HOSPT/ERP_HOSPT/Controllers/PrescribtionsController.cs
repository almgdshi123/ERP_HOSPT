using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP_HOSPT.Data;
using ERP_HOSPT.Models;

namespace ERP_HOSPT.Controllers
{
    public class PrescribtionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrescribtionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prescribtions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Prescribtion.Include(p => p.drug).Include(p => p.patient).Include(p => p.physician);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Prescribtions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescribtion = await _context.Prescribtion
                .Include(p => p.drug)
                .Include(p => p.patient)
                .Include(p => p.physician)
                .SingleOrDefaultAsync(m => m.PrescribtionId == id);
            if (prescribtion == null)
            {
                return NotFound();
            }

            return View(prescribtion);
        }

        // GET: Prescribtions/Create
        public IActionResult Create(int id )
        {
            ViewBag.interview = id;

            ViewData["DrugId"] = new SelectList(_context.Drug, "DrugId", "d_name");
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname");
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name");
            return View(new Diagnosis());
        }

        // POST: Prescribtions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,  Dure dure )
        {
            Diagnosis diagnosis=new Diagnosis();
            for (int i = 0; i < dure.Drug.Count();i++)
            {
                diagnosis.Drug += (i+1).ToString() + " - " + dure.Drug[i] + "  <br />" ;
                diagnosis.Drug_detail += (i+1).ToString() + " - " + dure.Drug_detail[i] + "  <br />";

            }
            var interview = _context.Interview.Find(id);
            diagnosis.interviewId = id;
            diagnosis.PatientId = interview.PatientId;
            diagnosis.PhysicianId = interview.PhysicianId;
            diagnosis.Diagnosis_date = "2015/8/9";
            diagnosis.Dig = dure.Dig;
            _context.Diagnosis.Add(diagnosis);
            _context.SaveChanges();



            var applicationDbContext = _context.R_analysis.Where(r => r.interviewId == id);
            ViewBag.Analysis = applicationDbContext;
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", interview.PatientId);
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", interview.PhysicianId);
            ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName", interview.userId);
            return View("~/Views/interviews/Edit1.cshtml", interview);
        }
        // GET: Prescribtions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0)

                return View(new Prescribtion());
            else
            {

                var prescribtion = await _context.Prescribtion.FindAsync( id);
                if (prescribtion == null)
                {
                    return NotFound();
                }
                ViewData["DrugId"] = new SelectList(_context.Drug, "DrugId", "d_name", prescribtion.DrugId);
                ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", prescribtion.PatientId);
                ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", prescribtion.PhysicianId);
                return View(prescribtion);
            }
        }

        // POST: Prescribtions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrescribtionId,PhysicianId,PatientId,Pre_date,Dig,DrugId,pre_detail")] Prescribtion prescribtion)
        {
            if (id != prescribtion.PrescribtionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                //Insert
                if (id == 0)
                {

                    _context.Add(prescribtion);
                    await _context.SaveChangesAsync();

                }
                //Update(patient);

                else
                {
                    try
                    {
                        _context.Update(prescribtion);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PrescribtionExists(prescribtion.PatientId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllprescribtion", _context.Prescribtion.Include(p => p.drug).Include(p => p.patient).Include(p => p.physician).ToList()) });

            }
            ViewData["DrugId"] = new SelectList(_context.Drug, "DrugId", "d_name", prescribtion.DrugId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", prescribtion.PatientId);
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_name", prescribtion.PhysicianId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", prescribtion) });

        }

        // GET: Prescribtions/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var prescribtion = await _context.Prescribtion
        //        .Include(p => p.drug)
        //        .Include(p => p.patient)
        //        .Include(p => p.physician)
        //        .SingleOrDefaultAsync(m => m.PrescribtionId == id);
        //    if (prescribtion == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(prescribtion);
        //}

        //// POST: Prescribtions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescribtion = await _context.Prescribtion.SingleOrDefaultAsync(m => m.PrescribtionId == id);
            _context.Prescribtion.Remove(prescribtion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrescribtionExists(int id)
        {
            return _context.Prescribtion.Any(e => e.PrescribtionId == id);
        }
    }
}
