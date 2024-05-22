using ModelAPI;
using System.Collections.Generic;
using WebshopAPI.Database;

namespace WebshopAPI.BusinessLogicLayer
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderDB _orderDB;
        private readonly IProductDB _productDB;
        private readonly IOrderLineDB _orderLineDB;

        public OrderLogic(IOrderDB orderDB, IProductDB productDB, IOrderLineDB orderLineDB)
        {
            _orderDB = orderDB;
            _productDB = productDB;
            _orderLineDB = orderLineDB;
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

            foreach (var orderLine in order.OrderLines)
            {
                _orderLineDB.Add(orderLine);
            }

            UpdateProductStock(order);
        }

        public void UpdateOrder(Order updatedOrder)
        {
            _orderDB.Update(updatedOrder);
        }

        public void DeleteOrder(int orderId)
        {
            _orderDB.Delete(orderId);
        }

        public void UpdateProductStock(Order order)
        {
            foreach (var orderLine in order.OrderLines)
            {
                var product = _productDB.GetById(orderLine.ProductId);
                if (product != null)
                {
                    product.Stock -= orderLine.Quantity;
                    _productDB.Update(product);
                }
            }
        }
    }
}