namespace WebshopApplication.Models;

public class Order
{
    public int OrderId { get; set; }
    public Customer Cust { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public decimal TotalPrice { get; set; }
    public int CustomerId_FK { get; set; }
    public List<OrderLine> OrderLines { get; set; }
}

