using ProvaPub.Dtos;
using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IProductService : IDomainService<Product>
    {
        Task<PagedList<Product>> PaginedListAsync(CancellationToken cancellationToken, QueryStringParameters parameters);
    }
}
