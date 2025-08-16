using ProvaPub.Dtos;
using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface ICustomerService : IDomainService<Customer>
    {
        Task<PagedList<Customer>> PaginedListAsync(CancellationToken cancellationToken, QueryStringParameters parameters);
        Task<bool> CanPurchaseAsync(int customerId, decimal purchaseValue, CancellationToken cancellationToken);
    }
}
