using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebshopApplication.ServiceLayer;

namespace WebshopApplication.Views.Product
{
    public class Create : PageModel
    {
        private readonly ProductService _productService;

        public Create(ProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public WebshopApplication.Models.Product Product { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _productService.SaveProduct(Product);
                return RedirectToPage("/Product/Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}