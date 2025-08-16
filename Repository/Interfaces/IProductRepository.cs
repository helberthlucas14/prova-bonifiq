using ProvaPub.Models;

namespace ProvaPub.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query(CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(T entity, CancellationToken cancellationToken);
    }

    public interface IRandomNumberRepository : IRepository<RandomNumber>
    {
    }

    public interface IProductRepository : IRepository<Product>
    {
    }

    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetByIdCustomerWithOrder(int id, CancellationToken cancellationToken);
    }

    public interface IOrderRepository : IRepository<Order>
    {
    }
}
