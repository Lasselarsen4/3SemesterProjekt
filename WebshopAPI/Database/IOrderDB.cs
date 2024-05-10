using Model;
using System.Collections.Generic;

namespace WebshopAPI.Database
{
    public interface IOrderDB
    {
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
    }
}