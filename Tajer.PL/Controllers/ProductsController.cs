using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tajer.BL.DTO;
using Tajer.BL.Services.Interfaces;

namespace Tajer.Web.Controllers
{
    public class ProductsController(IProductService _productService, ICategoryService _categoryService) : Controller
    {
        // ─── helpers ────────────────────────────────────────────────────────────
        private async Task PopulateCategoriesAsync(int? selectedId = null)
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedId);
        }

        // ─── Index ───────────────────────────────────────────────────────────────
        // GET: /Products
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _productService.GetAllForAdminAsync();
                return View(products);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(Enumerable.Empty<ProductDTO>());
            }
        }

        // ─── Details ─────────────────────────────────────────────────────────────
        // GET: /Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ─── Create ───────────────────────────────────────────────────────────────
        // GET: /Products/Create
        public async Task<IActionResult> Create()
        {
            await PopulateCategoriesAsync();
            return View(new ProductDTO());
        }

        // POST: /Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesAsync(model.CategoryId);
                return View(model);
            }

            try
            {
                // Handle image upload
                if (imageFile is { Length: > 0 })
                    model.ImageUrl = await SaveImageAsync(imageFile);

                var created = await _productService.CreateAsync(model);
                TempData["Success"] = $"Product \"{created.Name}\" created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateCategoriesAsync(model.CategoryId);
                return View(model);
            }
        }

        // ─── Edit ─────────────────────────────────────────────────────────────────
        // GET: /Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                await PopulateCategoriesAsync(product.CategoryId);
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: /Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDTO model, IFormFile? imageFile)
        {
            if (id != model.Id)
                return BadRequest("ID mismatch.");

            if (!ModelState.IsValid)
            {
                await PopulateCategoriesAsync(model.CategoryId);
                return View(model);
            }

            try
            {
                if (imageFile is { Length: > 0 })
                    model.ImageUrl = await SaveImageAsync(imageFile);

                await _productService.UpdateAsync(id, model);
                TempData["Success"] = $"Product \"{model.Name}\" updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateCategoriesAsync(model.CategoryId);
                return View(model);
            }
        }

        // ─── Delete ───────────────────────────────────────────────────────────────
        // GET: /Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: /Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _productService.Delete(id);
                TempData["Success"] = "Product deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // ─── Toggle Active ────────────────────────────────────────────────────────
        // POST: /Products/Toggle/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                var toggled = await _productService.ToggleActiveAsync(product);
                TempData["Success"] = toggled
                    ? $"Product \"{product.Name}\" is now active."
                    : $"Product \"{product.Name}\" is now inactive.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // ─── Private helpers ──────────────────────────────────────────────────────
        private async Task<string> SaveImageAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/images/products/{uniqueName}";
        }
    }
}