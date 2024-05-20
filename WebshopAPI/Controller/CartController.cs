using Microsoft.AspNetCore.Mvc;
using WebshopAPI.BusinessLogicLayer;
using WebshopAPI.Database;
using ModelAPI;

namespace WebshopAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IProductLogic _productLogic;
        private static Cart _cart = new Cart();

        public CartController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CartItem>> Get()
        {
            return Ok(_cart.Items);
        }

        [HttpPost("add/{productId}")]
        public ActionResult Add(int productId, [FromQuery] int quantity)
        {
            var product = _productLogic.GetProductById(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            _cart.AddItem(product, quantity);
            return Ok(_cart.Items);
        }

        [HttpPut("update/{productId}")]
        public ActionResult Update(int productId, [FromQuery] int quantity)
        {
            _cart.UpdateItem(productId, quantity);
            return Ok(_cart.Items);
        }

        [HttpDelete("remove/{productId}")]
        public ActionResult Remove(int productId)
        {
            _cart.RemoveItem(productId);
            return Ok(_cart.Items);
        }

        [HttpGet("total")]
        public ActionResult<decimal> GetTotal()
        {
            return Ok(_cart.GetTotalPrice());
        }
    }
}