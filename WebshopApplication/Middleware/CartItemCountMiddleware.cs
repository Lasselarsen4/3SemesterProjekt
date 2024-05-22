using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebshopApplication.ServiceLayer;

namespace WebshopApplication.Middleware
{
    public class CartItemCountMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICartService _cartService;

        public CartItemCountMiddleware(RequestDelegate next, ICartService cartService)
        {
            _next = next;
            _cartService = cartService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cartItems = _cartService.GetCartItems().ToList();
            var cartItemCount = cartItems.Sum(item => item.Quantity);

            // Set the cart item count in HttpContext.Items
            context.Items["CartItemCount"] = cartItemCount;

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}