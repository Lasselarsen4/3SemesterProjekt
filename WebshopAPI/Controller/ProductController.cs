using Microsoft.AspNetCore.Mvc;
using Model;


namespace WebshopAPI.Controller 
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product { productId = 1, Name = "Product 1", Price = 10.99m, Description = "Description for Product 1" },
            new Product { productId = 2, Name = "Product 2", Price = 20.49m, Description = "Description for Product 2" },
            new Product { productId = 3, Name = "Product 3", Price = 5.99m, Description = "Description for Product 3" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(_products);
            
        }
    }
}


