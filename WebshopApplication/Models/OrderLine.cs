namespace WebshopApplication.Models
{
    public class OrderLine
    {
        public int OrderLineId { get; set; }
        public int OrderId { get; set; }  // Ensure this is correctly mapped
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}