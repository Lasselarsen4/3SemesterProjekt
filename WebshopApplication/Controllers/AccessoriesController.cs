using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebshopApplication.Models;

namespace WebshopApplication.Controllers
{
    public class AccessoriesController : Controller
    {
        private readonly ILogger<AccessoriesController> _logger;

        public AccessoriesController(ILogger<AccessoriesController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}