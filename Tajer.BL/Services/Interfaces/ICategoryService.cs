using Tajer.BL.DTO;

namespace Tajer.BL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<IEnumerable<CategoryDTO>> GetAllForAdminAsync();
        Task<CategoryDTO> GetByIdAsync(int id);
        Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO);
        Task<CategoryDTO> UpdateAsync(int id, CategoryDTO categoryDTO);
        Task Delete(int id);
        Task<bool> ToggleActiveAsync(CategoryDTO categoryDTO);
    }
}