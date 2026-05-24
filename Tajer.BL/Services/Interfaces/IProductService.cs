using Tajer.BL.DTO;

namespace Tajer.BL.Services.Interfaces
{
    public interface IProductService
    {
        // Queries
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<IEnumerable<ProductDTO>> GetAllForAdminAsync();
        Task<ProductDTO> GetByIdAsync(int id);

        // Commands
        Task<ProductDTO> CreateAsync(ProductDTO productDto);
        Task<ProductDTO> UpdateAsync(int id, ProductDTO productDto);
        Task Delete(int id);
        Task<bool> ToggleActiveAsync(ProductDTO productDto);
    }
}