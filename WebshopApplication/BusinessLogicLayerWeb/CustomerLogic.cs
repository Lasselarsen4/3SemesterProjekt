using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ModelAPI;
using WebshopApplication.ServiceLayer;

namespace WebshopApplication.BusinessLogicLayerWeb
{
    public class CustomerLogic : ICustomerLogic
    {
        private readonly ICustomerService _customerService;

        public CustomerLogic(IConfiguration configuration)
        {
            _customerService = new CustomerService(configuration);
        }

        public Task<List<Customer>> GetCustomers(string sortParam)
        {
            return _customerService.GetCustomers(sortParam);
        }

        public Task<Customer> GetCustomerById(int id)
        {
            return _customerService.GetCustomers(null, id)
                .ContinueWith(task => task.Result != null && task.Result.Count > 0 ? task.Result[0] : null);
        }

        public Task<bool> InsertCustomer(Customer customer)
        {
            return _customerService.SaveCustomer(customer);
        }

        public Task<bool> UpdateCustomer(Customer customer)
        {
            return _customerService.UpdateCustomer(customer);
        }

        public Task<bool> DeleteCustomer(int id)
        {
            return _customerService.DeleteCustomer(id);
        }
    }
}