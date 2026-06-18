namespace OrderService.Services;

public interface IProductApiClient
{
    Task<decimal?> GetProductPriceAsync(int productId);
}
