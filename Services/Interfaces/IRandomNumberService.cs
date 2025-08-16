using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IRandomNumberService : IDomainService<RandomNumberService>
    {
        Task<int> GetRandomAsync(CancellationToken cancellationToken);
    }
}
