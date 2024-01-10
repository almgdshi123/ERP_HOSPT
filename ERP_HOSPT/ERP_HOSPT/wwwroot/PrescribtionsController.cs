using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP_HOSPT.Data;

namespace ERP_HOSPT.wwwroot
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
        public IActionResult Create()
        {
            ViewData["DrugId"] = new SelectList(_context.Drug, "DrugId", "DrugId");
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname");
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_addr");
            return View();
        }

        // POST: Prescribtions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrescribtionId,PhysicianId,PatientId,Pre_date,Dig,DrugId,pre_detail")] Prescribtion prescribtion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prescribtion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrugId"] = new SelectList(_context.Drug, "DrugId", "DrugId", prescribtion.DrugId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", prescribtion.PatientId);
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_addr", prescribtion.PhysicianId);
            return View(prescribtion);
        }

        // GET: Prescribtions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescribtion = await _context.Prescribtion.SingleOrDefaultAsync(m => m.PrescribtionId == id);
            if (prescribtion == null)
            {
                return NotFound();
            }
            ViewData["DrugId"] = new SelectList(_context.Drug, "DrugId", "DrugId", prescribtion.DrugId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", prescribtion.PatientId);
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_addr", prescribtion.PhysicianId);
            return View(prescribtion);
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
                try
                {
                    _context.Update(prescribtion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescribtionExists(prescribtion.PrescribtionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrugId"] = new SelectList(_context.Drug, "DrugId", "DrugId", prescribtion.DrugId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Patientname", prescribtion.PatientId);
            ViewData["PhysicianId"] = new SelectList(_context.Physician, "PhysicianId", "phy_addr", prescribtion.PhysicianId);
            return View(prescribtion);
        }

        // GET: Prescribtions/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Prescribtions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
