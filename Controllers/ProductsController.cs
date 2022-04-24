using MarieSundbergAssignment.Models.Product;
using MarieSundbergAssignment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarieSundbergAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel request)
        {
            var item = await _service.CreateProductAsync(request);
            if (item != null)
            {
                return new OkObjectResult(item);
            }

            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return new OkObjectResult(await _service.GetAllProductsAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductModel request)
        {
            var item = await _service.UpdateProductAsync(id, request);
            if (item != null)
            {
                return new OkObjectResult(item);
            }

            return new BadRequestResult();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (await _service.DeleteProductAsync(id))
            {
                return new OkResult();
            }

            return new BadRequestResult();
        }
    }
}
