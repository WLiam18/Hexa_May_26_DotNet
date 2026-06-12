using System;

namespace OrderAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        
        public Customer? Customer { get; set; }
        public Product? Product { get; set; }
    }
}