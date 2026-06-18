namespace Hospital.AppointmentService.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public DateOnly AppointmentDate { get; set; }

        public TimeOnly AppointmentTime { get; set; }

        public string AppointmentStatus { get; set; } = "Scheduled";

        public string? Reason { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
