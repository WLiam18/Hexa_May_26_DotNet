using OrderManagementApp.Interfaces;
using OrderManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementApp.Services
{
    public class OrderService
    {
        private readonly IProductRespository _productRepository;
        private readonly IEmailService _emailService;

        public OrderService(IProductRespository productRepository, IEmailService emailService)
        {
            _productRepository=productRepository;
            _emailService=emailService;

        }
        public string PlaceOrder(int productId, int quantity, string customerEmail)
        {
            Product? product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                return "Product Not Found";
            }
            if (quantity <= 0)
            {
                return "Quantity must be greater than zero";
            }
            if (product.AvailableQuantity < quantity)
            {
                return "Insufficient Stock";
            }
            _productRepository.UpdateStock(productId, quantity);
            _emailService.SendOrderConfirmation(customerEmail, product.ProductName);

            return "Order placed successfully";
        }
    }
}
