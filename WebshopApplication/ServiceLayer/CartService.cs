using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WebshopApplication.Models;

namespace WebshopApplication.ServiceLayer
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartCookieName = "CartCookie";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private Cart GetCartFromCookies()
        {
            var cookie = _httpContextAccessor.HttpContext.Request.Cookies[CartCookieName];
            return string.IsNullOrEmpty(cookie) ? new Cart() : JsonConvert.DeserializeObject<Cart>(cookie);
        }

        private void SaveCartToCookies(Cart cart)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = System.DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
            };
            var cartJson = JsonConvert.SerializeObject(cart);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CartCookieName, cartJson, cookieOptions);
        }

        public IEnumerable<CartItem> GetCartItems()
        {
            var cart = GetCartFromCookies();
            return cart.Items;
        }

        public void AddToCart(Product product, int quantity)
        {
            var cart = GetCartFromCookies();
            cart.AddItem(product, quantity);
            SaveCartToCookies(cart);
        }

        public void UpdateCartItem(int productId, int quantity)
        {
            var cart = GetCartFromCookies();
            cart.UpdateItem(productId, quantity);
            SaveCartToCookies(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCartFromCookies();
            cart.RemoveItem(productId);
            SaveCartToCookies(cart);
        }

        public decimal GetTotalPrice()
        {
            var cart = GetCartFromCookies();
            return cart.GetTotalPrice();
        }

        public void ClearCart()
        {
            var cart = new Cart();
            SaveCartToCookies(cart);
        }
    }
}
