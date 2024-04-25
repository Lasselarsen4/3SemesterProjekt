public class Order
{
    public Order(int orderId, DateTime orderDate)
    {
        OrderId = orderId;
        OrderDate = orderDate;
    }

    public Order()
    {
           
    }

    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
}