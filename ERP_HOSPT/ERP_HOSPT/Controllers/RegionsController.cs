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
    public class RegionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Regions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Region.Include(r => r.city);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Regions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Region
                .Include(r => r.city)
                .SingleOrDefaultAsync(m => m.RegionId == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: Regions/Create
        public IActionResult Create()
        {
            ViewData["cityId"] = new SelectList(_context.City, "cityId", "cit_name");
            return View(new Region());
        }

        // POST: Regions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("RegionId,reg_name,cityId")] Region region)
        {
            if (ModelState.IsValid)
            {
                
                if (id == 0)
                {

                    _context.Add(region);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    return NotFound();
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllRegions", _context.Region.Include(r => r.city).ToList()) });


            }
            ViewData["cityId"] = new SelectList(_context.City, "cityId", "cit_name", region.cityId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", region) });

        }

        // GET: Regions/Edit/5
        public async Task<IActionResult> Edit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Region());
            }
            var region = await _context.Region.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            ViewData["cityId"] = new SelectList(_context.City, "cityId", "cit_name", region.cityId);
            return View(region);
        }

        // POST: Regions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegionId,reg_name,cityId")] Region region)
        {
            if (id != region.RegionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {

                    _context.Add(region);
                    await _context.SaveChangesAsync();

                }
                //Update(patient);

                else
                {
                    try
                    {
                        _context.Update(region);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!RegionExists(region.RegionId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllRegions", _context.Region.Include(r => r.city).ToList()) });

            }
            ViewData["cityId"] = new SelectList(_context.City, "cityId", "cit_name", region.cityId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", region) });
        }

        //// GET: Regions/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var region = await _context.Region
        //        .Include(r => r.city)
        //        .SingleOrDefaultAsync(m => m.RegionId == id);
        //    if (region == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(region);
        //}

        //// POST: Regions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          
            var region = await _context.Region.SingleOrDefaultAsync(m => m.RegionId == id);
            _context.Region.Remove(region);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           
        }

        private bool RegionExists(int id)
        {
            return _context.Region.Any(e => e.RegionId == id);
        }
    }
}
