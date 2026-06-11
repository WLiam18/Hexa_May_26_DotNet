namespace InventoryManagementAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int StockQunatity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
