using System;

namespace HospitalAppointmentAPI.DTOs
{
    public class DoctorResponseDto
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public decimal ConsultationFee { get; set; }
        public bool IsAvailable { get; set; }
    }
}
