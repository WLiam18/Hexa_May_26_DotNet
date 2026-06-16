using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceMvcDbFirstDemo.ViewModels
{
    public class ProductCreateViewModel
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "ProductName is required")]
        public string ProductName { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage ="Price is required")]
        [Range(1,100000,ErrorMessage="Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage="stock quantity is required.")]
        [Range(0,1000,ErrorMessage ="Stock quqntity cannot be negative")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage ="Category id is Required")]
        public int CategoryId { get; set; }

        [NotMapped]
        public IFormFile? ProductImage { get; set; }

        public string? ExistingImagePath { get; set; }


    }
}
