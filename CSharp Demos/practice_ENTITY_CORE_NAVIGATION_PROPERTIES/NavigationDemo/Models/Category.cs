namespace NavigationDemo.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        
        // Collection Navigation: One Category has MANY Products
        public List<Product> Products { get; set; } = new List<Product>();
    }
}