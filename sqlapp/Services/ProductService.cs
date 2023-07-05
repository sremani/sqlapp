using System.Data.SqlClient;
using sqlapp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json;

namespace sqlapp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        private readonly IFeatureManager _featureManager;

        public ProductService(IConfiguration configuration, IFeatureManager featureManager)
        {
            _configuration = configuration;
            _featureManager = featureManager;
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration["SQLConnection"]);
        }

        public async Task<bool> IsBeta()
        {
            if (await _featureManager.IsEnabledAsync("beta"))
                return true;
            else
            {
                return false;
            }
        }

        public async Task<List<Product>> GetProducts()
        {
            const string functionURL = "https://proofofdunction.azurewebsites.net/api/GetProducts?code=O3K_A8aT1nWw1zqnPHcU4WaCHRaxKBxuELT4IQ3VasdGAzFu7qz8_w==";
            
            using(HttpClient client = new())
            {
                var response = await client.GetAsync(functionURL);
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Product>>(result);
                }
                else
                {
                    return null;
                }

            }
        }
    }
}
