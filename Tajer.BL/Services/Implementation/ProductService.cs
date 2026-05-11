using AutoMapper;
using Tajer.BL.DTO;
using Tajer.BL.Services.Interfaces;
using Tajer.DAL.Models;
using Tajer.DAL.Repo.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Tajer.BL.Services.Implementation
{
    public class ProductService(IUnitOfWork _unit, IMapper _mapper) : IProductService
    {
        #region Add Product
        public async Task<ProductDTO> CreateAsync(ProductDTO productDto)
        {
            // 1. Validate Input
            if (productDto is null)
                throw new Exception("Product data is required.");
            if (string.IsNullOrWhiteSpace(productDto.Name))
                throw new Exception("Product name is required.");
            if (productDto.Price <= 0)
                throw new Exception("Product price must be greater than zero.");

            // 2. Get Repos
            var ProductRepo = _unit.GetRepo<Product, int>();
            var CategoryRepo = _unit.GetRepo<Category, int>();
            // 3. Check Category Exists
            var category = await CategoryRepo.GetById(productDto.CategoryId);
            if (category is null)
                throw new Exception("Category not found.");

            // 4. Check Duplicate (Business Rule)
            var products = await ProductRepo.GetAll();
            bool isDublicate = products.Any(p => p.Name.ToLower() == productDto.Name.ToLower());
            if (isDublicate)
                throw new Exception("A product with the same name already exists.");


            // 5. Map DTO → Entity
            var productEntity = _mapper.Map<Product>(productDto);
            // 6. Apply Business Logic of Date
            productEntity.CreatedAt = DateTime.UtcNow;

            // 7. Add
            await ProductRepo.Add(productEntity);
            // 8. Save
            await _unit.SaveAsync();
            // 9. Return Result
            return _mapper.Map<ProductDTO>(productEntity);
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            var ProductRepo = _unit.GetRepo<Product, int>();
            var product = await ProductRepo.GetById(id);
            if (product is null)
                throw new Exception("Product not found.");
            else
            {
                ProductRepo.Delete(id);
                await _unit.SaveAsync();
            }
        }

        #endregion

        #region Update
        public async Task Update(int id)
        {
            var ProductRepo = _unit.GetRepo<Product, int>();
            var product = await ProductRepo.GetById(id);
            if (product is null)
                throw new Exception("Product not found.");
            else
            {
                ProductRepo.Update(product);
                product.UpdatedAt = DateTime.UtcNow;
                await _unit.SaveAsync();
            }
        }
        #endregion


        #region GetAll
        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var ProductRepo = _unit.ProductRepo;

            var product = await ProductRepo.GetAllWithCategory(p => p.IsActive);
            return _mapper.Map<IEnumerable<ProductDTO>>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllForAdminAsync()
        {
            var ProductRepo = _unit.ProductRepo;

            var product = await ProductRepo.GetAllWithCategory();
            return _mapper.Map<IEnumerable<ProductDTO>>(product);
        }

        #endregion


        #region GetById
        public async Task<ProductDTO> GetByIdAsync(int id)
        {

            var repo = _unit.ProductRepo;

            var product = await repo.GetByIdWithCategoryAndReview(id);
            if (product is null)
                throw new Exception("Product Not Found");
            else

                return _mapper.Map<ProductDTO>(product);
        }

        #endregion


        #region  Toggle
        public async Task<bool> ToggleActiveAsync(ProductDTO productdto)
        {
            var productRepo = _unit.GetRepo<Product, int>();

            var product = await productRepo.GetById(productdto.Id);
            if (product is null)
                throw new Exception("Product Not Found");



            product.IsActive = !product.IsActive;

            productRepo.Update(product);

            product.UpdatedAt = DateTime.UtcNow;
            ;

            return await _unit.SaveAsync() > 0;







        }

        #endregion

    }
}
