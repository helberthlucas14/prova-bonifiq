using FluentAssertions;
using Moq;
using ProvaPub.Exceptions;
using ProvaPub.Repository;
using Xunit;

namespace ProvaPub.Tests
{

    [Collection(nameof(CustomerRepositoryTestsFixture))]
    public class CustomerRepositoryTests
    {
        private readonly CustomerRepositoryTestsFixture _fixture;
        public CustomerRepositoryTests(CustomerRepositoryTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(Should_Return_Customer_When_GetByIdCustomerWithOrder_NotFound))]
        [Trait("Repostitory", "Repostitory - Customer")]
        public async void Should_Return_Customer_When_GetByIdCustomerWithOrder_NotFound()
        {
            // Arrange
            var dateProvider = new FakeDateTimeProvider { UtcNow = new DateTime(2025, 8, 12, 10, 0, 0) };
            var exampleCustomers = _fixture.GetValidCustomersWithOrdersList(DateTime.UtcNow, custumerLength: 10);
            var exampleCustomer = exampleCustomers[3];
            var context = _fixture.CreateDbContext();
            var repository = new CustomerRepository(context);

            // Act
            Func<Task> task = async () => await repository.GetByIdCustomerWithOrder(
                exampleCustomer.Id,
                CancellationToken.None);

            // Assert
            await task.Should()
             .ThrowAsync<NotFoundException>()
             .WithMessage($"Customer '{exampleCustomer.Id}' not found.");
        }

        [Fact(DisplayName = nameof(Should_Return_Customer_When_GetByIdCustomerWithOrder_Found))]
        [Trait("Repostitory", "Repostitory - Customer")]
        public async void Should_Return_Customer_When_GetByIdCustomerWithOrder_Found()
        {
            // Arrange
            var dateProvider = new FakeDateTimeProvider { UtcNow = new DateTime(2025, 8, 12, 10, 0, 0) };
            var exampleCustomers = _fixture.GetValidCustomersWithOrdersList(DateTime.UtcNow, custumerLength: 10);
            var exampleCustomer = exampleCustomers[3];

            var context = _fixture.CreateDbContext(true);
            await context.Customers.AddRangeAsync(exampleCustomers);
            await context.SaveChangesAsync();
            var repository = new CustomerRepository(context);

            // Act
            var result = await repository.GetByIdCustomerWithOrder(exampleCustomer.Id, CancellationToken.None);
            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(exampleCustomer.Id);
            result.Name.Should().Be(exampleCustomer.Name);
            result.Orders.Should().HaveSameCount(exampleCustomer.Orders);
            result.Orders.ToList().ForEach(item =>
            {
                var example = exampleCustomer.Orders.FirstOrDefault(x => x.Id == item.Id);
                example.Should().NotBeNull();
                example.Id.Should().Be(item.Id);
                example.CustomerId.Should().Be(item.CustomerId);
                example.Value.Should().Be(item.Value);
                example.OrderDate.Should().Be(item.OrderDate);
            });
        }
    }

}
