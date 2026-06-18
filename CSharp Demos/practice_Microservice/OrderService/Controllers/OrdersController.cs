using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private static List<Order> _orders = new();
    private readonly IProductApiClient _productApiClient;

    public OrdersController(IProductApiClient productApiClient)
    {
        _productApiClient = productApiClient;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_orders);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        return order == null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var price = await _productApiClient.GetProductPriceAsync(request.ProductId);
        if (!price.HasValue) return BadRequest("Product not found");

        var order = new Order
        {
            Id = _orders.Count + 1,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            TotalPrice = price.Value * request.Quantity,
            Status = "Pending",
            CreatedAt = DateTime.Now
        };

        _orders.Add(order);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateStatus(int id, string status)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order == null) return NotFound();

        order.Status = status;
        return NoContent();
    }
}

public class CreateOrderRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
