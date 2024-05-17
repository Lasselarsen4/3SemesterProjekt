using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ModelAPI;
using WebshopApplication.BusinessLogicLayerWeb;
using Microsoft.Extensions.Configuration;

namespace WebshopApplication.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductLogic _productLogic;

        public ProductController(IConfiguration inConfiguration)
        {
            _productLogic = new ProductLogic(inConfiguration);
        }

        // GET: /Product
        [HttpGet]
        public async Task<IActionResult> Index(string sortParam)
        {
            var products = await _productLogic.GetProducts(sortParam);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // GET: /Product/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productLogic.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: /Product/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                if (await _productLogic.InsertProduct(product))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: /Product/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productLogic.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: /Product/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Product ID mismatch.");
            }

            try
            {
                if (await _productLogic.UpdateProduct(product))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: /Product/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productLogic.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (await _productLogic.DeleteProduct(id))
                {
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest($"Failed to delete product with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
