using ModelAPI;
using System.Collections.Generic;

namespace WebshopAPI.BusinessLogicLayer
{
    public interface IOrderLogic
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int orderId);
        void PlaceOrder(Order order);
        void UpdateOrder(Order updatedOrder);
        void DeleteOrder(int orderId);
    }
}