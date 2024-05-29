using Microsoft.AspNetCore.Mvc;
using WebshopApplication.Models;
using WebshopApplication.ServiceLayer;

namespace WebshopApplication.Controllers
{
    [Route("[controller]")]
    public class OrderLineController : Controller
    {
        private readonly IOrderLineService _orderLineService;

        public OrderLineController(IConfiguration configuration)
        {
            _orderLineService = new OrderLineService(configuration);
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(string sortParam)
        {
            var orderLines = await _orderLineService.GetOrderLines(sortParam);
            return View(orderLines);
        }
        
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderLine orderLine)
        {
            if (ModelState.IsValid)
            {
                if (await _orderLineService.SaveOrderLine(orderLine))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(orderLine);
        }
        

        [HttpPost("Edit/{orderId}/{productId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int orderId, int productId, OrderLine orderLine)
        {
            if (orderId != orderLine.OrderId || productId != orderLine.ProductId)
            {
                return BadRequest("Order line ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                if (await _orderLineService.UpdateOrderLine(orderId, productId, orderLine))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(orderLine);
        }
        

        [HttpPost("Delete/{orderId}/{productId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int orderId, int productId)
        {
            if (await _orderLineService.DeleteOrderLine(orderId, productId))
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest($"Failed to delete order line with OrderId {orderId} and ProductId {productId}.");
        }
    }
}
