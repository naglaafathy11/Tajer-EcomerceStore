using Tajer.DAL.Data;
using Tajer.DAL.Repo.Interfaces;

namespace Tajer.DAL.Repo.Implemntation
{
    public class UnitOfWork(AppDbContext _context) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repos = new Dictionary<string, object>();

        public IProductRepo ProductRepo => new ProductRepo(_context);

        public ICartRepo CartRepo => new CartRepo(_context);

        public IOrderRepo orderRepo => new OrderRepo(_context);

        public IGenaricRepo<T, TK> GetRepo<T, TK>() where T : BaseEntity<TK>
        {
            var type = typeof(T).Name;
            if (_repos.ContainsKey(type))
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

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
