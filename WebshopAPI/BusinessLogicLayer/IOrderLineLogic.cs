using ModelAPI;

namespace WebshopAPI.BusinessLogicLayer
{
    public interface IOrderLineLogic
    {
        IEnumerable<OrderLine> GetAllOrderLines();
        OrderLine GetOrderLineById(int orderId, int productId);
        void AddOrderLine(OrderLine orderLine);
        void UpdateOrderLine(int orderId, int productId, OrderLine updatedOrderLine);
        void DeleteOrderLine(int orderId, int productId);  // Ensure this method is included in the interface
    }
}