namespace WebshopApplication.Models;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductDescription { get; set; }
    public int Stock { get; set; }
    public byte[] RowVersion { get; set; } // Add this property
}
