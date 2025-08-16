using Microsoft.EntityFrameworkCore;
using ProvaPub.Dtos;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;
using System.Threading;

namespace ProvaPub.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(TestDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Product>> GetPagedListAsync(QueryStringParameters parameters, CancellationToken cancellationToken)
        {
            var query = DbSet.AsNoTracking()
                             .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                             .Take(parameters.PageSize)
                             .AsQueryable();

            return await PagedList<Product>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize, cancellationToken);
        }
    }
}
