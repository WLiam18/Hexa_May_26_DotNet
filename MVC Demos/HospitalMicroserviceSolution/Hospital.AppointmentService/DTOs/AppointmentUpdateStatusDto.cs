using System.ComponentModel.DataAnnotations;

namespace Hospital.AppointmentService.DTOs
{
    public class AppointmentUpdateStatusDto
    {
        [Required]
        [RegularExpression("^(Scheduled|Completed|Cancelled)$",
           ErrorMessage = "Status must be Scheduled, Completed, or Cancelled.")]
        public string AppointmentStatus { get; set; } = string.Empty;
    }
}
