using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<Product>> PaginedListAsync(CancellationToken cancellationToken, int page, int pageSize = 10)
        {
            return await PagedList<Product>.ToPagedListAsync(_repository.Query(cancellationToken), page, pageSize, cancellationToken);
        }
    }
}
