using Microsoft.AspNetCore.Mvc;
using ModelAPI;

namespace WebshopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly List<Order> _orders = new List<Order>
        {
            new Order(1, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2), 50.99m),
            new Order(2, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(3), 75.49m),
            new Order(3, DateTime.Now, DateTime.Now.AddDays(1), 30.99m)
        };

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return Ok(_orders);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order object is null");
            }

            // Generate a unique ID for the new order
            order.OrderId = _orders.Count + 1;
            
            _orders.Add(order);
            
            return CreatedAtAction(nameof(Get), new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Order updatedOrder)
        {
            var orderToUpdate = _orders.FirstOrDefault(o => o.OrderId == id);
            if (orderToUpdate == null)
            {
                return NotFound($"Order with ID {id} not found");
            }

            orderToUpdate.OrderDate = updatedOrder.OrderDate;
            orderToUpdate.DeliveryDate = updatedOrder.DeliveryDate;
            orderToUpdate.TotalPrice = updatedOrder.TotalPrice;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var orderToRemove = _orders.FirstOrDefault(o => o.OrderId == id);
            if (orderToRemove == null)
            {
                return NotFound($"Order with ID {id} not found");
            }

            _orders.Remove(orderToRemove);

            return NoContent();
        }
    }
}
