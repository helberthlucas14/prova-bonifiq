using Microsoft.EntityFrameworkCore;
using ProvaPub.Exceptions;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Repository.Interfaces;

public abstract class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly TestDbContext Context;
    protected readonly DbSet<T> DbSet;

    public Repository(TestDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public IQueryable<T> Query(CancellationToken cancellationToken = default)
    {
        return DbSet.AsNoTracking();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await DbSet.AsNoTracking()
                              .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        NotFoundException.ThrowIfNull(entity, $"{typeof(T)} '{id}' not found.");
        return entity;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }
}