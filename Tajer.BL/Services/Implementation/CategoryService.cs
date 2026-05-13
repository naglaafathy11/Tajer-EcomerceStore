using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tajer.BL.DTO;
using Tajer.BL.Services.Interfaces;
using Tajer.DAL.Models;
using Tajer.DAL.Repo.Interfaces;

namespace Tajer.BL.Services.Implementation
{
    public class CategoryService(IUnitOfWork _unit, IMapper _mapper) : ICategoryService
    {
        #region Add
        public async Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO)
        {
            // 1. Validate Input
            if (categoryDTO is null)
                throw new Exception("Category data is required.");
            if (string.IsNullOrWhiteSpace(categoryDTO.Name))
                throw new Exception("Category name is required.");


            // 2. Get Repos
            var Repo = _unit.GetRepo<Category, int>();



            // 4. Check Duplicate (Business Rule)
            var categories = await Repo.GetAll();
            bool isDublicate = categories.Any(c => c.Name.ToLower() == categoryDTO.Name.ToLower());
            if (isDublicate)
                throw new Exception("A Category with the same name already exists.");


            // 5. Map DTO → Entity
            var Category = _mapper.Map<Category>(categoryDTO);

            // 7. Add
            await Repo.Add(Category);
            // 8. Save
            await _unit.SaveAsync();
            // 9. Return Result
            return _mapper.Map<CategoryDTO>(Category);

        }

        #endregion
        #region Delete
        public async Task Delete(int id)
        {
            var Repo = _unit.GetRepo<Category, int>();
            var Category = await Repo.GetById(id);
            if (Category == null)
                throw new Exception("Category Not Found");
            else

                Repo.Delete(Category.Id);
            await _unit.SaveAsync();
        }
        #endregion

        #region GetAll
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var Repo = _unit.GetRepo<Category, int>();
            var Categories = await Repo.GetAll(c => c.IsActive);

            return _mapper.Map<IEnumerable<CategoryDTO>>(Categories);


        }

        public async Task<IEnumerable<CategoryDTO>> GetAllForAdminAsync()
        {
            var Repo = _unit.GetRepo<Category, int>();
            var Categories = await Repo.GetAll();
            return _mapper.Map<IEnumerable<CategoryDTO>>(Categories);
        }

        #endregion
        #region GetById
        public async Task<CategoryDTO> GetByIdAsync(int id)
        {
            var Repo = _unit.GetRepo<Category, int>();
            var Category = await Repo.GetById(id);
            if (Category == null)
                throw new Exception("Category Not Found");

            return _mapper.Map<CategoryDTO>(Category);
        }

        #endregion
        #region Toggle
        public async Task<bool> ToggleActiveAsync(CategoryDTO categoryDTO)
        {
            var Repo = _unit.GetRepo<Category, int>();

            var category = await Repo.GetById(categoryDTO.Id);
            if (category == null)
                throw new Exception("Category Not Found");
            else
            {
                category.IsActive = !category.IsActive;
                Repo.Update(category);

                return await _unit.SaveAsync() > 0;
            }

        }

        #endregion
        #region Update
        public async Task Update(int id)
        {
            var Repo = _unit.GetRepo<Category, int>();
            var Category = await Repo.GetById(id);
            if (Category == null)
                throw new Exception("Category Not Found");
            else
            {
                Repo.Update(Category);
                await _unit.SaveAsync();

            }
        } 
        #endregion  
    }
}
