namespace Model;

public class Product
{
    // Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
        
    // Constructor
    public Product()
    {
        CreatedAt = DateTime.Now; // Default value for CreatedAt property
    }
        
    // Methods (optional)
    public override string ToString()
    {
        return $"{Id} - {Name}: ${Price}";
    }
}