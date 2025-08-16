using ProvaPub.Dtos;
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

        public async Task<PagedList<Product>> PaginedListAsync(CancellationToken cancellationToken, QueryStringParameters parameters)
        {
            var pagedList = await _repository.GetPagedListAsync(parameters, cancellationToken);
            return pagedList;
        }
    }
}
