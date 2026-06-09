using Microsoft.AspNetCore.Mvc;
using ProductAPI.DTOs;
using ProductAPI.Services.Interfaces;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        // DI Container injects IProductService automatically
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(new
            {
                statusCode = 200,
                message = "Products retrieved successfully",
                data = products
            });
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { statusCode = 400, message = "Invalid product ID" });

            var product = await _service.GetByIdAsync(id);
            if (product == null)
                return NotFound(new { statusCode = 404, message = "Product not found" });

            return Ok(new
            {
                statusCode = 200,
                message = "Product retrieved successfully",
                data = product
            });
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);

            if (!result.Success)
                return BadRequest(new { statusCode = 400, message = result.Message });

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, new
            {
                statusCode = 201,
                message = result.Message,
                data = result.Data
            });
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (!result.Success)
            {
                if (result.Message == "Product not found")
                    return NotFound(new { statusCode = 404, message = result.Message });
                return BadRequest(new { statusCode = 400, message = result.Message });
            }

            return Ok(new
            {
                statusCode = 200,
                message = result.Message,
                data = result.Data
            });
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.Success)
                return NotFound(new { statusCode = 404, message = result.Message });

            return Ok(new { statusCode = 200, message = result.Message });
        }
    }
}