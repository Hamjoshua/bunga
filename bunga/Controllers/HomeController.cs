using bunga.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using bunga;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bunga.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string holder)
        {
            var lol = ModelState;
            string address = HttpContext.Request.Form["address"];
            string members = HttpContext.Request.Form["members"];
            string wifi = HttpContext.Request.Form["wifi"];
            string min_price = HttpContext.Request.Form["min_price"];
            string max_price = HttpContext.Request.Form["max_price"];
            return RedirectToAction("Index", "Bungaloes", new { address = address, members=members, wifi=wifi,
                                                                min_price=min_price, max_price=max_price});
        }

        [Authorize(Roles = RoleNames.Buyer)]
        public IActionResult Privacy()
        {            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
