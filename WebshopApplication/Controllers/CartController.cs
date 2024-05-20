using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebshopApplication.Models;
using WebshopApplication.ServiceLayer;

namespace WebshopApplication.Controllers
{
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CartController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        // GET: /Cart
        [HttpGet]
        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            var totalPrice = _cartService.GetTotalPrice();
            ViewBag.TotalPrice = totalPrice;
            return View(cartItems);
        }

        // POST: /Cart/Add
        [HttpPost("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId, int quantity)
        {
            var product = await _productService.GetById(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            _cartService.AddToCart(product, quantity);
            return RedirectToAction(nameof(Index));
        }

        // POST: /Cart/Update
        [HttpPost("Update")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int productId, int quantity)
        {
            _cartService.UpdateCartItem(productId, quantity);
            return RedirectToAction(nameof(Index));
        }

        // POST: /Cart/Remove
        [HttpPost("Remove")]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction(nameof(Index));
        }
    }
}