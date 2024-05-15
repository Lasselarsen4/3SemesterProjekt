using Microsoft.AspNetCore.Mvc;
using ModelAPI;
using WebshopAPI.BusinessLogicLayer;
using System;

namespace WebshopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderLineController : ControllerBase
    {
        private readonly IOrderLineLogic _orderLineLogic;

        public OrderLineController(IOrderLineLogic orderLineLogic)
        {
            _orderLineLogic = orderLineLogic;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderLine>> Get()
        {
            var orderLines = _orderLineLogic.GetAllOrderLines();
            return Ok(orderLines);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderLine> Get(int id)
        {
            var orderLine = _orderLineLogic.GetOrderLineById(id);
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

            try
            {
                _orderLineLogic.AddOrderLine(orderLine);
                return CreatedAtAction(nameof(Get), new { id = orderLine.Quantity }, orderLine);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add order line: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderLine updatedOrderLine)
        {
            try
            {
                _orderLineLogic.UpdateOrderLine(id, updatedOrderLine);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Failed to update order line with quantity {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _orderLineLogic.DeleteOrderLine(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Failed to delete order line with quantity {id}: {ex.Message}");
            }
        }
    }
}
