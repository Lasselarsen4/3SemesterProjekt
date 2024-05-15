using ModelAPI;
using System.Collections.Generic;

namespace WebshopAPI.BusinessLogicLayer
{
    public interface ICustomerLogic
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int customerId);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer updatedCustomer);
        void DeleteCustomer(int customerId);
    }
}