using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERPDB.Models;
using Microsoft.AspNetCore.Authorization;

namespace ERPDB.Controllers
{
    public class PatientsController : Controller
    {
        ERPB_DBContext _context = new ERPB_DBContext();

        public PatientsController()
        {
        }

        // GET: Patients
        public async Task<IActionResult> Index(Patient patient)
        {
            int id = 0;

            if (patient.PatientId == 2)
            {
                id = int.Parse(patient.Patientname);
            }
            var applicationDbContext = _context.Patient.Include(p => p.Region).Include(p => p.City).Where(a => a.Patientname == patient.Patientname || a.PaMobile == patient.Patientname || a.PaNat == patient.Patientname || a.PatientId == id);

            return View(await applicationDbContext.ToListAsync());

        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.City)
                .Include(p => p.Region)
                .SingleOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.City, "CityId", "CitName");
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "RegName");
            return View();
        }
        [AllowAnonymous]
        [HttpGet]

        public IActionResult Compeny()
        {

            var compny = _context.Company;
            return View(compny);
        }
        [AllowAnonymous]
        [HttpPost]
        public  IActionResult Compeny ([Bind("CompanName")]Company company)
        {
            var newid = new Random();
            var uid = newid.Next(100000000, 1000000000);
            company.CompanyId = uid.ToString();
            
            _context.Company.Add(company);
            _context.SaveChanges();

            return RedirectToAction(nameof(Compeny));
        }
        [AllowAnonymous]

        [HttpGet]
        public IActionResult Compenyid(string id)
        
        {
            
            var compny = _context.Company.FirstOrDefault(c => c.CompanyId==id);
            if (compny.State == null)
            {
                compny.State = 0;

            }
            else if (compny.State == 0)
            {
                compny.State = 1;

            }
            else if (compny.State == 1)
            {
                compny.State = 0;

            }
            _context.Update(compny);
            _context.SaveChanges();


            return RedirectToAction(nameof(Compeny));
        }

        [AllowAnonymous]

        [HttpGet]
        public IActionResult Compenyditils(string id)
        {

            var compny = _context.Company.Find(id);
            return View(compny);
        }


        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,Patientname,RegionId,CityId,PaAddr,PaData,PaEmail,PaJob,PaMobile,PaNat,PaNote,PaPhone,PaSex")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.City, "CityId", "CitName", patient.CityId);
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "RegName", patient.RegionId);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.SingleOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.City, "CityId", "CitName", patient.CityId);
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "RegName", patient.RegionId);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,Patientname,RegionId,CityId,PaAddr,PaData,PaEmail,PaJob,PaMobile,PaNat,PaNote,PaPhone,PaSex")] Patient patient)
        {
            if (id != patient.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientId))
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
            ViewData["CityId"] = new SelectList(_context.City, "CityId", "CitName", patient.CityId);
            ViewData["RegionId"] = new SelectList(_context.Region, "RegionId", "RegName", patient.RegionId);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.City)
                .Include(p => p.Region)
                .SingleOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patient.SingleOrDefaultAsync(m => m.PatientId == id);
            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.PatientId == id);
        }
    }
}
