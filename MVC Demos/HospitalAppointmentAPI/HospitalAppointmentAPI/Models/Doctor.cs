using System;
using System.Collections.Generic;

namespace HospitalAppointmentAPI.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string FullName { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public decimal ConsultationFee { get; set; }

    public bool IsAvailable { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
