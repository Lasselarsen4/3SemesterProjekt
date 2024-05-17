// ProductController.cs
using Microsoft.AspNetCore.Mvc;
using ModelAPI;
using System.Collections.Generic;
using System.Linq;
using WebshopAPI.BusinessLogicLayer;

namespace WebshopAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductLogic _productLogic;

        public ProductController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _productLogic.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _productLogic.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product object is null");
            }

            _productLogic.AddProduct(product);

            return CreatedAtAction(nameof(Get), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product updatedProduct)
        {
            try
            {
                _productLogic.UpdateProduct(updatedProduct);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Failed to update product with ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productLogic.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound($"Failed to delete product with ID {id}: {ex.Message}");
            }      
        }
    }
}
