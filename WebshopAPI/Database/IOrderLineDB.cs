using ModelAPI;
using System.Collections.Generic;

namespace WebshopAPI.Database
{
    public interface IOrderLineDB
    {
        IEnumerable<OrderLine> GetAll();
        OrderLine GetById(int id);
        void Add(OrderLine orderLine);
        void Update(OrderLine orderLine);
        void Delete(int id);
    }
}