using System;
using System.Collections.Generic;
using System.Text;
using Tajer.BL.DTO;

namespace Tajer.BL.Services.Interfaces
{
    public interface ICategoryService
    {

        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<IEnumerable<CategoryDTO>> GetAllForAdminAsync();
        Task<CategoryDTO> GetByIdAsync(int id);
        Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO);
        Task Update(int id);
        Task Delete(int id);
        Task<bool> ToggleActiveAsync(CategoryDTO categoryDTO);

    }
}
