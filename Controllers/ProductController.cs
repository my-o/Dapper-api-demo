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
        public ActionResult<Product> GetProducts()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            return Ok(product);
        }

        [HttpPost]
        public ActionResult AddProduct(Product entity)
        {
            _productRepository.AddProduct(entity);
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(Product entity, int id)
        {
            _productRepository.UpdateProduct(entity, id);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            _productRepository.RemoveProduct(id);
            return Ok();
        }
    }
}