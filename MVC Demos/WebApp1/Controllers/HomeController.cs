using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ViewDataExample()
        {
            ViewData["CompanyName"] = "Hexaware technologies";
            ViewData["TrainerName"] = "Geetha";
            ViewData["TotalEmployees"] = 1000;
            return View();
        }

        public IActionResult ViewBagExample()
        {
            ViewBag.CompanyName= "Hexaware technologies";
            ViewBag.TrainerName = "Geetha";
            ViewBag.TotalEmployees = 4500;
            return View();
        }
        
    }
}
