using InventoryManagementAPI.Services.Interfaces;

namespace InventoryManagementAPI.Services.Implementations
{
    public class ApplicationSettingsService : IApplicationSettingsService
    {
        private readonly Guid _instanceId = Guid.NewGuid();
        public string GetCompanyName()
        {
            Console.WriteLine($"Settings service Instance: {_instanceId}");
            return "Inventory Management System";
        }

        public string GetSuuportEmail()
        {
            return "support@imsinventory.com";
        }
    }
}
