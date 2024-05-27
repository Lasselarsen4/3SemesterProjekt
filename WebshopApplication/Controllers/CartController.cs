using Microsoft.AspNetCore.Mvc;
using WebshopApplication.Models;
using WebshopApplication.ServiceLayer;

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

        [HttpGet]
        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems().ToList();
            var viewModel = new CheckoutViewModel
            {
                CartItems = cartItems
            };
            ViewBag.TotalPrice = _cartService.GetTotalPrice();
            return View(viewModel);
        }

        [HttpGet("Checkout")]
        public IActionResult Checkout()
        {
            return View(new Customer());
        }

        [HttpPost("PlaceOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Attempt to save the customer
                var customerSaved = await _customerService.SaveCustomer(customer);
                if (customerSaved)
                {
                    // Retrieve the saved customer
                    var savedCustomer = (await _customerService.GetCustomers("none"))
                        .FirstOrDefault(c => c.Email == customer.Email && c.Phone == customer.Phone);

                    if (savedCustomer != null)
                    {
                        // Retrieve cart items
                        var cartItems = _cartService.GetCartItems();
                        if (cartItems.Any())
                        {
                            // Create the order
                            var order = new Order
                            {
                                Cust = savedCustomer,
                                OrderDate = DateTime.Now,
                                DeliveryDate = DateTime.Now.AddDays(7),
                                TotalPrice = _cartService.GetTotalPrice(),
                                CustomerId = savedCustomer.CustomerId,
                                OrderLines = cartItems.Select(item => new OrderLine
                                {
                                    ProductId = item.ProductId,
                                    Quantity = item.Quantity
                                }).ToList()
                            };

                            // Validate and update stock for each product in the order
                            foreach (var orderLine in order.OrderLines)
                            {
                                var product = await _productService.GetById(orderLine.ProductId);
                                if (product == null || product.Stock < orderLine.Quantity)
                                {
                                    return BadRequest($"Insufficient stock for product ID {orderLine.ProductId}");
                                }

                                product.Stock -= orderLine.Quantity;
                                await _productService.UpdateProduct(product);
                            }

                            // Attempt to save the order
                            var orderSaved = await _orderService.SaveOrder(order);
                        }
                    }
                }
            }
            
            _cartService.ClearCart();

            // Redirect to OrderConfirmation
            return RedirectToAction("OrderConfirmation");
        }

        [HttpGet("OrderConfirmation")]
        public IActionResult OrderConfirmation()
        {
            return View();
        }

        [HttpPost("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId, int quantity)
        {
            var product = await _productService.GetById(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            var existingCartItem = _cartService.GetCartItems().FirstOrDefault(item => item.ProductId == productId);
            var totalQuantity = existingCartItem != null ? existingCartItem.Quantity + quantity : quantity;

            if (totalQuantity > product.Stock)
            {
                return BadRequest($"Cannot add {quantity} items to the cart. Only {product.Stock} items available in stock.");
            }

            _cartService.AddToCart(product, quantity);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Update")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int productId, int quantity)
        {
            _cartService.UpdateCartItem(productId, quantity);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Remove")]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction(nameof(Index));
        }
    }
}
