using Newtonsoft.Json;
using System.Text;
using WebshopApplication.Models;

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

        public async Task<Product> GetById(int productId)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/product/{productId}";
            var response = await _serviceConnection.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product>(content);
            }

            return null;
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
                if (id > 0)
                {
                    var singleProduct = JsonConvert.DeserializeObject<Product>(content);
                    return new List<Product> { singleProduct };
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<Product>>(content);
                }
            }

            return new List<Product>();
        }

        public async Task<bool> SaveProduct(Product product)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/product";

            var productForInsert = new
            {
                product.ProductName,
                product.ProductPrice,
                product.ProductDescription,
                product.Stock
            };

            var json = JsonConvert.SerializeObject(productForInsert);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _serviceConnection.CallServicePost(content);
            return response != null && response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/product/{product.ProductId}";

            var productForUpdate = new
            {
                product.ProductId,
                product.ProductName,
                product.ProductPrice,
                product.ProductDescription,
                product.Stock
            };

            var json = JsonConvert.SerializeObject(productForUpdate);
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
