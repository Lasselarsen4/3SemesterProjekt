using ModelAPI;
using System.Collections.Generic;

namespace WebshopAPI.Database
{
    public interface IOrderLineDB
    {
        IEnumerable<OrderLine> GetAll();
        OrderLine GetById(int orderId, int productId);
        void Add(OrderLine orderLine);
        void Update(int orderId, int productId, OrderLine orderLine);
        void Delete(int orderId, int productId);
    }
}