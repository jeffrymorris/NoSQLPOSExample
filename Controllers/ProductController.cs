using System;
using System.Collections.Generic;
using Couchbase.Core;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NoSQLPOSExample.Infrastructure;
using NoSQLPOSExample.Models;
using Couchbase.Extensions.Caching;
using Couchbase.N1QL;

namespace NoSQLPOSExample.Controllers
{
    public class ProductController : Controller
    {
        private IRepository _repository;
        private IDistributedCache _distributedCache;

        public ProductController(IRepository repository, IDistributedCache distributedCache)
        {
            _repository = repository;
            _distributedCache = distributedCache;
        }

        // GET: Product
        public ActionResult Index()
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            _distributedCache.Set("theCacheKey", bytes, new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(10)
            });

            const string statement = "SELECT META().id, p.description, p.name, p.price FROM `pos` as p WHERE type='product'";
            return View(_repository.QueryAsync<Product>(statement).GetAwaiter().GetResult());
        }

        // GET: Product/Details/5
        public ActionResult Details(string id)
        {
            var product = _repository.GetAsync<Product>(id).GetAwaiter().GetResult();
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            var bytes = _distributedCache.Get("theCacheKey");
            if (bytes != null)
            {
                ViewData["Message"] = System.Text.Encoding.UTF8.GetString(bytes);
            }

            return View();
        }

        // POST: Product/Create
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

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
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

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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