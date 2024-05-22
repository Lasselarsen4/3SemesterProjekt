using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebshopApplication.Models;
using WebshopApplication.ServiceLayer;

namespace WebshopApplication.BusinessLogicLayerWeb
{
    public class OrderLineLogic : IOrderLineLogic
    {
        private readonly IOrderLineService _orderLineService;

        public OrderLineLogic(IConfiguration configuration)
        {
            _orderLineService = new OrderLineService(configuration);
        }

        public Task<List<OrderLine>> GetOrderLines(string sortParam)
        {
            return _orderLineService.GetOrderLines(sortParam);
        }

        public Task<OrderLine> GetOrderLineById(int orderId, int productId)
        {
            return _orderLineService.GetOrderLines(null, orderId, productId)
                .ContinueWith(task => task.Result != null && task.Result.Count > 0 ? task.Result[0] : null);
        }

        public Task<bool> InsertOrderLine(OrderLine orderLine)
        {
            return _orderLineService.SaveOrderLine(orderLine);
        }

        public Task<bool> UpdateOrderLine(int orderId, int productId, OrderLine orderLine)
        {
            if (orderLine.OrderId != orderId || orderLine.ProductId != productId)
            {
                throw new ArgumentException("Order line ID mismatch.");
            }
            return _orderLineService.UpdateOrderLine(orderLine);
        }

        public Task<bool> DeleteOrderLine(int orderId, int productId)
        {
            return _orderLineService.DeleteOrderLine(orderId, productId);
        }
    }
}