using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApplication.Models;

namespace WebshopApplication.ServiceLayer
{
    public interface IOrderService
    {
        Task<bool> SaveOrder(Order order);
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrders(string sortParam);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int id);
    }
}