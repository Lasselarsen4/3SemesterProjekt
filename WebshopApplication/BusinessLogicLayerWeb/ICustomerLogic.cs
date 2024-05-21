using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApplication.Models;

namespace WebshopApplication.BusinessLogicLayerWeb
{
    public interface ICustomerLogic
    {
        Task<List<Customer>?> GetCustomers(string? sortParam);
        Task<Customer?> GetCustomerById(int id);
        Task<bool> InsertCustomer(Customer customer);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int delId);
    }
}