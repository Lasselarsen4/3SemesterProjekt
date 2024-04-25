using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebshopAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 10.99m },
            new Product { Id = 2, Name = "Product 2", Price = 20.49m },
            new Product { Id = 3, Name = "Product 3", Price = 5.99m }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(_products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
