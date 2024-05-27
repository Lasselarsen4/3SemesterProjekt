using ModelAPI;

namespace WebshopAPI.BusinessLogicLayer
{
    public class CartLogic : ICartLogic
    {
        private readonly Cart _cart = new Cart();

        public Cart GetCart()
        {
            return _cart;
        }

        public void AddToCart(Product product, int quantity)
        {
            var existingItem = _cart.Items.FirstOrDefault(i => i.ProductId == product.ProductId);
            if (existingItem == null)
            {
                _cart.Items.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    Quantity = quantity
                });
            }
            else
            {
                existingItem.Quantity += quantity;
            }
        }

        public void UpdateCartItem(int productId, int quantity)
        {
            var item = _cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                if (item.Quantity <= 0)
                {
                    _cart.Items.Remove(item);
                }
            }
        }

        public void RemoveFromCart(int productId)
        {
            var item = _cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                _cart.Items.Remove(item);
            }
        }

        public decimal GetTotalPrice()
        {
            return _cart.GetTotalPrice();
        }
    }
}