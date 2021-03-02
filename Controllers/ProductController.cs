using System.Threading.Tasks;
using DapperApi.Models;
using DapperApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product entity)
        {
            await _productRepository.AddProduct(entity);
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(Product entity, int id)
        {
            await _productRepository.UpdateProduct(entity, id);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productRepository.RemoveProduct(id);
            return Ok();
        }
    }
}