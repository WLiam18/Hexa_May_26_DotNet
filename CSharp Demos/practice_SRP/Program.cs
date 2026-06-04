using System;

namespace EmployeeManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var validator = new EmployeeValidator();
            var repository = new EmployeeRepository();
            
            // Create employee
            var emp1 = new Employee(101, "William", "IT", 50000);
            
            if (validator.IsValid(emp1))
            {
                repository.Save(emp1);
            }
            
            var emp2 = new Employee(102, "Joel", "HR", 60000);
            
            if (validator.IsValid(emp2))
            {
                repository.Save(emp2);
            }
            
            Console.WriteLine("\nAll Employees:");
            foreach (var emp in repository.GetAll())
            {
                emp.Display();
            }
            
            Console.ReadLine();
        }
    }
}