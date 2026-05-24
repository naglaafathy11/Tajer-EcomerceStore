using AutoMapper;
using Tajer.BL.DTO;
using Tajer.BL.Services.Interfaces;
using Tajer.DAL.Models;
using Tajer.DAL.Repo.Interfaces;

namespace Tajer.BL.Services.Implementation
{
    public class CategoryService(IUnitOfWork _unit, IMapper _mapper) : ICategoryService
    {
        #region Create
        public async Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
                throw new Exception("Category data is required.");
            if (string.IsNullOrWhiteSpace(categoryDTO.Name))
                throw new Exception("Category name is required.");

            var repo = _unit.GetRepo<Category, int>();

            var all = await repo.GetAll();
            bool isDuplicate = all.Any(c => c.Name.Equals(categoryDTO.Name, StringComparison.OrdinalIgnoreCase));
            if (isDuplicate)
                throw new Exception("A category with the same name already exists.");

            var entity = _mapper.Map<Category>(categoryDTO);
            entity.CreatedAt = DateTime.UtcNow;

            await repo.Add(entity);
            await _unit.SaveAsync();

            return _mapper.Map<CategoryDTO>(entity);
        }
        #endregion

        #region Update
        public async Task<CategoryDTO> UpdateAsync(int id, CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
                throw new Exception("Category data is required.");
            if (string.IsNullOrWhiteSpace(categoryDTO.Name))
                throw new Exception("Category name is required.");

            var repo = _unit.GetRepo<Category, int>();

            var category = await repo.GetById(id);
            if (category is null)
                throw new Exception("Category not found.");

            var all = await repo.GetAll();
            bool isDuplicate = all.Any(c =>
                c.Id != id &&
                c.Name.Equals(categoryDTO.Name, StringComparison.OrdinalIgnoreCase));
            if (isDuplicate)
                throw new Exception("Another category with the same name already exists.");

            _mapper.Map(categoryDTO, category);
            category.UpdatedAt = DateTime.UtcNow;

            repo.Update(category);
            await _unit.SaveAsync();

            return _mapper.Map<CategoryDTO>(category);
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            var repo = _unit.GetRepo<Category, int>();
            var category = await repo.GetById(id);
            if (category is null)
                throw new Exception("Category not found.");

            repo.Delete(category.Id);
            await _unit.SaveAsync();
        }
        #endregion

        #region GetAll
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var repo = _unit.GetRepo<Category, int>();
            var categories = await repo.GetAll(c => c.IsActive);
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllForAdminAsync()
        {
            var repo = _unit.GetRepo<Category, int>();
            var categories = await repo.GetAll();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
        #endregion

        #region GetById
        public async Task<CategoryDTO> GetByIdAsync(int id)
        {
            var repo = _unit.GetRepo<Category, int>();
            var category = await repo.GetById(id);
            if (category is null)
                throw new Exception("Category not found.");

            return _mapper.Map<CategoryDTO>(category);
        }
        #endregion

        #region Toggle
        public async Task<bool> ToggleActiveAsync(CategoryDTO categoryDTO)
        {
            var repo = _unit.GetRepo<Category, int>();
            var category = await repo.GetById(categoryDTO.Id);
            if (category is null)
                throw new Exception("Category not found.");

            category.IsActive = !category.IsActive;
            category.UpdatedAt = DateTime.UtcNow;

            repo.Update(category);
            return await _unit.SaveAsync() > 0;
        }
        #endregion
    }
}