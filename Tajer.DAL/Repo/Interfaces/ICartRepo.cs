using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Tajer.DAL.Repo.Interfaces
{
    public interface ICartRepo :IGenaricRepo<ShoppingCartItem , int>
    {
        Task<IEnumerable<ShoppingCartItem>> GetAllWithProduct(Expression< Func<ShoppingCartItem , bool>>?cond  = null);

    }
}
