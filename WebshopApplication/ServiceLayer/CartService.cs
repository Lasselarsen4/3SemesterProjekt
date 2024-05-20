using ModelAPI;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace WebshopApplication.ServiceLayer
{
    public class CartService : ICartService
    {
        private readonly IServiceConnection _serviceConnection;

        public CartService(IConfiguration configuration)
        {
            var baseUrl = configuration["ServiceUrlToUse"];
            _serviceConnection = new ServiceConnection(baseUrl);
        }

        public async Task<Cart> GetCartByUser(string userId)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/cart/current?userId={userId}";
            var response = await _serviceConnection.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Cart>(content);
            }
            return null;
        }

        public async Task<Cart> CreateCart(string userId)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/cart/create?userId={userId}";
            var response = await _serviceConnection.CallServicePost(null);
            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Cart>(content);
            }
            return null;
        }

        public async Task<Cart> AddItemToCart(string userId, Product product, int quantity)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/cart/items/add?userId={userId}&quantity={quantity}";
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _serviceConnection.CallServicePost(content);
            if (response != null && response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Cart>(responseContent);
            }
            return null;
        }

        public async Task<Cart> RemoveItemFromCart(string userId, int productId)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/cart/items/remove?userId={userId}&productId={productId}";
            var response = await _serviceConnection.CallServiceDelete();
            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Cart>(content);
            }
            return null;
        }
    }
}
