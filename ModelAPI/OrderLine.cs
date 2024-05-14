namespace ModelAPI;

public class OrderLine
{
    public OrderLine(int quantity)
    {
        Quantity = quantity;
    }

    public OrderLine()
    {
           
    }

    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
}