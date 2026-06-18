using System.ComponentModel.DataAnnotations;

namespace Hospital.DoctorService.DTOs
{
    public class DoctorCreateDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Specialization { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public decimal ConsultationFee { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
