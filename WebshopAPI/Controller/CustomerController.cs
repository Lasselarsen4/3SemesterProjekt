using Microsoft.AspNetCore.Mvc;
using ModelAPI;
using WebshopAPI.BusinessLogicLayer;
using System.Collections.Generic;
using System;

namespace WebshopAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerLogic _customerLogic;

        public CustomerController(ICustomerLogic customerLogic)
        {
            _customerLogic = customerLogic;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            var customers = _customerLogic.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = _customerLogic.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer object is null");
            }

            try
            {
                _customerLogic.AddCustomer(customer);
                return CreatedAtAction(nameof(Get), new { id = customer.CustomerId }, customer);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add customer: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer updatedCustomer)
        {
           try
           {
               _customerLogic.UpdateCustomer(updatedCustomer);
               return NoContent();
           }
           catch(Exception ex)
           {
               return NotFound ($"Failed to update customer with ID {id}: {ex.Message}");
           }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _customerLogic.DeleteCustomer(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Failed to delete product with ID {id}: {ex.Message}");
            }
        }
    }
}
