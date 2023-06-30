using sqlapp.Models;

namespace sqlapp.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
    }
}