using System.Collections.Generic;
using ModelAPI;
using WebshopAPI.Database;

namespace WebshopAPI.BusinessLogicLayer
{
    public class CustomerLogic : ICustomerLogic
    {
        private readonly ICustomerDB _customerDb;

        public CustomerLogic(ICustomerDB customerDb)
        {
            _customerDb = customerDb;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerDb.GetAll();
        }

        public Customer GetCustomerById(int id)
        {
            return _customerDb.GetById(id);
        }

        public void AddCustomer(Customer customer)
        {
            _customerDb.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerDb.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            _customerDb.Delete(id);
        }
    }
}