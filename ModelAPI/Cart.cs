namespace ModelAPI
{
    public class Cart
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public List<CartItem> Items { get; set; }
        public DateTime CreatedAt { get; set; }

        public Cart()
        {
            Items = new List<CartItem>();
            CreatedAt = DateTime.UtcNow;
        }

        public void AddItem(Product product, int quantity = 1)
        {
            var existingItem = Items.Find(item => item.Product.ProductId == product.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new CartItem { Product = product, Quantity = quantity });
            }
        }

        public void RemoveItem(int productId)
        {
            var itemToRemove = Items.Find(item => item.Product.ProductId == productId);
            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
            }
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Product.ProductPrice * item.Quantity;
            }
            return total;
        }
    }
}