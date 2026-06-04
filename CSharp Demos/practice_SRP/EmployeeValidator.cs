using System;

namespace EmployeeManagement
{
    public class EmployeeValidator
    {
        public bool IsValid(Employee emp)
        {
            if (emp == null)
                return false;
                
            if (string.IsNullOrWhiteSpace(emp.Name))
            {
                Console.WriteLine("Error: Name required");
                return false;
            }
            
            if (emp.Salary <= 0)
            {
                Console.WriteLine("Error: Salary must be positive");
                return false;
            }
            
            return true;
        }
    }
}