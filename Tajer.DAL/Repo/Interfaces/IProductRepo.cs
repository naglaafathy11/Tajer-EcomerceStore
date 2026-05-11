using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
namespace Tajer.DAL.Repo.Interfaces
{
    public interface IProductRepo:IGenaricRepo<Product,int>
    {
        Task<IEnumerable<Product>> GetAllWithCategory(Expression<Func<Product, bool>>? Cond = null);
        Task<Product> GetByIdWithCategoryAndReview(int id);
    

}
}
