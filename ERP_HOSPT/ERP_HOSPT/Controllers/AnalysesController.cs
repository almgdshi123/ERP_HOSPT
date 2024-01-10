using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP_HOSPT.Data;
using Microsoft.AspNetCore.Authorization;

namespace ERP_HOSPT.Controllers
{
    public class AnalysesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnalysesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        // GET: Analyses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Analysis.ToListAsync());
        }

        //GET: Analyses/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var analysis = await _context.Analysis
        //        .SingleOrDefaultAsync(m => m.AnalysisId == id);
        //    if (analysis == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(analysis);
        //}

        // GET: Analyses/Create
        public IActionResult Create()
        {
            return View(new Analysis());
        }

        // POST: Analyses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("AnalysisId,a_name,a_Pric")] Analysis analysis)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {

                    _context.Add(analysis);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    return NotFound();
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllanalysis", _context.Analysis.ToList()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", analysis) });
        }

        // GET: Analyses/Edit/5
        public async Task<IActionResult> Edit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Analysis());
            }

            var analysis = await _context.Analysis.FindAsync(id);
            if (analysis == null)
            {
                return NotFound();
            }
            return View(analysis);

            
        }

        // POST: Analyses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnalysisId,a_name,a_Pric")] Analysis analysis)
        {
            if (id != analysis.AnalysisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
              
                


                //Insert
                if (id == 0)
                {

                    _context.Add(analysis);
                    await _context.SaveChangesAsync();

                }
                //Update(patient);

                else
                {
                    try
                    {
                        _context.Update(analysis);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AnalysisExists(analysis.AnalysisId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllanalysis", _context.Analysis.ToList()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", analysis) });
        }

        // GET: Analyses/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var analysis = await _context.Analysis
        //        .SingleOrDefaultAsync(m => m.AnalysisId == id);
        //    if (analysis == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(analysis);
        //}

        //// POST: Analyses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var analysis = await _context.Analysis.SingleOrDefaultAsync(m => m.AnalysisId == id);
            _context.Analysis.Remove(analysis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnalysisExists(int id)
        {
            return _context.Analysis.Any(e => e.AnalysisId == id);
        }
    }
}
