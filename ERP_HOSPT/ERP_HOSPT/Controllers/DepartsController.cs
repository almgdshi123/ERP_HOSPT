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
    public class DepartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Departs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Depart.ToListAsync());
        }

        // GET: Departs/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var depart = await _context.Depart
        //        .SingleOrDefaultAsync(m => m.Departno == id);
        //    if (depart == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(depart);
        //}

        // GET: Departs/Create
        public IActionResult Create()
        {
            return View(new Depart());
        }


        // POST: Departs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Departno,dept_name")] Depart depart)
        {
            if (ModelState.IsValid)
            {
         
                if (id == 0)
                {

                    _context.Add(depart);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    return NotFound();
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this,  "_ViewAll", _context.Depart.ToList()) });


            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", depart) });


        }

        // GET: Departs/Edit/5
        public async Task<IActionResult> Edit(int id = 0)
        {
            if (id == 0)
               
            return View(new Depart());
            else
            {
                var depart = await _context.Depart.FindAsync(id);
                if (depart == null)
                {
                    return NotFound();
                }
                return View(depart);
            }
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Departno,dept_name")] Depart depart)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id ==0)
                {
                   
                    _context.Add(depart);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(depart);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DepartExists(depart.Departno))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Depart.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", depart) });
        }

        // POST: Departs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Departno,dept_name")] Depart depart)
        //{
        //    if (id != depart.Departno)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(depart);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!DepartExists(depart.Departno))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(depart);
        //}

        // GET: Departs/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var depart = await _context.Depart
        //        .SingleOrDefaultAsync(m => m.Departno == id);
        //    if (depart == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(depart);
        //}

        //// POST: Departs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depart = await _context.Depart.SingleOrDefaultAsync(m => m.Departno == id);
            _context.Depart.Remove(depart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartExists(int id)
        {
            return _context.Depart.Any(e => e.Departno == id);
        }
    }
}
