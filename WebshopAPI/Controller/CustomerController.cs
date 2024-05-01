using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;

namespace WebshopAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly List<Customer> _customers;
        public CustomerController()
        {
            _customers = new List<Customer>();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(_customers);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = _customers.Find(c => c.CustomerId == id);
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
            
            customer.CustomerId = _customers.Count + 1;

            _customers.Add(customer);

            return CreatedAtAction(nameof(Get), new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer updatedCustomer)
        {
            var existingCustomer = _customers.Find(c => c.CustomerId == id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.Address = updatedCustomer.Address;
            existingCustomer.Phone = updatedCustomer.Phone;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customerToRemove = _customers.Find(c => c.CustomerId == id);
            if (customerToRemove == null)
            {
                return NotFound();
            }
            _customers.Remove(customerToRemove);
            return NoContent();
        }
    }
}
