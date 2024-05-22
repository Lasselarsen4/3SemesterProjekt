using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApplication.Models;

namespace WebshopApplication.BusinessLogicLayerWeb
{
    public interface IOrderLogic
    {
        Task<List<Order>> GetOrders(string sortParam);
        Task<Order> GetOrderById(int id);
        Task<bool> InsertOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int id);
    }
}