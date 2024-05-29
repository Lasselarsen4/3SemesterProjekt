using Microsoft.AspNetCore.Mvc;
using WebshopApplication.Models;
using WebshopApplication.ServiceLayer;

namespace WebshopApplication.Controllers
{
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(IOrderService orderService, ICustomerService customerService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(string sortParam)
        {
            var orders = await _orderService.GetOrders(sortParam);
            return View(orders);
        }
        
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Cust = await _customerService.GetCustomerById(order.CustomerId);

            return View(order);
        }
        
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                if (await _orderService.SaveOrder(order))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(order);
        }
        
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest("Order ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                if (await _orderService.UpdateOrder(order))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(order);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _orderService.DeleteOrder(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest($"Failed to delete order with ID {id}.");
        }
    }
}
