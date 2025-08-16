using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IProductService : IDomainService<Product>
    {
        Task<PagedList<Product>> PaginedListAsync(CancellationToken cancellationToken, int page, int pageSize = 10);
    }
}
