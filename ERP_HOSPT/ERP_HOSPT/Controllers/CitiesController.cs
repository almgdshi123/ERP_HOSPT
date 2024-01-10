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
    public class CitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            return View(await _context.City.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.City
                .SingleOrDefaultAsync(m => m.cityId == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            return View(new City());
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("cityId,cit_name")] City city)
        {
            if (ModelState.IsValid)
            {
               
                if (id == 0)
                {

                    _context.Add(city);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    return NotFound();
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllcity", _context.City.ToList()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", city) });

       
        }

        // GET: Cities/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id = 0)
        {
            var city = await _context.City.SingleOrDefaultAsync(m => m.cityId == id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("cityId,cit_name")] City city)
        {
            if (id != city.cityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.cityId))
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
            return View(city);
        }

        //// GET: Cities/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var city = await _context.City
        //        .SingleOrDefaultAsync(m => m.cityId == id);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(city);
        //}

        //// POST: Cities/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var city = await _context.City.SingleOrDefaultAsync(m => m.cityId == id);
            _context.City.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.cityId == id);
        }
    }
}
