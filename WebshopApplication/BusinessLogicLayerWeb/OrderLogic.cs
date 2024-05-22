using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebshopApplication.ServiceLayer;
using WebshopApplication.Models;
using WebshopApplication.ServiceLayer.WebshopApplication.ServiceLayer;

namespace WebshopApplication.BusinessLogicLayerWeb
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderService _orderService;

        public OrderLogic(IConfiguration configuration)
        {
            _orderService = new OrderService(configuration);
        }

        public Task<List<Order>> GetOrders(string sortParam)
        {
            return _orderService.GetOrders(sortParam);
        }

        public Task<Order> GetOrderById(int id)
        {
            return _orderService.GetOrders(null, id)
                .ContinueWith(task => task.Result != null && task.Result.Count > 0 ? task.Result[0] : null);
        }

        public Task<bool> InsertOrder(Order order)
        {
            return _orderService.SaveOrder(order);
        }

        public Task<bool> UpdateOrder(Order order)
        {
            return _orderService.UpdateOrder(order);
        }

        public Task<bool> DeleteOrder(int id)
        {
            return _orderService.DeleteOrder(id);
        }
    }
}