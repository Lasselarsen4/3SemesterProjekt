using WebshopApplication.Models;
using WebshopApplication.ServiceLayer;

namespace WebshopApplication.BusinessLogicLayerWeb
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderLogic(IConfiguration configuration)
        {
            _orderService = new OrderService(configuration);
            _customerService = new CustomerService(configuration);
        }

        public async Task<List<Order>> GetOrders(string sortParam)
        {
            return await _orderService.GetOrders(sortParam);
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order != null)
            {
                var customer = await _customerService.GetCustomerById(order.CustomerId);
                order.Cust = customer;
            }
            return order;
        }

        public async Task<bool> InsertOrder(Order order)
        {
            return await _orderService.SaveOrder(order);
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            return await _orderService.UpdateOrder(order);
        }

        public async Task<bool> DeleteOrder(int id)
        {
            return await _orderService.DeleteOrder(id);
        }
    }
}