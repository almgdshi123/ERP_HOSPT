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
    public class DrugsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DrugsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Drugs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Drug.ToListAsync());
        }

        // GET: Drugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drug = await _context.Drug
                .SingleOrDefaultAsync(m => m.DrugId == id);
            if (drug == null)
            {
                return NotFound();
            }

            return View(drug);
        }

        // GET: Drugs/Create
        public IActionResult Create()
        {
            return View(new Drug());
        }

        // POST: Drugs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("DrugId,d_name,d_pric")] Drug drug)
        {
            if (ModelState.IsValid)
            {
                

                if (id == 0)
                {

                    _context.Add(drug);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    return NotFound();
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllDrugs", _context.Drug.ToList()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", drug) });

        }

        // GET: Drugs/Edit/5
        public async Task<IActionResult> Edit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Drug());
            }

            var drug = await _context.Drug.FindAsync(id);
            if (drug == null)
            {
                return NotFound();
            }
            return View(drug);
        }

        // POST: Drugs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DrugId,d_name,d_pric")] Drug drug)
        {
          

            if (ModelState.IsValid)
            {
               



                //Insert
                if (id == 0)
                {

                    _context.Add(drug);
                    await _context.SaveChangesAsync();

                }
                //Update(patient);

                else
                {
                    try
                    {
                        _context.Update(drug);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DrugExists(drug.DrugId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllDrugs", _context.Drug.ToList()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", drug) });
        }

        //GET: Drugs/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var drug = await _context.Drug
        //        .SingleOrDefaultAsync(m => m.DrugId == id);
        //    if (drug == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(drug);
        //}

        //POST: Drugs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drug = await _context.Drug.SingleOrDefaultAsync(m => m.DrugId == id);
            _context.Drug.Remove(drug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrugExists(int id)
        {
            return _context.Drug.Any(e => e.DrugId == id);
        }
    }
}
