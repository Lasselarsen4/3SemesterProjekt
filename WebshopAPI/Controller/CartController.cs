using Microsoft.AspNetCore.Mvc;
using ModelAPI;
using WebshopAPI.BusinessLogicLayer;
using System.Collections.Generic;
using System;

namespace WebshopAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartLogic _cartLogic;

        public CartController(ICartLogic cartLogic)
        {
            _cartLogic = cartLogic;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cart>> Get()
        {
            var carts = _cartLogic.GetAllCarts();
            return Ok(carts);
        }

        [HttpGet("{id}")]
        public ActionResult<Cart> Get(int id)
        {
            var cart = _cartLogic.GetCartById(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost]
        public ActionResult<Cart> Post([FromBody] Cart cart)
        {
            if (cart == null)
            {
                return BadRequest("Cart object is null");
            }

            try
            {
                _cartLogic.AddCart(cart);
                return CreatedAtAction(nameof(Get), new { id = cart.CartId }, cart);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add cart: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cart updatedCart)
        {
           try
           {
               _cartLogic.UpdateCart(updatedCart);
               return NoContent();
           }
           catch(Exception ex)
           {
               return NotFound($"Failed to update cart with ID {id}: {ex.Message}");
           }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _cartLogic.DeleteCart(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Failed to delete cart with ID {id}: {ex.Message}");
            }
        }
    }
}
