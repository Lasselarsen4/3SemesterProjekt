using Microsoft.AspNetCore.Mvc;
using ModelAPI;

namespace WebshopAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private static readonly Dictionary<int, Cart> Carts = new Dictionary<int, Cart>();
        
        [HttpGet("{cartId}")]
        public ActionResult<Cart> Get(int cartId)
        {
            if (Carts.TryGetValue(cartId, out var cart))
            {
                return Ok(cart);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<Cart> Post()
        {
            var newCart = new Cart();
            Carts[newCart.CartId] = newCart;
            return CreatedAtAction(nameof(Get), new { cartId = newCart.CartId }, newCart);
        }

        [HttpPost("{cartId}/items")]
        public ActionResult<Cart> AddItem(int cartId, [FromBody] Product product, [FromQuery] int quantity = 1)
        {
            if (Carts.TryGetValue(cartId, out var cart))
            {
                cart.AddItem(product, quantity);
                return Ok(cart);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{cartId}/items/{productId}")]
        public ActionResult<Cart> RemoveItem(int cartId, int productId)
        {
            if (Carts.TryGetValue(cartId, out var cart))
            {
                cart.RemoveItem(productId);
                return Ok(cart);
            }
            else
            {
                return NotFound();
            }
        }
    }
}