using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tajer.DAL.Data;
using Tajer.DAL.Models;
using Tajer.DAL.Repo.Interfaces;

namespace Tajer.DAL.Repo.Implemntation
{
    public class UnitOfWork(AppDbContext _context) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repos = new Dictionary<string, object>();
        public IGenaricRepo<T, TK> GetRepo<T, TK>() where T : BaseEntity<TK>
        {
            var type = typeof(T).Name;
            if(_repos.ContainsKey(type))
            {
                return (IGenaricRepo<T, TK>)_repos[type];
            }
            else
            {
                var repo = new GenaricRepo<T, TK>(_context);
                _repos.Add(type, repo);
                return repo;
            }

        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
