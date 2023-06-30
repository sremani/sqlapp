using System.Data.SqlClient;
using sqlapp.Models;
using Microsoft.Extensions.Configuration;

namespace sqlapp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
        }

        public IEnumerable<Product> GetProducts()
        {
            var _products = new List<Product>();
            using (var _conn = GetConnection())
            {
                _conn.Open();
                var _sql = "SELECT ProductID, ProductName, Quantity FROM Products";
                using (var _cmd = new SqlCommand(_sql, _conn))
                {
                    using (var _reader = _cmd.ExecuteReader())
                    {
                        while (_reader.Read())
                        {
                            _products.Add(new Product()
                            {
                                ProductID = _reader.GetInt32(0),
                                ProductName = _reader.GetString(1),
                                Quantity = _reader.GetInt32(2)
                            });
                        }
                    }
                }
            }
            return _products;
        }
    }
}
