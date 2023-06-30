using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sqlapp.Models;
using sqlapp.Services;

namespace sqlapp.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Product> Products;

        public void OnGet()
        {
            ProductService _service = new ProductService();
            Products = _service.GetProducts();
        }
    }
}