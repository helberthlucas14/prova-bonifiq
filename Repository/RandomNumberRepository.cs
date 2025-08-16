using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository
{
    public class RandomNumberRepository : Repository<RandomNumber>, IRandomNumberRepository
    {
        public RandomNumberRepository(TestDbContext context) : base(context)
        {
        }
    }
}
