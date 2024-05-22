using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApplication.Models;

namespace WebshopApplication.ServiceLayer
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomers(string sortParam, int id = -1);
        Task<Customer> GetCustomerById(int id); // New method to fetch a single customer by ID
        Task<bool> SaveCustomer(Customer customer);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);
    }
}