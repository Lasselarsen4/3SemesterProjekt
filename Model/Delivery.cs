public class Delivery
{
    public Delivery(DateTime deliveryDate, decimal price)
    {
        DeliveryDate = deliveryDate;
        Price = price;
    }
        
    public Delivery()
    {
           
    }
        
    public DateTime DeliveryDate { get; set; }
    public decimal Price { get; set; }
}