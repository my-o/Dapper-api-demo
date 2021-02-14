using System.Collections.Generic;
using DapperApi.Models;

namespace DapperApi.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int id);
        void AddProduct(Product entity);
        void UpdateProduct(Product entity, int id);
        void RemoveProduct(int it);
    }
}