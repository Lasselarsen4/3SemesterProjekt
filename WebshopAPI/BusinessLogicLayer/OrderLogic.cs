using ModelAPI;
using System.Collections.Generic;
using System.Linq;
using WebshopAPI.Database;

namespace WebshopAPI.BusinessLogicLayer
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderDB _orderDB;

        public OrderLogic(IOrderDB orderDB)
        {
            _orderDB = orderDB;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderDB.GetAll();
        }

        public Order GetOrderById(int orderId)
        {
            return _orderDB.GetById(orderId);
        }

        public void PlaceOrder(Order order)
        {
            _orderDB.Add(order);
        }

        public void UpdateOrder(Order updatedOrder)
        {
            _orderDB.Update(updatedOrder);
        }

        public void DeleteOrder(int orderId)
        {
            _orderDB.Delete(orderId);
        }
    }
}