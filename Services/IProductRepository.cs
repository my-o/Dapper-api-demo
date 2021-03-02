using System.Collections.Generic;
using System.Threading.Tasks;
using DapperApi.Models;

namespace DapperApi.Services
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task AddProduct(Product entity);
        Task UpdateProduct(Product entity, int id);
        Task RemoveProduct(int it);
    }
}