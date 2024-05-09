using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebshopAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderLineController : ControllerBase
    {
        private readonly List<OrderLine> _orderLines = new List<OrderLine>
        {
            new OrderLine { Quantity = 2 },
            new OrderLine { Quantity = 1 },
            new OrderLine { Quantity = 3 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<OrderLine>> Get()
        {
            return Ok(_orderLines);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderLine> Get(int id)
        {
            var orderLine = _orderLines.FirstOrDefault(o => o.Quantity == id);
            if (orderLine == null)
            {
                return NotFound();
            }
            return Ok(orderLine);
        }

        [HttpPost]
        public ActionResult<OrderLine> Post([FromBody] OrderLine orderLine)
        {
            if (orderLine == null)
            {
                return BadRequest("Order line object is null");
            }

            // Generate a unique ID for the new order line

            _orderLines.Add(orderLine);

            return CreatedAtAction(nameof(Get), new { id = orderLine.Quantity }, orderLine);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderLine updatedOrderLine)
        {
            var orderLineToUpdate = _orderLines.FirstOrDefault(o => o.Quantity == id);
            if (orderLineToUpdate == null)
            {
                return NotFound($"Order line with quantity {id} not found");
            }

            orderLineToUpdate.Quantity = updatedOrderLine.Quantity;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var orderLineToRemove = _orderLines.FirstOrDefault(o => o.Quantity == id);
            if (orderLineToRemove == null)
            {
                return NotFound($"Order line with quantity {id} not found");
            }

            _orderLines.Remove(orderLineToRemove);

            return NoContent();
        }
    }
}
