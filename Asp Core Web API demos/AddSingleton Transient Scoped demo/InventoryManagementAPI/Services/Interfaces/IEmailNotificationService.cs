namespace InventoryManagementAPI.Services.Interfaces
{
    public interface IEmailNotificationService
    {
        void SendLowStockNotification(string productName, int stockQuantity);
    }
}
