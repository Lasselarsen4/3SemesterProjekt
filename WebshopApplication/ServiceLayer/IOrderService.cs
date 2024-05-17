using System.Collections.Generic;
using System.Threading.Tasks;
using ModelAPI;

namespace WebshopApplication.ServiceLayer
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders(string? sortParam, int id = -1);
        Task<bool> SaveOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int delId);
    }
}