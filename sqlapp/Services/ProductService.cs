using System.Data.SqlClient;
using sqlapp.Models;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "proofofdata.database.windows.net";
        private static string db_user = "sremani";
        private static string db_password = "2600$unTr33Lane";
        private static string db_database = "proofofdata";


        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;

            return new SqlConnection(_builder.ConnectionString);
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
