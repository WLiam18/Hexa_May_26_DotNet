using System.ComponentModel.DataAnnotations;

namespace Hospital.AppointmentService.DTOs
{
    public class AppointmentCreateDto
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public DateOnly AppointmentDate { get; set; }

        [Required]
        public TimeOnly AppointmentTime { get; set; }

        public string? Reason { get; set; }
    }
}
