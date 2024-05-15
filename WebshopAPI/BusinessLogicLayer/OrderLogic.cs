using ModelAPI;
using System.Collections.Generic;
using System.Linq;

namespace WebshopAPI.BusinessLogicLayer
{
    public class OrderLogic : IOrderLogic
    {
        private readonly List<Order> _orders = new List<Order>();

        public IEnumerable<Order> GetAllOrders()
        {
            return _orders;
        }

        public Order GetOrderById(int orderId)
        {
            return _orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public void PlaceOrder(Order order)
        {
            order.OrderId = _orders.Count + 1;
            _orders.Add(order);
        }

        public void UpdateOrder(Order updatedOrder)
        {
            var orderToUpdate = _orders.FirstOrDefault(o => o.OrderId == updatedOrder.OrderId);
            if (orderToUpdate != null)
            {
                orderToUpdate.OrderDate = updatedOrder.OrderDate;
                orderToUpdate.DeliveryDate = updatedOrder.DeliveryDate;
                orderToUpdate.TotalPrice = updatedOrder.TotalPrice;
            }
        }

        public void DeleteOrder(int orderId)
        {
            var orderToRemove = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (orderToRemove != null)
            {
                _orders.Remove(orderToRemove);
            }
        }
    }
}