using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface ICustomerService : IDomainService<Customer>
    {
        Task<PagedList<Customer>> PaginedListAsync(CancellationToken cancellationToken, int page, int pageSize = 10);
        Task<bool> CanPurchaseAsync(int customerId, decimal purchaseValue, CancellationToken cancellationToken);
    }
}
