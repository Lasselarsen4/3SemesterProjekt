// ProductController.cs
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            // Logic to retrieve and display a list of products
            return View();
        }

        // Add more action methods as needed for product-related functionality
    }
}