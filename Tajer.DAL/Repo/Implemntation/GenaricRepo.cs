using Microsoft.EntityFrameworkCore;
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
    public class GenaricRepo<T,TK>(AppDbContext context) : IGenaricRepo<T, TK> where T : BaseEntity<TK>  
    {
        private readonly AppDbContext _context = context;

        public async Task Add(T Entity)
        {
            await  _context.Set<T>().AddAsync(Entity);
        }

        public void Delete(TK id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(TK id)
        { 
            return await _context.Set<T>().FindAsync(id);
        }

        

        public void Update(T Entity)
        {
            _context.Set<T>().Update(Entity);
        }
    }
}
