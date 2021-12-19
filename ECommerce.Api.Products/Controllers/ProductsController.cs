using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider _productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            _productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            return Ok(await _productsProvider.GetProductsAsync());
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            return Ok(await _productsProvider.GetProductAsync(id));
        }
    }
}
