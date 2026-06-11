namespace InventoryManagementAPI.Dtos
{
    public class ProductResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int StockQunatity { get; set; }
        public decimal UnitPrice { get; set; }
        public string RequestId { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;       

    }
}
