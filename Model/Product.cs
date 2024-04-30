namespace Model
{
    public class Product
    {
        
        public int productId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Id: {productId}, Name: {Name}, Price: {Price}, Description: {Description}";
        }
    }
}
