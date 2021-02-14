using System.Collections.Generic;
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

        public IEnumerable<Product> GetProducts()
        {
            return WithConnection(conn =>
            {
                var query = conn.Query<Product>(_commandText.GetProducts);
                return query;
            });
        }

        public Product GetProductById(int id)
        {
            return WithConnection(conn =>
            {
                var query = conn.QueryFirstOrDefault<Product>(_commandText.GetProductById,
                    new{ Id = id });
                return query;
            });
        }

        public void AddProduct(Product entity)
        {
            WithConnection(conn =>
            {
                conn.Execute(_commandText.AddProduct,
                    new { Name = entity.Name, Cost = entity.Cost, CreatedDate = entity.CreatedDate });
            });
        }

        public void UpdateProduct(Product entity, int id)
        {
            WithConnection(conn =>
            {
                conn.Execute(_commandText.UpdateProduct,
                    new Product(){ Name = entity.Name, Cost = entity.Cost, Id = id });
            });
        }

        public void RemoveProduct(int id)
        {
            WithConnection(conn =>
            {
                conn.Execute(_commandText.RemoveProduct,
                    new { Id = id });
            });
        }
    }
}