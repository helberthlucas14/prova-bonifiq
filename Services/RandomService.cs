using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using System.Security.Cryptography;

namespace ProvaPub.Services
{
    public class RandomService
    {
        TestDbContext _ctx;
        public RandomService()
        {
            var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Teste;Trusted_Connection=True;")
    .Options;
            _ctx = new TestDbContext(contextOptions);
        }
        public async Task<int> GetRandom()
        {
            var number = GenerateRandomNumber();

            while (ExistNumber(number))
                number = GenerateRandomNumber();

            _ctx.Numbers.Add(new RandomNumber() { Number = number });
            _ctx.SaveChanges();
            return number;
        }
        private bool ExistNumber(int number) => _ctx.Numbers.Any(x => x.Number == number);
        private int GenerateRandomNumber()
        {
            byte[] bytes = new byte[4];
            RandomNumberGenerator.Fill(bytes);
            int value = BitConverter.ToInt32(bytes, 0) & int.MaxValue;
            return value;
        }
    }
}