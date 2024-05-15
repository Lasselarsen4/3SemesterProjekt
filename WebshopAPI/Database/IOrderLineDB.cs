using ModelAPI;
using System.Collections.Generic;

namespace WebshopAPI.Database
{
    public interface IOrderLineDB
    {
        IEnumerable<OrderLine> GetAll();

        OrderLine GetById(int id);

        void Add(OrderLine orderLine);

        void Update(int id, OrderLine orderLine);

        void Delete(int id);
    }
}