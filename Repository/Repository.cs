using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TestDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(TestDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>(); ;
        }

        public IQueryable<T> Query(CancellationToken cancellationToken = default) => _dbSet.AsQueryable();
    }
}
