using Microsoft.AspNetCore.Mvc;

namespace CodeFirstLibraryMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult About()
    {
        return View();
    }
}