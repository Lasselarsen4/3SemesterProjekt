using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebshopApplication.Models;
using System;
using WebshopApplication.BusinessLogicLayerWeb;

namespace WebshopApplication.Views.Product
{
    public class Create : PageModel
    {
        private readonly ProductLogic _productLogic;

        public Create(ProductLogic productLogic)
        {
            _productLogic = productLogic;
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
                await _productLogic.InsertProduct(Product);
                return RedirectToPage("/Product/Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}