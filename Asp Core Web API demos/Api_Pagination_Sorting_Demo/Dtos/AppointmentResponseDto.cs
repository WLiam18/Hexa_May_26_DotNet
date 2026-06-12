namespace Api_Pagination_Sorting_Demo.Dtos
{
    public class AppointmentResponseDto
    {
        public int AppointmentId { get; set; }

        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;

        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string PatientCity { get; set; } = string.Empty;
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string AppointmentStatus { get; set; } =string.Empty;
        public string? Symptoms { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
