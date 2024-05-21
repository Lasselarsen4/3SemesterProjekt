using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebshopApplication.Models;
using WebshopApplication.ServiceLayer;
using ModelAPI;

namespace WebshopApplication.Controllers
{
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public CartController(IProductService productService, ICartService cartService, IOrderService orderService, ICustomerService customerService)
        {
            _productService = productService;
            _cartService = cartService;
            _orderService = orderService;
            _customerService = customerService;
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

        // GET: /Cart/Checkout
        [HttpGet("Checkout")]
        public IActionResult Checkout()
        {
            return View();
        }

        // POST: /Cart/PlaceOrder
        [HttpPost("PlaceOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Save customer information
                await _customerService.SaveCustomer(customer);

                // Create order
                var cartItems = _cartService.GetCartItems();
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    DeliveryDate = DateTime.Now.AddDays(7),
                    TotalPrice = _cartService.GetTotalPrice(),
                    CustomerId_FK = customer.CustomerId,
                    OrderLines = new List<OrderLine>()
                };

                // Add order lines
                foreach (var item in cartItems)
                {
                    order.OrderLines.Add(new OrderLine
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    });
                }

                // Save order
                await _orderService.SaveOrder(order);

                // Clear the cart
                _cartService.ClearCart();

                return RedirectToAction("OrderConfirmation");
            }

            return View("Checkout", customer);
        }

        // GET: /Cart/OrderConfirmation
        [HttpGet("OrderConfirmation")]
        public IActionResult OrderConfirmation()
        {
            return View();
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
