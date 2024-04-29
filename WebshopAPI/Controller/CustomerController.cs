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
        private readonly List<Customer> _customers = new List<Customer>
        {
            new Customer("John Doe", "john@example.com", "123 Main St", 123456789),
            new Customer("Jane Smith", "jane@example.com", "456 Elm St", 987654321)
        };

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(_customers);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = _customers.Find(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            // In a real application, you might validate the customer data before adding it
            _customers.Add(customer);
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer updatedCustomer)
        {
            var existingCustomer = _customers.Find(c => c.Id == id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            // Update the existing customer properties
            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.Address = updatedCustomer.Address;
            existingCustomer.Phone = updatedCustomer.Phone;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customerToRemove = _customers.Find(c => c.Id == id);
            if (customerToRemove == null)
            {
                return NotFound();
            }
            _customers.Remove(customerToRemove);
            return NoContent();
        }
    }
}
