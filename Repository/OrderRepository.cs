using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(TestDbContext context) : base(context)
        {
        }

        public Task<Order?> GetByIdAsync(int id, bool includeCustomer, CancellationToken cancellationToken = default)
        {
            return includeCustomer
                ? base.DbSet.Include(o => o.Customer).FirstOrDefaultAsync(o => o.Id == id, cancellationToken)
                : DbSet.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }
    }
}
