using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tajer.BL.DTO;
using Tajer.BL.Services.Interfaces;
using Tajer.DAL.Models;
using Tajer.DAL.Repo.Interfaces;

namespace Tajer.BL.Services.Implementation
{
    public class ProductService(IUnitOfWork _unit , IMapper _mapper) : IProductService
    {
        public async Task<ProductDTO> CreateAsync(ProductDTO productDto)
        {
            // 1. Validate Input
            if(productDto is null)
                throw new Exception("Product data is required.");
            if(string.IsNullOrWhiteSpace(productDto.Name))
                throw new Exception("Product name is required.");
            if(productDto.Price <= 0)
                throw new Exception("Product price must be greater than zero.");

            // 2. Get Repos
            var ProductRepo = _unit.GetRepo<Product, int>();
            var CategoryRepo = _unit.GetRepo<Category, int>();
            // 3. Check Category Exists
            var  category = CategoryRepo.GetById(productDto.CategoryId) ;
            if(category is null)
                throw new Exception("Category not found.");

            // 4. Check Duplicate (Business Rule)
            var products = await ProductRepo.GetAll();
            bool isDublicate = products.Any(p => p.Name.ToLower() == productDto.Name.ToLower());
            if(isDublicate)
                throw new Exception("A product with the same name already exists.");


            // 5. Map DTO → Entity
            var productEntity = _mapper.Map<Product>(productDto);
            // 6. Apply Business Logic of Date
            productEntity.CreatedAt = DateTime.UtcNow;

            // 7. Add
            await  ProductRepo.Add(productEntity);
            // 8. Save
             await _unit.Save();
            // 9. Return Result
            return _mapper.Map<ProductDTO>(productEntity);
        }


        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var ProductRepo = _unit.GetRepo<Product, int>();
            var products =await  ProductRepo.GetAll(x=>x.IsActive);
            var result = products.Select(p => new ProductDTO
            {
                Name = p.Name,
                Price = p.Price,
                DiscountPrice = p.DiscountPrice,
                CategoryId = p.CategoryId,
                //CategoryName= p.Category.Name,

            });

            return result;

        }
        public void DeleteAsync(ProductDTO productDto)
        {
            throw new NotImplementedException();
        }

       

        public Task<IEnumerable<ProductDTO>> GetAllForAdminAsync()
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetAverageRatingAsync(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetRelatedProductsAsync(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ToggleActiveAsync(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(ProductDTO productDto)
        {
            throw new NotImplementedException();
        }
    }
}
