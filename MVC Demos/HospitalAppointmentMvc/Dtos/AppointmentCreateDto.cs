using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentMvc.Dtos
{
    public class AppointmentCreateDto
    {
        [Required(ErrorMessage ="Doctor id is required")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage ="Patient Id is required")]
        public int PatientId { get; set; }

        [Required(ErrorMessage ="Appointment Date is required.")]
        public DateOnly AppointmentDate { get; set; }

        [Required(ErrorMessage = "Appointment Time is required.")]
        public TimeOnly AppointmentTime { get; set; }

        public string? Reason { get; set; }
    }
}
