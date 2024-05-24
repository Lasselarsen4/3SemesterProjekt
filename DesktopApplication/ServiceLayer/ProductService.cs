using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DesktopApplication.Models;



namespace DesktopApplication.ServiceLayer;

public class ProductService : IProductService
{
    private readonly IServiceConnection _serviceConnection;
    private readonly string _baseUrl = "http://localhost:5042/api/product"; // Replace with your API base URL

    public ProductService()
    {
        _serviceConnection = new ServiceConnection();
    }

    public async Task<List<Product>> GetProducts()
    {
        var response = await _serviceConnection.CallServiceGet(_baseUrl);
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Product>>(content);
    }

    public async Task<Product> GetProductById(int productId)
    {
        var response = await _serviceConnection.CallServiceGet($"{_baseUrl}/{productId}");
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Product>(content);
    }

    public async Task<bool> SaveProduct(Product product)
    {
        var json = JsonConvert.SerializeObject(product);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _serviceConnection.CallServicePost(_baseUrl, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var json = JsonConvert.SerializeObject(product);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _serviceConnection.CallServicePut($"{_baseUrl}/{product.ProductId}", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        var response = await _serviceConnection.CallServiceDelete($"{_baseUrl}/{productId}");
        return response.IsSuccessStatusCode;
    }
}