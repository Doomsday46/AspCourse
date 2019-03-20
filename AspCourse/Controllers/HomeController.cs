using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspCourse.Models;
using Microsoft.AspNetCore.Authorization;

namespace AspCourse.Controllers
{
    public class HomeController : Controller
    {

        [Authorize]
        public IActionResult Index()
        {
            string name = HttpContext.User.Identity.Name;
            ViewData["Account"] = $"{name}!";

            return View();
        }
        [Authorize]
        public IActionResult About()
        {
            string name = HttpContext.User.Identity.Name;
            ViewData["Account"] = $"{name}!";

            return View();
        }
        [Authorize]
        public IActionResult Contact()
        {
            string name = HttpContext.User.Identity.Name;
            ViewData["Account"] = $"{name}!";

            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            string name = HttpContext.User.Identity.Name;
            ViewData["Account"] = $"{name}!";
            return View();
        }
        [Authorize]
        public IActionResult Tournament()
        {
            string name = HttpContext.User.Identity.Name;
            ViewData["Account"] = $"{name}!";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            string name = HttpContext.User.Identity.Name;
            ViewData["Account"] = $"{name}!";
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
