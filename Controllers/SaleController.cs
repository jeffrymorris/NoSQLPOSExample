using System.Collections.Generic;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.Extensions.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoSQLPOSExample.Infrastructure;
using NoSQLPOSExample.Models;

namespace NoSQLPOSExample.Controllers
{
    public class SaleController : Controller
    {
        private IRepository _repository;

        public SaleController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Sale
        public ActionResult Index()
        {
            HttpContext.Session.SetObject("theKey", "Session Data!");

            return View(new List<Sale>());
        }

        // GET: Sale/Details/5
        public ActionResult Details(string id)
        {
            var sale = _repository.GetAsync<Sale>(id).GetAwaiter().GetResult();
            return View(sale);
        }

        // GET: Sale/Create
        public ActionResult Create()
        {
            ViewData["Message"] = HttpContext.Session.GetObject<string>("theKey");

            return View();
        }

        // POST: Sale/Create
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

        // GET: Sale/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sale/Edit/5
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

        // GET: Sale/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sale/Delete/5
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