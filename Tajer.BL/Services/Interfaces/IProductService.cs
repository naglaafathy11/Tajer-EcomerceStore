using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tajer.BL.DTO;
using Tajer.DAL.Models;

namespace Tajer.BL.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<IEnumerable<ProductDTO>> GetAllForAdminAsync();
        Task<ProductDTO> GetByIdAsync(int id);
        Task<ProductDTO> CreateAsync(ProductDTO productDto);
         Task Update (int id);
        Task Delete(int id);
        Task<bool> ToggleActiveAsync(ProductDTO product);

    }
}
