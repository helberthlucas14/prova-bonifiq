using ProvaPub.Models;

namespace ProvaPub.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query(CancellationToken cancellationToken = default);
    }
    public interface IProductRepository : IRepository<Product>
    {
    }

    public interface ICustomerRepository : IRepository<Customer>
    {
    }
}
