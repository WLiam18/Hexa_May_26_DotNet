namespace OrderAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerType { get; set; } = string.Empty; // Regular, Premium, VIP
    }
}