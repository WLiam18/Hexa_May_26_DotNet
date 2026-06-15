using Microsoft.AspNetCore.Mvc;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee
                {
                    EmployeeId = 1,
                    EmlpoyeeName = "Geetha",
                    Department="IT",
                    Salary=18000,
                    City="Coimbatore"
                },
                new Employee
                {
                    EmployeeId = 2,
                    EmlpoyeeName = "Fransy",
                    Department="Helath care",
                    Salary=8000,
                    City="Pune"
                },
                new Employee
                {
                    EmployeeId = 2,
                    EmlpoyeeName = "Parsuna",
                    Department="WH",
                    Salary=10000,
                    City="Hyderabad"
                },
            };

            return View(employees);
        }

        public IActionResult Details()
        {
            return View();
        }
        public ContentResult Message()
        {
            return Content("welcome ti Employee MVC Application");
        }
        public JsonResult GetEmployeeJson()
        {
            var employee = new Employee
            {
                EmployeeId = 1,
                EmlpoyeeName = "Geetha",
                Department = "IT",
                Salary = 18000,
                City = "Coimbatore"
            };
            return Json(employee);
        }

        public RedirectResult GotoGoogle()
        {
            return Redirect("https://www.google.com");
        }

        public IActionResult FindEmployee(int id)
        {
            if(id<=0)
            {
                return NotFound("Employee not Found");
            }
            return Content("Employee Found with the Id : " + id);
        }
    }
}
