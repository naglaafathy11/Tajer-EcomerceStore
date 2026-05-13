using System.Linq.Expressions;
using Tajer.DAL.Data;
using Tajer.DAL.Repo.Interfaces;

namespace Tajer.DAL.Repo.Implemntation
{
    public class CartRepo : GenaricRepo<ShoppingCartItem, int>, ICartRepo
    {
        private readonly AppDbContext _dbContext;


        public CartRepo(AppDbContext context) : base(context)
        {
             _dbContext = context;
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetAllWithProduct(Expression<Func<ShoppingCartItem, bool>>? cond = null)
        {

            if (cond != null)
            {
                var cartitemWithproduct = _dbContext.ShoppingCartItems.Where(cond).Include(p => p.Product);
                return await cartitemWithproduct.ToListAsync();

            }
            else
            {
                var cartitemWithproduct = _dbContext.ShoppingCartItems.Include(p => p.Product);
                return await cartitemWithproduct.ToListAsync();




            }

        }
    }
}
