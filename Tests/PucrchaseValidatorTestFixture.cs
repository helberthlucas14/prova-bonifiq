using ProvaPub.Models;
using Xunit;

namespace ProvaPub.Tests
{

    [CollectionDefinition(nameof(PucrchaseValidatorTestFixture))]
    public class PucrchaseValidatorTestFixtureCollection : ICollectionFixture<PucrchaseValidatorTestFixture> { }
    public class PucrchaseValidatorTestFixture
    {
        public List<Order> GetRandomOrders(int customerId, int length, DateTime dateTime)
        {
            if (length <= 0) return new List<Order>();

            return Enumerable.Range(1, length)
                .Select(i =>
                {
                    var target = dateTime.AddMonths(-(i * 1));

                    var day = Math.Min(dateTime.Day, DateTime.DaysInMonth(target.Year, target.Month));

                    var orderDate = new DateTime(
                        target.Year,
                        target.Month,
                        day,
                        dateTime.Hour,
                        0,
                        0,
                        DateTimeKind.Utc
                    );
                    return new Order(10, customerId, orderDate);
                })
                .ToList();
        }
    }
}

