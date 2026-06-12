using System;
using System.Collections.Generic;

namespace Api_Pagination_Sorting_Demo.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string DoctorName { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public decimal ConsultationFee { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public bool IsAvailable { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
