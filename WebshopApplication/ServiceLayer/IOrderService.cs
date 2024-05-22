using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApplication.Models;

namespace WebshopApplication.ServiceLayer
{
    public interface IOrderService
    {
        Task<bool> SaveOrder(Order order);
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrders(string sortParam = null, int id = -1);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int id);
    }
}