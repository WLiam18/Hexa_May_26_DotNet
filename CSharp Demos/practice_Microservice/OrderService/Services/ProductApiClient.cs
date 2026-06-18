using System.Net.Http.Headers;
using System.Text.Json;

namespace OrderService.Services;

public class ProductApiClient : IProductApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<decimal?> GetProductPriceAsync(int productId)
    {
        // Forward the JWT token from the original request
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token?.Replace("Bearer ", ""));

        var response = await _httpClient.GetAsync($"api/Products/{productId}");
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("price").GetDecimal();
    }
}
