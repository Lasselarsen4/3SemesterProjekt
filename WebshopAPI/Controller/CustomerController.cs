using Microsoft.AspNetCore.Mvc;
using ModelAPI;
using WebshopAPI.BusinessLogicLayer;

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

            _customerLogic.AddCustomer(customer);

            return CreatedAtAction(nameof(Get), new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer updatedCustomer)
        {
            var existingCustomer = _customerLogic.GetCustomerById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.FirstName = updatedCustomer.FirstName;
            existingCustomer.LastName = updatedCustomer.LastName;
            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.Address = updatedCustomer.Address;
            existingCustomer.Phone = updatedCustomer.Phone;

            _customerLogic.UpdateCustomer(existingCustomer);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customerToRemove = _customerLogic.GetCustomerById(id);
            if (customerToRemove == null)
            {
                return NotFound();
            }

            _customerLogic.DeleteCustomer(id);

            return NoContent();
        }
    }
}
