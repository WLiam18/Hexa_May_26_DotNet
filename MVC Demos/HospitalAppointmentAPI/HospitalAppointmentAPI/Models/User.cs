using System.Collections.Generic;

namespace HospitalAppointmentAPI.Models;

public partial class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string FullName { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
