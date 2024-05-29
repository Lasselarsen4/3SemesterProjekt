using Microsoft.AspNetCore.Mvc;
using WebshopApplication.Models;
using WebshopApplication.ServiceLayer;

namespace WebshopApplication.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IConfiguration configuration)
        {
            _productService = new ProductService(configuration);
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(string sortParam)
        {
            var products = await _productService.GetProducts(sortParam);
            return View(products);
        }
        
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (await _productService.SaveProduct(product))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }
        
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Product ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                if (await _productService.UpdateProduct(product))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _productService.DeleteProduct(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest($"Failed to delete product with ID {id}.");
        }
    }
}
