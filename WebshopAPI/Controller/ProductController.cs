using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace WebshopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product(1, "Product 1", 10.99m, "Description for Product 1"),
            new Product(2, "Product 2", 20.49m, "Description for Product 2"),
            new Product(3, "Product 3", 5.99m, "Description for Product 3"),
            new Supplements(4, "Creatine Supplement", 29.99m, "High-quality creatine monohydrate supplement", "Monohydrate", null, null),
            new Equipment(5, "Dumbbells", 49.99m, "Set of adjustable dumbbells for strength training", "Adjustable", null, "Rubberized"),
            new Accessories(6, "Weightlifting Straps", 9.99m, "High-quality straps for better grip during weightlifting", "Nylon", null, null)
        };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(_products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
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

            // Generate a unique ID for the new product
            product.ProductId = _products.Count + 1;
            
            _products.Add(product);
            
            return CreatedAtAction(nameof(Get), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product updatedProduct)
        {
            var productToUpdate = _products.FirstOrDefault(p => p.ProductId == id);
            if (productToUpdate == null)
            {
                return NotFound($"Product with ID {id} not found");
            }

            productToUpdate.ProductName = updatedProduct.ProductName;
            productToUpdate.ProductPrice = updatedProduct.ProductPrice;
            productToUpdate.ProductDescription = updatedProduct.ProductDescription;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var productToRemove = _products.FirstOrDefault(p => p.ProductId == id);
            if (productToRemove == null)
            {
                return NotFound($"Product with ID {id} not found");
            }

            _products.Remove(productToRemove);

            return NoContent();
        }
    }
}


