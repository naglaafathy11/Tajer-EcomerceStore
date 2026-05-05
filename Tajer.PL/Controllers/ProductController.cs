using Microsoft.AspNetCore.Mvc;
using Tajer.BL.DTO;
using Tajer.BL.Services.Interfaces;

namespace Tajer.PL.Controllers
{
    public class ProductController(IProductService _productService) : Controller
    {

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            return View(products);
        }


        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                 _productService.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
