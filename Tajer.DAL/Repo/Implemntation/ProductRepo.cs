using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tajer.DAL.Data;
using Tajer.DAL.Repo.Interfaces;

namespace Tajer.DAL.Repo.Implemntation
{
    public class ProductRepo : GenaricRepo<Product, int>, IProductRepo
    {
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllWithCategory(Expression<Func<Product, bool>>? Cond = null)
        {
            var Products = _context.Products.Include(p => p.Category);
            if (Cond != null)
            {
                return await Products.Where(Cond).ToListAsync();
            }
            else
            {
                return await Products.ToListAsync();
            }
        }

        public async Task<Product> GetByIdWithCategoryAndReview(int id)
        {
            var product = _context.Products.Where(p => p.Id == id).Include(p => p.Category).Include(p => p.Reviews);

            var result = await product.FirstOrDefaultAsync();
            if (result == null)
            {
                throw new Exception("Product not found");
            }
            else
            {
                return result;

            }
        }
    }
}
