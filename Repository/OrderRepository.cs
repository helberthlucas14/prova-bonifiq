using Microsoft.EntityFrameworkCore;
using ProvaPub.Exceptions;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(TestDbContext context) : base(context)
        {
        }

        public async Task<Order?> GetByIdOrderWithCustomer(int id, CancellationToken cancellationToken)
        {
            var order = await DbSet
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            NotFoundException.ThrowIfNull(order, $"Order '{id}' not found.");
            return order;
        }

        public Task<PagedList<Order>> PaginedListAsync(CancellationToken cancellationToken, int page, int pageSize = 10)
        {
            throw new NotImplementedException();
        }
    }
}
