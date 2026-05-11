using Microsoft.AspNetCore.Mvc;
using Tajer.BL.DTO;
using Tajer.BL.Services.Interfaces;

namespace Tajer.PL.Controllers
{
    public class ProductController(IProductService _productService) : Controller
    {
        // GetAllProducts
        public async Task<IActionResult> Index()
        {
            var product = await _productService.GetAllAsync();
            return View(product);

        }


        //GetAllProductsByAdmin

        public async Task<IActionResult> GetAllProductsByAdmin()
        {
            var product = await _productService.GetAllForAdminAsync();
            return View(product);
        }


        //GetProductById

        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        //Create
        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return View(productDto);

            await _productService.CreateAsync(productDto);

            return RedirectToAction(nameof(Index));
        }
    }
}
