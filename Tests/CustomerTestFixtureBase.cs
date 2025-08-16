using Bogus;
using ProvaPub.Models;

namespace ProvaPub.Tests
{
    public class CustomerTestFixtureBase
    {
        protected Faker Faker { get; set; }
        public CustomerTestFixtureBase()
        {
            Faker = new Faker("pt_BR");
        }

        public List<Customer> GetValidCustomersWithOrdersList(DateTime dateTime, int custumerLength = 10, int orderLength = 3)
        {
            return Enumerable.Range(1, custumerLength)
                .Select(i => new Customer
                {
                    Id = i,
                    Name = Faker.Name.FullName(),
                    Orders = GetValidOrders(i, orderLength, dateTime)
                }).ToList();
        }

        private List<Order> GetValidOrders(int customerId, int length, DateTime dateTime)
        {
            if (length <= 0) return new List<Order>();

            return Enumerable.Range(1, length)
                .Select(i =>
                {
                    var target = dateTime.AddMonths(-(i * 2));

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

                    return new Order(GeneratePurchaseValue(), customerId, orderDate);
                })
                .ToList();
        }
        private decimal GeneratePurchaseValue() => Faker.Random.Decimal(1, 99);
    }
}
