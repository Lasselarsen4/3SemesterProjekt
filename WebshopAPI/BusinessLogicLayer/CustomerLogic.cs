using ModelAPI;
using WebshopAPI.Database;
using System.Collections.Generic;
using System.Linq;

namespace WebshopAPI.BusinessLogicLayer
{
    public class CustomerLogic : ICustomerLogic
    {
        private readonly ICustomerDB _customerDB;

        public CustomerLogic(ICustomerDB customerDB)
        {
            _customerDB = customerDB;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerDB.GetAll();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _customerDB.GetById(customerId);
        }

        public void AddCustomer(Customer customer)
        {
            _customerDB.Add(customer);
        }

        public void UpdateCustomer(Customer updatedCustomer)
        {
            _customerDB.Update(updatedCustomer);
        }

        public void DeleteCustomer(int customerId)
        {
            _customerDB.Delete(customerId);
        }
    }
}