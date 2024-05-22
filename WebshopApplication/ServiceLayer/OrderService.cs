using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebshopApplication.Models;

namespace WebshopApplication.ServiceLayer
{
    public class OrderService : IOrderService
    {
        private readonly IServiceConnection _serviceConnection;

        public OrderService(IConfiguration configuration)
        {
            var baseUrl = configuration["ServiceUrlToUse"];
            _serviceConnection = new ServiceConnection(baseUrl);
        }

        public async Task<bool> SaveOrder(Order order)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/order";

            var orderForInsert = new
            {
                order.OrderDate,
                order.DeliveryDate,
                order.TotalPrice,
                order.CustomerId,
                Cust = new
                {
                    order.Cust.FirstName,
                    order.Cust.LastName,
                    order.Cust.Email,
                    order.Cust.Phone,
                    order.Cust.StreetName,
                    order.Cust.HouseNumber,
                    order.Cust.ZipCode
                },
                order.OrderLines
            };

            var json = JsonConvert.SerializeObject(orderForInsert);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            System.Diagnostics.Debug.WriteLine(json);

            var response = await _serviceConnection.CallServicePost(content);
            return response != null && response.IsSuccessStatusCode;
        }
        

        public async Task<Order> GetOrderById(int id)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/order/{id}";

            var response = await _serviceConnection.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<Order>(content);
                return order;
            }
            return null;
        }

        public async Task<List<Order>> GetOrders(string sortParam)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/order";
            if (!string.IsNullOrEmpty(sortParam) && sortParam.ToLower() != "none")
            {
                _serviceConnection.UseUrl += $"?sortBy={sortParam}";
            }

            var response = await _serviceConnection.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Order>>(content);
            }

            return new List<Order>();
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/order/{order.OrderId}";

            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _serviceConnection.CallServicePut(content);
            return response != null && response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/order/{id}";
            var response = await _serviceConnection.CallServiceDelete();
            return response != null && response.IsSuccessStatusCode;
        }
    }
}
