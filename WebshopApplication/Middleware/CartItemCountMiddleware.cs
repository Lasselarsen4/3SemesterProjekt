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
            
            context.Items["CartItemCount"] = cartItemCount;
            
            await _next(context);
        }
    }
}