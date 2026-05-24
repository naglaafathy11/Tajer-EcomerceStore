using AutoMapper;
using Tajer.BL.DTO;
using Tajer.BL.Services.Interfaces;
using Tajer.DAL.Models;
using Tajer.DAL.Repo.Interfaces;

namespace Tajer.BL.Services.Implementation
{
    public class ProductService(IUnitOfWork _unit, IMapper _mapper) : IProductService
    {
        #region Create
        public async Task<ProductDTO> CreateAsync(ProductDTO productDto)
        {
            if (productDto is null)
                throw new Exception("Product data is required.");
            if (string.IsNullOrWhiteSpace(productDto.Name))
                throw new Exception("Product name is required.");
            if (productDto.Price <= 0)
                throw new Exception("Product price must be greater than zero.");

            var productRepo = _unit.GetRepo<Product, int>();
            var categoryRepo = _unit.GetRepo<Category, int>();

            var category = await categoryRepo.GetById(productDto.CategoryId);
            if (category is null)
                throw new Exception("Category not found.");

            var all = await productRepo.GetAll();
            bool isDuplicate = all.Any(p => p.Name.Equals(productDto.Name, StringComparison.OrdinalIgnoreCase));
            if (isDuplicate)
                throw new Exception("A product with the same name already exists.");

            var entity = _mapper.Map<Product>(productDto);
            entity.CreatedAt = DateTime.UtcNow;

            await productRepo.Add(entity);
            await _unit.SaveAsync();

            return _mapper.Map<ProductDTO>(entity);
        }
        #endregion

        #region Update
        public async Task<ProductDTO> UpdateAsync(int id, ProductDTO productDto)
        {
            if (productDto is null)
                throw new Exception("Product data is required.");
            if (string.IsNullOrWhiteSpace(productDto.Name))
                throw new Exception("Product name is required.");
            if (productDto.Price <= 0)
                throw new Exception("Product price must be greater than zero.");

            var productRepo = _unit.GetRepo<Product, int>();
            var categoryRepo = _unit.GetRepo<Category, int>();

            var product = await productRepo.GetById(id);
            if (product is null)
                throw new Exception("Product not found.");

            var category = await categoryRepo.GetById(productDto.CategoryId);
            if (category is null)
                throw new Exception("Category not found.");

            // Check duplicate name — exclude the current product itself
            var all = await productRepo.GetAll();
            bool isDuplicate = all.Any(p =>
                p.Id != id &&
                p.Name.Equals(productDto.Name, StringComparison.OrdinalIgnoreCase));
            if (isDuplicate)
                throw new Exception("Another product with the same name already exists.");

            // Map only the fields that should change
            _mapper.Map(productDto, product);
            product.UpdatedAt = DateTime.UtcNow;

            productRepo.Update(product);
            await _unit.SaveAsync();

            return _mapper.Map<ProductDTO>(product);
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            var productRepo = _unit.GetRepo<Product, int>();
            var product = await productRepo.GetById(id);
            if (product is null)
                throw new Exception("Product not found.");

            productRepo.Delete(product.Id);
            await _unit.SaveAsync();
        }
        #endregion

        #region GetAll
        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _unit.ProductRepo.GetAllWithCategory(p => p.IsActive);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllForAdminAsync()
        {
            var products = await _unit.ProductRepo.GetAllWithCategory();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
        #endregion

        #region GetById
        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var product = await _unit.ProductRepo.GetByIdWithCategoryAndReview(id);
            if (product is null)
                throw new Exception("Product not found.");

            return _mapper.Map<ProductDTO>(product);
        }
        #endregion

        #region Toggle Active
        public async Task<bool> ToggleActiveAsync(ProductDTO productDto)
        {
            var productRepo = _unit.GetRepo<Product, int>();
            var product = await productRepo.GetById(productDto.Id);
            if (product is null)
                throw new Exception("Product not found.");

            product.IsActive = !product.IsActive;
            product.UpdatedAt = DateTime.UtcNow;

            productRepo.Update(product);
            return await _unit.SaveAsync() > 0;
        }
        #endregion
    }
}