using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tajer.DAL.Models;

namespace Tajer.DAL.Repo.Interfaces
{
    public interface IUnitOfWork
    {
        IGenaricRepo<T, TK> GetRepo<T, TK>() where T : BaseEntity<TK>;

        Task<int> SaveAsync();

        IProductRepo ProductRepo { get; }
    }
}
