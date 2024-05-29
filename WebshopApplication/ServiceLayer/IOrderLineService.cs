using WebshopApplication.Models;

namespace WebshopApplication.ServiceLayer
{
    public interface IOrderLineService
    {
        Task<List<OrderLine>> GetOrderLines(string? sortParam, int orderId = -1, int productId = -1);
        Task<bool> SaveOrderLine(OrderLine orderLine);
        Task<bool> UpdateOrderLine(int orderId, int productId, OrderLine orderLine);
        Task<bool> DeleteOrderLine(int orderId, int productId);
    }
}