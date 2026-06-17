using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstLibraryMVC.Models;

public class Book
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Author { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string? Genre { get; set; }
    
    [Required]
    public int TotalCopies { get; set; } = 1;
    
    public int AvailableCopies { get; set; } = 1;
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    // Navigation property
    public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
}