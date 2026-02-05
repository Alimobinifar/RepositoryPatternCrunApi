using Application.Products;
using Domain.ProductModels;
using Domain.ProductsVMS;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {

        private readonly ProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product dto)
        {
            var product = await _service.CreateAsync(dto);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product is null)
                return NotFound();
            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ProductDto dto)
        {
            var product = await _service.GetByIdAsync(id);
            if (product is null)
                return NotFound();

            product.Data.Title = dto.Title;
            product.Data.Price = dto.Price;

            await _service.UpdateAsync(product.Data);
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
