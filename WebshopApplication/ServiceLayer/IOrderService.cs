using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApplication.Models;

namespace WebshopApplication.ServiceLayer
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders(string? sortParam, int id = -1);
        Task<Order> GetOrderById(int id); // New method to fetch a single order by ID
        Task<bool> SaveOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int delId);
    }
}