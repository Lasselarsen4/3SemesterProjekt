using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ModelAPI;

namespace WebshopApplication.ServiceLayer
{
    public class OrderLineService : IOrderLineService
    {
        private readonly IServiceConnection _serviceConnection;

        public OrderLineService(IConfiguration configuration)
        {
            var baseUrl = configuration["ServiceUrlToUse"];
            _serviceConnection = new ServiceConnection(baseUrl);
        }

        public async Task<List<OrderLine>> GetOrderLines(string sortParam, int orderId = -1, int productId = -1)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/orderline";
            if (orderId > 0 && productId > 0)
            {
                _serviceConnection.UseUrl += $"/{orderId}/{productId}";
            }
            else if (!string.IsNullOrEmpty(sortParam) && sortParam.ToLower() != "none")
            {
                _serviceConnection.UseUrl += $"?sortBy={sortParam}";
            }

            var response = await _serviceConnection.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (orderId > 0 && productId > 0)
                {
                    var singleOrderLine = JsonConvert.DeserializeObject<OrderLine>(content);
                    return new List<OrderLine> { singleOrderLine };
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<OrderLine>>(content);
                }
            }

            return new List<OrderLine>();
        }

        public async Task<bool> SaveOrderLine(OrderLine orderLine)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/orderline";

            var json = JsonConvert.SerializeObject(orderLine);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _serviceConnection.CallServicePost(content);
            return response != null && response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateOrderLine(OrderLine orderLine)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/orderline/{orderLine.OrderId}/{orderLine.ProductId}";

            var json = JsonConvert.SerializeObject(orderLine);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _serviceConnection.CallServicePut(content);
            return response != null && response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrderLine(int orderId, int productId)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/orderline/{orderId}/{productId}";
            var response = await _serviceConnection.CallServiceDelete();
            return response != null && response.IsSuccessStatusCode;
        }
    }
}
