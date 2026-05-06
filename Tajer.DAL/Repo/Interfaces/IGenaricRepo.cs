using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tajer.DAL.Models;

namespace Tajer.DAL.Repo.Interfaces
{
    public interface IGenaricRepo<T ,TK> where T : BaseEntity<TK>
    {
        Task<IEnumerable<T>> GetAll(Expression< Func<T, bool>>?cond = null);
        Task<T?> GetById(TK id);
        Task Add(T Entity);
        void Update(T Entity);
        void Delete(TK id); 




    }
}
