using Microsoft.AspNetCore.Mvc;
using WebshopApplication.Models;
using WebshopApplication.ServiceLayer;

namespace WebshopApplication.Controllers
{
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(IConfiguration configuration)
        {
            _customerService = new CustomerService(configuration);
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(string sortParam)
        {
            var customers = await _customerService.GetCustomers(sortParam);
            return View(customers);
        }
        
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
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
                if (await _customerService.SaveCustomer(customer))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(customer);
        }
        
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
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
                if (await _customerService.UpdateCustomer(customer))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(customer);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
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
            if (await _customerService.DeleteCustomer(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest($"Failed to delete customer with ID {id}.");
        }
    }
}
