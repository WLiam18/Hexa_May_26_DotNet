using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class EmployeeCreateViewModel
    {
        [Required]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Range(10000,200000)]
        public decimal Salary { get; set; }

        [Required]
        public string  City { get; set; }

    }
}
