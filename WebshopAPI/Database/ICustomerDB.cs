using ModelAPI;
using System.Collections.Generic;

namespace WebshopAPI.Database
{
    public interface ICustomerDB
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }
}