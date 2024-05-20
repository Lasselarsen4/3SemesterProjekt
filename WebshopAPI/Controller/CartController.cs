using Microsoft.AspNetCore.Mvc;
using ModelAPI;
using WebshopAPI.BusinessLogicLayer;
using System.Security.Claims;
using WebshopAPI.Database;

namespace WebshopAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartLogic _cartLogic;
        private readonly IProductDB _productDB;

        public CartController(ICartLogic cartLogic, IProductDB productDB)
        {
            _cartLogic = cartLogic;
            _productDB = productDB;
        }

        private string GetUserId()
        {
            // Example implementation, modify based on your authentication system
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? HttpContext.Session.Id;
        }

        [HttpGet("current")]
        public ActionResult<Cart> GetCurrent()
        {
            var userId = GetUserId();
            var cart = _cartLogic.GetCartByUser(userId);
            if (cart != null)
            {
                return Ok(cart);
            }
            return NotFound();
        }

        [HttpPost("create")]
        public ActionResult<Cart> Create()
        {
            var userId = GetUserId();
            var newCart = _cartLogic.CreateCart(userId);
            return CreatedAtAction(nameof(GetCurrent), new { }, newCart);
        }

        [HttpPost("items/add")]
        public ActionResult<Cart> AddItem([FromBody] Product product, [FromQuery] int quantity = 1)
        {
            var userId = GetUserId();
            var dbProduct = _productDB.GetById(product.ProductId);
            if (dbProduct == null)
            {
                return NotFound(); // Product not found
            }

            var cart = _cartLogic.AddItemToCart(userId, dbProduct, quantity);
            if (cart != null)
            {
                return Ok(cart);
            }
            return NotFound();
        }

        [HttpDelete("items/remove")]
        public ActionResult<Cart> RemoveItem([FromQuery] int productId)
        {
            var userId = GetUserId();
            var cart = _cartLogic.RemoveItemFromCart(userId, productId);
            if (cart != null)
            {
                return Ok(cart);
            }
            return NotFound();
        }
    }
}
