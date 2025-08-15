using ProvaPub.Models;

namespace ProvaPub.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query(CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
    }
    public interface IProductRepository : IRepository<Product>
    {
    }

    public interface ICustomerRepository : IRepository<Customer>
    {
    }

    public interface IOrderRepository : IRepository<Order>
    {
        public Task<Order?> GetByIdAsync(int id, bool includeCustomer, CancellationToken cancellationToken = default);
    }
}
