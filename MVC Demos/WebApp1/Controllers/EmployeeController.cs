using Microsoft.AspNetCore.Mvc;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class EmployeeController : Controller
    {
        public static List<Employee> employees = new List<Employee>()
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
                    EmployeeId = 3,
                    EmlpoyeeName = "Parsuna",
                    Department="WH",
                    Salary=10000,
                    City="Hyderabad"
                },
            };

        public IActionResult Index()
        {

            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Employee employee = new Employee();
            employee.EmployeeId = employees.Count + 1;
            employee.EmlpoyeeName = model.EmployeeName;
            employee.Salary = model.Salary;
            employee.Department = model.Department;
            employee.City = model.City;

            employees.Add(employee);
            // TempData["SuccessMessage"] = "Employee Created Sucessfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Employee? emp = employees.FirstOrDefault(e => e.EmployeeId == id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee? emp = employees.FirstOrDefault(e => e.EmployeeId == id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee updateEmployee)
        {
            Employee? emp = employees.FirstOrDefault(e => e.EmployeeId == updateEmployee.EmployeeId);
            if (emp == null)
            {
                return NotFound();
            }

            emp.EmlpoyeeName = updateEmployee.EmlpoyeeName;
            emp.Department = updateEmployee.Department;
            emp.Salary = updateEmployee.Salary;
            emp.City = updateEmployee.City;

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Employee? emp = employees.FirstOrDefault(e => e.EmployeeId==id);
            if(emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            Employee? emp = employees.FirstOrDefault(e => e.EmployeeId!=id);
            if(emp==null)
                return NotFound();
            employees.Remove(emp);
            return RedirectToAction("Index");
        }
        //public ContentResult Message()
        //{
        //    return Content("welcome ti Employee MVC Application");
        //}
        //public JsonResult GetEmployeeJson()
        //{
        //    var employee = new Employee
        //    {
        //        EmployeeId = 1,
        //        EmlpoyeeName = "Geetha",
        //        Department = "IT",
        //        Salary = 18000,
        //        City = "Coimbatore"
        //    };
        //    return Json(employee);
        //}

        //public RedirectResult GotoGoogle()
        //{
        //    return Redirect("https://www.google.com");
        //}

        //public IActionResult FindEmployee(int id)
        //{
        //    if(id<=0)
        //    {
        //        return NotFound("Employee not Found");
        //    }
        //    return Content("Employee Found with the Id : " + id);
        //}
        //public IActionResult CreateEmployee()
        //{
        //    TempData["SuccessMessage"] = "Employee Created Successfully";
        //    return RedirectToAction("EmployeeSuccess");
        //}

        //public IActionResult EmployeeSuccess()
        //{
        //    return View();
        //}
    }
}
