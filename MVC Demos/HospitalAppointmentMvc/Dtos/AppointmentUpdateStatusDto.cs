using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentMvc.Dtos
{
    public class AppointmentUpdateStatusDto
    {
        [Required(ErrorMessage="Appointment status is required.")]
        [RegularExpression("^(Scheduled|Completed|Cancelled)$",ErrorMessage = "Status must be Scheduled, Completed, or Cancelled.")]
        public string AppointmentStatus { get; set; } = string.Empty;
    }
}
