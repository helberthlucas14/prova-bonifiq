using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(TestDbContext context) : base(context)
        {
        }
    }
}
