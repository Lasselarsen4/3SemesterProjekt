// IOrderLogic.cs
using ModelAPI;

public interface IOrderLogic
{
    IEnumerable<Order> GetAllOrders();
    Order GetOrderById(int id);
    void PlaceOrder(Order order);
    void UpdateOrder(Order order);
    void DeleteOrder(int id);
    void UpdateProductStock(Order order);
}