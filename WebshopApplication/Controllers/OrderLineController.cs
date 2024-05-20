using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ModelAPI;
using WebshopApplication.BusinessLogicLayerWeb;
using Microsoft.Extensions.Configuration;

namespace WebshopApplication.Controllers
{
    [Route("[controller]")]
    public class OrderLineController : Controller
    {
        private readonly OrderLineLogic _orderLineLogic;

        public OrderLineController(IConfiguration configuration)
        {
            _orderLineLogic = new OrderLineLogic(configuration);
        }

        // GET: /OrderLine
        [HttpGet]
        public async Task<IActionResult> Index(string sortParam)
        {
            var orderLines = await _orderLineLogic.GetOrderLines(sortParam);
            return View(orderLines);
        }

        // GET: /OrderLine/Details/5
        [HttpGet("Details/{orderId}/{productId}")]
        public async Task<IActionResult> Details(int orderId, int productId)
        {
            var orderLine = await _orderLineLogic.GetOrderLineById(orderId, productId);
            if (orderLine == null)
            {
                return NotFound();
            }
            return View(orderLine);
        }

        // GET: /OrderLine/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /OrderLine/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderLine orderLine)
        {
            if (ModelState.IsValid)
            {
                if (await _orderLineLogic.InsertOrderLine(orderLine))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(orderLine);
        }

        // GET: /OrderLine/Edit/5
        [HttpGet("Edit/{orderId}/{productId}")]
        public async Task<IActionResult> Edit(int orderId, int productId)
        {
            var orderLine = await _orderLineLogic.GetOrderLineById(orderId, productId);
            if (orderLine == null)
            {
                return NotFound();
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
                if (await _orderLineLogic.UpdateOrderLine(orderId, productId, orderLine))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(orderLine);
        }

        [HttpGet("Delete/{orderId}/{productId}")]
        public async Task<IActionResult> Delete(int orderId, int productId)
        {
            var orderLine = await _orderLineLogic.GetOrderLineById(orderId, productId);
            if (orderLine == null)
            {
                return NotFound();
            }
            return View(orderLine);
        }

        [HttpPost("Delete/{orderId}/{productId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int orderId, int productId)
        {
            if (await _orderLineLogic.DeleteOrderLine(orderId, productId))
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest($"Failed to delete order line with OrderId {orderId} and ProductId {productId}.");
        }
    }
}
