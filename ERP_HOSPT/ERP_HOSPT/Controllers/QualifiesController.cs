using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP_HOSPT.Data;

namespace ERP_HOSPT.Controllers
{
    public class QualifiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QualifiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Qualifies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Qualify.ToListAsync());
        }

        // GET: Qualifies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualify = await _context.Qualify
                .SingleOrDefaultAsync(m => m.QualifyId == id);
            if (qualify == null)
            {
                return NotFound();
            }

            return View(qualify);
        }

        public IActionResult Create()
        {
            return View(new Qualify());
        }

        // POST: Qualifies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("QualifyId,q_name")] Qualify qualify)
        {
            if (ModelState.IsValid)
            {
               
                if (id == 0)
                {

                    _context.Add(qualify);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    return NotFound();
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllqualify", _context.Qualify.ToList()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", qualify) });

        }

        // GET: Qualifies/Edit/5
        public async Task<IActionResult> Edit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Qualify());
            }

            var qualify = await _context.Qualify.FindAsync(id);
            if (qualify == null)
            {
                return NotFound();
            }
            return View(qualify);
        }

        // POST: Qualifies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QualifyId,q_name")] Qualify qualify)
        {
            if (id != qualify.QualifyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                //Insert
                if (id == 0)
                {

                    _context.Add(qualify);
                    await _context.SaveChangesAsync();

                }
                //Update(patient);

                else
                {
                    try
                    {
                        _context.Update(qualify);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!QualifyExists(qualify.QualifyId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllqualify", _context.Qualify.ToList()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", qualify) });

        }

        //// GET: Qualifies/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var qualify = await _context.Qualify
        //        .SingleOrDefaultAsync(m => m.QualifyId == id);
        //    if (qualify == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(qualify);
        //}

        //// POST: Qualifies/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qualify = await _context.Qualify.SingleOrDefaultAsync(m => m.QualifyId == id);
            _context.Qualify.Remove(qualify);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QualifyExists(int id)
        {
            return _context.Qualify.Any(e => e.QualifyId == id);
        }
    }
}
