using Microsoft.AspNetCore.Mvc;
using WebshopApplication.BusinessLogicLayerWeb;
using WebshopApplication.Models;

namespace WebshopApplication.Controllers
{
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly OrderLogic _orderLogic;

        public OrderController(IConfiguration configuration)
        {
            _orderLogic = new OrderLogic(configuration);
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(string sortParam)
        {
            var orders = await _orderLogic.GetOrders(sortParam);
            return View(orders);
        }
        
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderLogic.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
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
                if (await _orderLogic.InsertOrder(order))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(order);
        }
        
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderLogic.GetOrderById(id);
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
                if (await _orderLogic.UpdateOrder(order))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(order);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderLogic.GetOrderById(id);
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
            if (await _orderLogic.DeleteOrder(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest($"Failed to delete order with ID {id}.");
        }
    }
}
