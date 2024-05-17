using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ModelAPI;

namespace WebshopApplication.ServiceLayer
{
    public class ProductService : IProductService
    {
        private readonly IServiceConnection _serviceConnection;

        public ProductService(IConfiguration configuration)
        {
            var baseUrl = configuration["ServiceUrlToUse"];
            _serviceConnection = new ServiceConnection(baseUrl);
        }

        public async Task<List<Product>> GetProducts(string sortParam, int id = -1)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/product";
            if (id > 0)
            {
                _serviceConnection.UseUrl += $"/{id}";
            }
            else if (!string.IsNullOrEmpty(sortParam) && sortParam.ToLower() != "none")
            {
                _serviceConnection.UseUrl += $"?sortBy={sortParam}";
            }

            var response = await _serviceConnection.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Product>>(content);
            }

            return new List<Product>();
        }

        public async Task<bool> SaveProduct(Product product)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/product";
    
            // Do not include productId in the JSON for insertion
            var productForInsert = new
            {
                product.ProductName,
                product.ProductPrice,
                product.ProductDescription
            };

            var json = JsonConvert.SerializeObject(productForInsert);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _serviceConnection.CallServicePost(content);
            return response != null && response.IsSuccessStatusCode;
        }


        public async Task<bool> UpdateProduct(Product product)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/product/{product.ProductId}";
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _serviceConnection.CallServicePut(content);
            return response != null && response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/product/{id}";
            var response = await _serviceConnection.CallServiceDelete();
            return response != null && response.IsSuccessStatusCode;
        }
    }
}
