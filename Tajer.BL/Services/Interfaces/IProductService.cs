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
        Task <IEnumerable<ProductDTO>> GetAllProducts();
        Task <ProductDTO> GetProductById(int id);
        void AddProduct(ProductDTO productdto);
        void UpdateProduct(ProductDTO productdto);
        void DeleteProduct(int id);

    }
}
