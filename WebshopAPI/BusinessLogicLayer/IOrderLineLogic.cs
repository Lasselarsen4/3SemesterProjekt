using ModelAPI;
using System.Collections.Generic;

namespace WebshopAPI.BusinessLogicLayer
{
    public interface IOrderLineLogic
    {
        IEnumerable<OrderLine> GetAllOrderLines();
        OrderLine GetOrderLineById(int id);
        void AddOrderLine(OrderLine orderLine);
        void UpdateOrderLine(int id, OrderLine updatedOrderLine);
        void DeleteOrderLine(int id);
    }
}