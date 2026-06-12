namespace Api_Pagination_Sorting_Demo.Dtos
{
    public class AppointmentFilterRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? DoctorId { get; set; }
        public int?  PatientId { get; set; }
        public string? AppointmentStatus { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string? SortBy { get; set; } = "appointmentDate";
        public string? SortDirection { get; set; } = "asc";
    }
}
