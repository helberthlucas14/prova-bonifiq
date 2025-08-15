using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(TestDbContext context) : base(context)
        {
        }
    }
}
