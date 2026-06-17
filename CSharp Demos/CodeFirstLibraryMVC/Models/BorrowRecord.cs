using System.ComponentModel.DataAnnotations;

namespace CodeFirstLibraryMVC.Models;

public class BorrowRecord
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int BookId { get; set; }
    
    [Required]
    public int MemberId { get; set; }
    
    [Required]
    public DateTime BorrowDate { get; set; } = DateTime.Now;
    
    [Required]
    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(14);
    
    public DateTime? ReturnDate { get; set; }
    
    [MaxLength(20)]
    public string Status { get; set; } = "Borrowed"; // Borrowed, Returned, Overdue
    
    // Navigation properties
    public Book? Book { get; set; }
    public Member? Member { get; set; }
}