using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using NoSQLPOSExample.Infrastructure;
using NoSQLPOSExample.Models;

namespace examples.Controllers
{
    public class HomeController : Controller
    {
        private IPosBucketProvider _provider;

        public HomeController(IPosBucketProvider provider)
        {
            _provider = provider;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
