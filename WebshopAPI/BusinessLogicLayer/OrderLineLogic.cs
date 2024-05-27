using ModelAPI;
using WebshopAPI.Database;

namespace WebshopAPI.BusinessLogicLayer
{
    public class OrderLineLogic : IOrderLineLogic
    {
        private readonly IOrderLineDB _orderLineDB;

        public OrderLineLogic(IOrderLineDB orderLineDB)
        {
            _orderLineDB = orderLineDB;
        }

        public IEnumerable<OrderLine> GetAllOrderLines()
        {
            return _orderLineDB.GetAll();
        }

        public OrderLine GetOrderLineById(int orderId, int productId)
        {
            return _orderLineDB.GetById(orderId, productId);
        }

        public void AddOrderLine(OrderLine orderLine)
        {
            if (orderLine == null)
            {
                throw new ArgumentNullException(nameof(orderLine));
            }

            _orderLineDB.Add(orderLine);
        }

        public void UpdateOrderLine(int orderId, int productId, OrderLine updatedOrderLine)
        {
            if (updatedOrderLine == null)
            {
                throw new ArgumentNullException(nameof(updatedOrderLine));
            }
            

            _orderLineDB.Update(orderId, productId, updatedOrderLine);
        }

        public void DeleteOrderLine(int orderId, int productId)
        {
            _orderLineDB.Delete(orderId, productId);
        }
    }
}