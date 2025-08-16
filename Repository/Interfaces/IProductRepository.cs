using ProvaPub.Dtos;
using ProvaPub.Models;

namespace ProvaPub.Repository.Interfaces
{
    public interface IPagedListRepository<T> where T : class
    {
        Task<PagedList<T>> GetPagedListAsync(QueryStringParameters parameters, CancellationToken cancellationToken);
    }
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query(CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(T entity, CancellationToken cancellationToken);
    }

    public interface IProductRepository : IRepository<Product>, IPagedListRepository<Product>
    {
    }

    public interface ICustomerRepository : IRepository<Customer>, IPagedListRepository<Customer>
    {
        Task<Customer?> GetByIdCustomerWithOrder(int id, CancellationToken cancellationToken);
    }
}
