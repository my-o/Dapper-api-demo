using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace DapperApi.Services
{
    public abstract class BaseRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        protected BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // user for buffered queries that return a type
        protected T WithConnection<T>(Func<IDbConnection, T> getData)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                return getData(connection);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

        // use for buffered queries that do not return a type
        protected void WithConnection(Action<IDbConnection> getData)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                getData(connection);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

        // use for non-buffered queries that return a type
        protected TResult WithConnection<TRead, TResult>(Func<IDbConnection, TRead> getData, Func<TRead, TResult> process)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                var data = getData(connection);
                return process(data);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
    }
}