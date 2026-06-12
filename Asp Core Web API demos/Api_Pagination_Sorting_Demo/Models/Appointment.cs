using System;
using System.Collections.Generic;

namespace Api_Pagination_Sorting_Demo.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public DateOnly AppointmentDate { get; set; }

    public TimeOnly AppointmentTime { get; set; }

    public string AppointmentStatus { get; set; } = null!;

    public string? Symptoms { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
