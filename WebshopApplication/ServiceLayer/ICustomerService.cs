using WebshopApplication.Models;

namespace WebshopApplication.ServiceLayer
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomers(string sortParam, int id = -1);
        Task<Customer> GetCustomerById(int id);
        Task<bool> SaveCustomer(Customer customer);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);
    }
}