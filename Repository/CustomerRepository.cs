using Microsoft.EntityFrameworkCore;
using ProvaPub.Dtos;
using ProvaPub.Exceptions;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(TestDbContext context) : base(context)
        {
        }

        public Task<Customer> GetByIdCustomerWithOrder(int id, CancellationToken cancellationToken)
        {
            var customer = DbSet.AsNoTracking()
                .Include(c => c.Orders)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);

            NotFoundException.ThrowIfNull(customer, $"Customer '{id}' not found.");
            return Task.FromResult<Customer>(customer);
        }

        public async Task<PagedList<Customer>> GetPagedListAsync(QueryStringParameters parameters, CancellationToken cancellationToken)
        {
            var query = DbSet.AsNoTracking()
                 .AsQueryable();

            return await PagedList<Customer>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize, cancellationToken);
        }
    }
}
