using ModelAPI;
using System.Collections.Generic;
using System.Linq;

namespace WebshopAPI.BusinessLogicLayer
{
    public class CustomerLogic : ICustomerLogic
    {
        private readonly List<Customer> _customers = new List<Customer>();

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customers;
        }

        public Customer GetCustomerById(int customerId)
        {
            return _customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        public void AddCustomer(Customer customer)
        {
            customer.CustomerId = _customers.Count + 1;
            _customers.Add(customer);
        }

        public void UpdateCustomer(Customer updatedCustomer)
        {
            var customerToUpdate = _customers.FirstOrDefault(c => c.CustomerId == updatedCustomer.CustomerId);
            if (customerToUpdate != null)
            {
                customerToUpdate.FirstName = updatedCustomer.FirstName;
                customerToUpdate.LastName = updatedCustomer.LastName;
                customerToUpdate.Email = updatedCustomer.Email;
                customerToUpdate.Address = updatedCustomer.Address;
                customerToUpdate.Phone = updatedCustomer.Phone;
            }
        }

        public void DeleteCustomer(int customerId)
        {
            var customerToRemove = _customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customerToRemove != null)
            {
                _customers.Remove(customerToRemove);
            }
        }
    }
}