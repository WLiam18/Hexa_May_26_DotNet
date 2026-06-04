using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement
{
    public class EmployeeRepository
    {
        private List<Employee> _employees = new List<Employee>();
        
        public void Save(Employee emp)
        {
            _employees.Add(emp);
            Console.WriteLine($"Saved: {emp.Name}");
        }
        
        public Employee GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }
        
        public List<Employee> GetAll()
        {
            return _employees.ToList();
        }
    }
}