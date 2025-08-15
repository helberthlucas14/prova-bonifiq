using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IDomainService<T> where T : class
    {
        Task<PagedList<T>> PaginedListAsync(int page, int pageSize = 10, CancellationToken cancellationToken = default);
    }

    public interface IProductService : IDomainService<Product>
    {
    }
    public interface ICustomerService : IDomainService<Customer>
    {
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}
