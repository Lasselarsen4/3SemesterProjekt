namespace WebshopApplication.Models;

public class OrderLine
{
    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
}