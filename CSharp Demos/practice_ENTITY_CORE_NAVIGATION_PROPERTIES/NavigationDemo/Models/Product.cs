namespace NavigationDemo.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        
        public int CategoryId { get; set; }           // Foreign Key
        public Category Category { get; set; }        // Reference Navigation
    }
}