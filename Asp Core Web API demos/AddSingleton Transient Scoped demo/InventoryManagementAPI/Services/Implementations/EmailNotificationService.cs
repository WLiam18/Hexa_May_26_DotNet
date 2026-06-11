using InventoryManagementAPI.Services.Interfaces;

namespace InventoryManagementAPI.Services.Implementations
{
    public class EmailNotificationService:IEmailNotificationService
    {
        private readonly Guid _instanceId=Guid.NewGuid();

        public void SendLowStockNotification(string productName, int stockQuantity)
        {
            Console.WriteLine($"Email Service Instance ; {_instanceId}");
            Console.WriteLine($"Low stock alert : {productName}, Stock: {stockQuantity}");
        }
    }
}
