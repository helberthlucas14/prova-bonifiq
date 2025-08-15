using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<Product>> PaginedListAsync(int page, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return await PagedList<Product>.ToPagedListAsync(_repository.Query(), page, pageSize, cancellationToken);
        }
    }
}
