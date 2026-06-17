using System.ComponentModel.DataAnnotations;

namespace CodeFirstLibraryMVC.Models;

public class Member
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;
    
    [Phone]
    [MaxLength(15)]
    public string? Phone { get; set; }
    
    public DateTime MembershipDate { get; set; } = DateTime.Now;
    
    public bool IsActive { get; set; } = true;
    
    // Navigation property
    public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
}