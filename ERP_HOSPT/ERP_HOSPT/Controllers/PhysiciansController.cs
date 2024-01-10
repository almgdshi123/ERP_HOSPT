using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP_HOSPT.Data;
using Microsoft.Extensions.Configuration;

namespace ERP_HOSPT.Controllers
{
    public class PhysiciansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhysiciansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Physicians
        public async Task<IActionResult> Index()
        {

            IConfigurationBuilder builder = new ConfigurationBuilder();
            IConfigurationRoot configuration = builder.AddJsonFile("key.json", false, reloadOnChange: true).Build();
            string CompanyId = configuration.GetSection("key").Value;
            var applicationDbContext = _context.Physician.Include(p => p.Depart).Include(p => p.Qualify).Include(p => p.Region).Include(p => p.city).Where(x=> x.CompanyId==CompanyId );
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Physicians/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var physician = await _context.Physician
        //        .Include(p => p.Depart)
        //        .Include(p => p.Qualify)
        //        .Include(p => p.Region)
        //        .Include(p => p.city)
        //        .SingleOrDefaultAsync(m => m.PhysicianId == id);
        //    if (physician == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(physician);
        //}

        // GET: Physicians/Create
        public IActionResult Create(int id = 0)
        {
            if (id == 0)
                ViewData["Departno"] = new SelectList(_context.Depart, "Departno", "dept_name");
            ViewData["QualifyId"] = new SelectList(_context.Qualify, "QualifyId", "q_name");
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "reg_name");
            ViewData["cityId"] = new SelectList(_context.City, "cityId", "cit_name");
            return View(new Physician());
        }
     
        // POST: Physicians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("PhysicianId,phy_name,phy_birth,phy_sex,phy_addr,phy_phone,phy_emil,Departno,QualifyId,cityId,RegionId")] Physician physician)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {

                    IConfigurationBuilder builder = new ConfigurationBuilder();
                    IConfigurationRoot configuration = builder.AddJsonFile("key.json", false, reloadOnChange: true).Build();
                    physician.CompanyId = configuration.GetSection("key").Value;
                    _context.Add(physician);
                    await _context.SaveChangesAsync();
                   
                }
                else
                {
                    return NotFound();
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllPhysicaion", _context.Physician.Include(p => p.Depart).Include(p => p.Qualify).Include(p => p.Region).Include(p => p.city).ToList()) });
            }

           
            
            ViewData["Departno"] = new SelectList(_context.Depart, "Departno", "dept_name", physician.Departno);
            ViewData["QualifyId"] = new SelectList(_context.Qualify, "QualifyId", "q_name", physician.QualifyId);
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "reg_name", physician.RegionId);
            ViewData["cityId"] = new SelectList(_context.City, "cityId", "cit_name", physician.cityId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", physician) });
        }

        // GET: Physicians/Edit/5
        public async Task<IActionResult> Edit(int id = 0)
        {
            if (id == 0)

                return View(new Physician());
            else
            {
                var physician = await _context.Physician.FindAsync(id);
                if (physician == null)
                {
                    return NotFound();
                }
                ViewData["userId"] = new SelectList(_context.Users, "Id", "UserName");
                ViewData["Departno"] = new SelectList(_context.Depart, "Departno", "dept_name", physician.Departno);
                ViewData["QualifyId"] = new SelectList(_context.Qualify, "QualifyId", "q_name", physician.QualifyId);
                ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "reg_name", physician.RegionId);
                ViewData["cityId"] = new SelectList(_context.City, "cityId", "cit_name", physician.cityId);
                return View(physician);
            }
        }

        // POST: Physicians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhysicianId,phy_name,phy_birth,phy_sex,phy_addr,phy_phone,phy_emil,Departno,QualifyId,cityId,RegionId,userId")] Physician physician)
        {

            if (ModelState.IsValid)
            {

                //Insert
                if (id == 0)
                {

                    _context.Add(physician);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    try
                 {
                    _context.Update(physician);
                    await _context.SaveChangesAsync();
                 }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhysicianExists(physician.PhysicianId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
             }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllPhysicaion", _context.Physician.Include(p => p.Depart).Include(p => p.Qualify).Include(p => p.Region).Include(p => p.city).ToList()) });

            }
            
            ViewData["Departno"] = new SelectList(_context.Depart, "Departno", "dept_name", physician.Departno);
            ViewData["QualifyId"] = new SelectList(_context.Qualify, "QualifyId", "q_name", physician.QualifyId);
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "reg_name", physician.RegionId);
            ViewData["cityId"] = new SelectList(_context.City, "cityId", "cit_name", physician.cityId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Edit", physician) });
          
        }

        // GET: Physicians/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var physician = await _context.Physician
        //        .Include(p => p.Depart)
        //        .Include(p => p.Qualify)
        //        .Include(p => p.Region)
        //        .Include(p => p.city)
        //        .SingleOrDefaultAsync(m => m.PhysicianId == id);
        //    if (physician == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(physician);
        //}

        //// POST: Physicians/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var physician = await _context.Physician.SingleOrDefaultAsync(m => m.PhysicianId == id);
            _context.Physician.Remove(physician);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhysicianExists(int id)
        {
            return _context.Physician.Any(e => e.PhysicianId == id);
        }
    }
}
