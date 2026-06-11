namespace InventoryManagementAPI.Dtos
{
    public class ProductCreateDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int StockQunatity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
