using Microsoft.AspNetCore.Mvc;
using ModelAPI;
using WebshopAPI.BusinessLogicLayer;
using System;
using System.Collections.Generic;

namespace WebshopAPI.Controller
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

        [HttpGet("{orderId}/{productId}")]
        public ActionResult<OrderLine> Get(int orderId, int productId)
        {
            var orderLine = _orderLineLogic.GetOrderLineById(orderId, productId);
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
                return CreatedAtAction(nameof(Get), new { orderId = orderLine.OrderId, productId = orderLine.ProductId }, orderLine);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add order line: {ex.Message}");
            }
        }

        [HttpPut("{orderId}/{productId}")]
        public IActionResult Put(int orderId, int productId, [FromBody] OrderLine updatedOrderLine)
        {
            try
            {
                _orderLineLogic.UpdateOrderLine(orderId, productId, updatedOrderLine);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Failed to update order line with OrderId {orderId} and ProductId {productId}: {ex.Message}");
            }
        }

        [HttpDelete("{orderId}/{productId}")]
        public IActionResult Delete(int orderId, int productId)
        {
            try
            {
                _orderLineLogic.DeleteOrderLine(orderId, productId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Failed to delete order line with OrderId {orderId} and ProductId {productId}: {ex.Message}");
            }
        }
    }
}
