using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDB.Controllers
{
    public class healthrecordController : Controller
    {
        // GET: healthrecord
        public IActionResult Index()
        {
            return View();
        }

        // GET: healthrecord/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: healthrecord/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: healthrecord/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: healthrecord/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: healthrecord/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: healthrecord/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: healthrecord/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}