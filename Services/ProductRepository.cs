using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DapperApi.Models;
using DapperApi.Services.Queries;
using Microsoft.Extensions.Configuration;

namespace DapperApi.Services
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private readonly ICommandText _commandText;

        public ProductRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await WithConnection(conn =>
            {
                var query = conn.QueryAsync<Product>(_commandText.GetProducts);
                return query;
            });
        }

        public async Task<Product> GetProductById(int id)
        {
            return await WithConnection(conn =>
            {
                var query = conn.QueryFirstOrDefaultAsync<Product>(_commandText.GetProductById,
                    new{ Id = id });
                return query;
            });
        }

        public async Task AddProduct(Product entity)
        {
            await WithConnection(conn =>
            {
                conn.ExecuteAsync(_commandText.AddProduct,
                    new { Name = entity.Name, Cost = entity.Cost, CreatedDate = entity.CreatedDate });
            });
        }

        public async Task UpdateProduct(Product entity, int id)
        {
            await WithConnection(conn =>
            {
                conn.ExecuteAsync(_commandText.UpdateProduct,
                    new Product(){ Name = entity.Name, Cost = entity.Cost, Id = id });
            });
        }

        public async Task RemoveProduct(int id)
        {
            await WithConnection(conn =>
            {
                conn.ExecuteAsync(_commandText.RemoveProduct,
                    new { Id = id });
            });
        }
    }
}