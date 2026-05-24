using Microsoft.AspNetCore.Mvc;
using Tajer.BL.DTO;
using Tajer.BL.Services.Interfaces;

namespace Tajer.Web.Controllers
{
    public class CategoriesController(ICategoryService _categoryService) : Controller
    {
        // ─── Index ───────────────────────────────────────────────────────────────
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _categoryService.GetAllForAdminAsync();
                return View(categories);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(Enumerable.Empty<CategoryDTO>());
            }
        }

        // ─── Details ─────────────────────────────────────────────────────────────
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // ─── Create ───────────────────────────────────────────────────────────────
        public IActionResult Create() => View(new CategoryDTO());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO model, IFormFile? imageFile)
        {
            ModelState.Remove("ImageUrl");      // ← ضيف ده

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                if (imageFile is { Length: > 0 })
                    model.ImageUrl = await SaveImageAsync(imageFile);

                var created = await _categoryService.CreateAsync(model);
                TempData["Success"] = $"Category \"{created.Name}\" created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        // ─── Edit ─────────────────────────────────────────────────────────────────
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryDTO model, IFormFile? imageFile)
        {

            ModelState.Remove("ImageUrl");      // ← ضيف ده

            if (id != model.Id)
                return BadRequest("ID mismatch.");

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                if (imageFile is { Length: > 0 })
                    model.ImageUrl = await SaveImageAsync(imageFile);

                await _categoryService.UpdateAsync(id, model);
                TempData["Success"] = $"Category \"{model.Name}\" updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        // ─── Delete ───────────────────────────────────────────────────────────────
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _categoryService.Delete(id);
                TempData["Success"] = "Category deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // ─── Toggle ───────────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                var toggled = await _categoryService.ToggleActiveAsync(category);
                TempData["Success"] = toggled
                    ? $"Category \"{category.Name}\" is now active."
                    : $"Category \"{category.Name}\" is now inactive.";
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
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "categories");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/images/categories/{uniqueName}";
        }
    }
}