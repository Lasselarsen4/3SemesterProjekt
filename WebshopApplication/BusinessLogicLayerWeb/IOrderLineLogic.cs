using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApplication.Models;

namespace WebshopApplication.BusinessLogicLayerWeb
{
    public interface IOrderLineLogic
    {
        Task<List<OrderLine>> GetOrderLines(string sortParam);
        Task<OrderLine> GetOrderLineById(int orderId, int productId);
        Task<bool> InsertOrderLine(OrderLine orderLine);
        Task<bool> UpdateOrderLine(int orderId, int productId, OrderLine orderLine);
        Task<bool> DeleteOrderLine(int orderId, int productId);
    }
}