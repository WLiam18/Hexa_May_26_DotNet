using InventoryManagementAPI.Services.Interfaces;

namespace InventoryManagementAPI.Services.Implementations
{
    public class RequestTrackingService : IRequestTrackingService
    {
        private readonly string  _requestId = Guid.NewGuid().ToString();
        public string GenerateRequestId()
        {
            return _requestId;
        }
    }
}
