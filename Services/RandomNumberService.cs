using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services.Interfaces;
using System.Security.Cryptography;

namespace ProvaPub.Services
{
    public class RandomNumberService : IRandomNumberService
    {
        private readonly IRandomNumberRepository _repository;
        public RandomNumberService(IRandomNumberRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> GetRandomAsync(CancellationToken cancellationToken)
        {
            var number = GenerateRandomNumber();

            while (ExistNumber(number, cancellationToken))
                number = GenerateRandomNumber();

            await _repository.AddAsync(new RandomNumber() { Number = number }, cancellationToken);

            return number;
        }
        private bool ExistNumber(int number, CancellationToken cancellationToken) => _repository
            .Query(cancellationToken)
            .AsNoTracking()
            .Any(x => x.Number == number);
        private int GenerateRandomNumber()
        {
            byte[] bytes = new byte[4];
            RandomNumberGenerator.Fill(bytes);
            int value = BitConverter.ToInt32(bytes, 0) & int.MaxValue;
            return value;
        }
    }
}