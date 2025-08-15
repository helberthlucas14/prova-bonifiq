using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TestDbContext Context;
        protected readonly DbSet<T> DbSet;

        public Repository(TestDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>(); ;
        }

        public IQueryable<T> Query(CancellationToken cancellationToken = default) => DbSet.AsQueryable();
        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id, cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }

    }
}
