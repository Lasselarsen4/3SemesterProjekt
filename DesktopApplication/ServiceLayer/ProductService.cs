using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DesktopApplication.Models;

namespace DesktopApplication.ServiceLayer
{
    public class ProductService : IProductService
    {
        private readonly IServiceConnection _serviceConnection;

        public ProductService(IServiceConnection serviceConnection)
        {
            _serviceConnection = serviceConnection;
        }

        public async Task<List<Product>> GetProducts()
        {
            var response = await _serviceConnection.CallServiceGet("product");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Product>>(content);
        }

        public async Task<Product> GetProductById(int productId)
        {
            var response = await _serviceConnection.CallServiceGet($"product/{productId}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product>(content);
        }

        public async Task<bool> SaveProduct(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _serviceConnection.CallServicePost("product", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _serviceConnection.CallServicePut($"product/{product.ProductId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var response = await _serviceConnection.CallServiceDelete($"product/{productId}");
            return response.IsSuccessStatusCode;
        }
    }
}
