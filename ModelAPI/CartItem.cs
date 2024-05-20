namespace ModelAPI
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }

        public CartItem() { } // Parameterless constructor

        public CartItem(int productId, string productName, decimal productPrice, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = quantity;
        }

        public decimal GetTotalPrice()
        {
            return ProductPrice * Quantity;
        }
    }
}