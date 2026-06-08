using System;
using System.Collections.Generic;

namespace HospitalAppoinmentConsoleApp.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public string PatientName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string City { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
