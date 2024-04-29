using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebshopAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly List<Order> _orders = new List<Order>
        {
            new Order(1, DateTime.Now.AddDays(-2)),
            new Order(2, DateTime.Now.AddDays(-1)),
            new Order(3, DateTime.Now)
        };

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return Ok(_orders);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = _orders.Find(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            // In a real application, you might validate the order data before adding it
            _orders.Add(order);
            return CreatedAtAction(nameof(Get), new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Order updatedOrder)
        {
            var index = _orders.FindIndex(o => o.OrderId == id);
            if (index == -1)
            {
                return NotFound();
            }
            _orders[index] = updatedOrder;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var orderToRemove = _orders.Find(o => o.OrderId == id);
            if (orderToRemove == null)
            {
                return NotFound();
            }
            _orders.Remove(orderToRemove);
            return NoContent();
        }
    }
}