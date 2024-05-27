using Microsoft.AspNetCore.Mvc;
using WebshopApplication.Models;
using WebshopApplication.BusinessLogicLayerWeb;

namespace WebshopApplication.Controllers
{
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomerLogic _customerLogic;

        public CustomerController(IConfiguration configuration)
        {
            _customerLogic = new CustomerLogic(configuration);
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(string sortParam)
        {
            var customers = await _customerLogic.GetCustomers(sortParam);
            return View(customers);
        }
        
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerLogic.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (await _customerLogic.InsertCustomer(customer))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(customer);
        }
        
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerLogic.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest("Customer ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                if (await _customerLogic.UpdateCustomer(customer))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(customer);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerLogic.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _customerLogic.DeleteCustomer(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest($"Failed to delete customer with ID {id}.");
        }
    }
}
