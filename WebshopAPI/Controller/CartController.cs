using Microsoft.AspNetCore.Mvc;
using ModelAPI;
using WebshopAPI.BusinessLogicLayer;

namespace WebshopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartLogic _cartLogic;

        public CartController(ICartLogic cartLogic)
        {
            _cartLogic = cartLogic;
        }

        [HttpGet("{cartId}")]
        public ActionResult<Cart> Get(int cartId)
        {
            var cart = _cartLogic.GetCartById(cartId);
            if (cart != null)
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
            return CreatedAtAction(nameof(Get), new { cartId = newCart.CartId }, newCart);
        }

        [HttpPost("{cartId}/items")]
        public ActionResult<Cart> AddItem(int cartId, [FromBody] Product product, [FromQuery] int quantity = 1)
        {
            var cart = _cartLogic.AddItemToCart(cartId, product, quantity);
            if (cart != null)
            {
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
            var cart = _cartLogic.RemoveItemFromCart(cartId, productId);
            if (cart != null)
            {
                return Ok(cart);
            }
            else
            {
                return NotFound();
            }
        }
    }
}