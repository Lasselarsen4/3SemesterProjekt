using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebshopApplication.ServiceLayer;
using System.Security.Claims;
using ModelAPI;
using WebshopApplication.Models;
using System.Diagnostics;

namespace WebshopApplication.Controllers
{
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        private string GetUserId()
        {
            // Example implementation, modify based on your authentication system
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? HttpContext.Session.Id;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var cart = await _cartService.GetCartByUser(userId);
            if (cart != null)
            {
                return View(cart);
            }
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create()
        {
            var userId = GetUserId();
            var newCart = await _cartService.CreateCart(userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("items/add")]
        public async Task<IActionResult> AddItem(int productId, int quantity = 1)
        {
            var userId = GetUserId();
            var dbProduct = await _productService.GetById(productId);
            if (dbProduct == null)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var cart = await _cartService.AddItemToCart(userId, dbProduct, quantity);
            if (cart != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("items/remove")]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            var userId = GetUserId();
            var cart = await _cartService.RemoveItemFromCart(userId, productId);
            if (cart != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
