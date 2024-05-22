using Microsoft.AspNetCore.Mvc;
using ModelAPI;
using WebshopAPI.BusinessLogicLayer;
using System;
using System.Collections.Generic;

namespace WebshopAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderLogic _orderLogic;

        public OrderController(IOrderLogic orderLogic)
        {
            _orderLogic = orderLogic;
        }

        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order object is null");
            }

            try
            {
                foreach (var orderLine in order.OrderLines)
                {
                    // Validate order line if necessary
                }

                _orderLogic.PlaceOrder(order);
                return CreatedAtAction(nameof(Get), new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to place order: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = _orderLogic.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            var orders = _orderLogic.GetAllOrders();
            return Ok(orders);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Order updatedOrder)
        {
            try
            {
                _orderLogic.UpdateOrder(updatedOrder);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Failed to update order with ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _orderLogic.DeleteOrder(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Failed to delete order with ID {id}: {ex.Message}");
            }
        }
    }
}
