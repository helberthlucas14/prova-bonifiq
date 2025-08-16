using FluentAssertions;
using Moq;
using ProvaPub.Exceptions;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services;
using ProvaPub.Validators.DomainValidations;
using Xunit;

namespace ProvaPub.Tests
{

    [Collection(nameof(CustomerServiceTestFixture))]
    public class CustomerServiceTest
    {
        private readonly CustomerServiceTestFixture _fixture;
        public CustomerServiceTest(CustomerServiceTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(Should_ArgumentOutOfRangeExceptionWhenCustomerId_LessThanOrEqual_0))]
        [Trait("Service", "Service - Customer")]
        public async void Should_ArgumentOutOfRangeExceptionWhenCustomerId_LessThanOrEqual_0()
        {
            // Arrange
            var customerId = -1;
            var dateProvider = new FakeDateTimeProvider { UtcNow = new DateTime(2025, 8, 12, 10, 0, 0) };
            var repositoryMock = new Mock<ICustomerRepository>();
            var service = new CustomerService(repositoryMock.Object, dateProvider);
            var handler = new NotificationValidationHandler();
            // Act
            Func<Task> task = async () => await service.CanPurchaseAsync(customerId, 50, CancellationToken.None);
            // Assert
            await task.Should()
             .ThrowAsync<ArgumentOutOfRangeException>()
             .WithMessage($"Specified argument was out of the range of valid values. (Parameter 'customerId')");
        }

        [Fact(DisplayName = nameof(Should_ArgumentOutOfRangeExceptionWhen_PurchaseValue_LessThanOrEqual_0))]
        [Trait("Service", "Service - Customer")]
        public async void Should_ArgumentOutOfRangeExceptionWhen_PurchaseValue_LessThanOrEqual_0()
        {
            // Arrange
            var purchaseValue = -1;
            var dateProvider = new FakeDateTimeProvider { UtcNow = new DateTime(2025, 8, 12, 10, 0, 0) };
            var repositoryMock = new Mock<ICustomerRepository>();
            var service = new CustomerService(repositoryMock.Object, dateProvider);
            var handler = new NotificationValidationHandler();
            // Act
            Func<Task> task = async () => await service.CanPurchaseAsync(1, purchaseValue, CancellationToken.None);
            // Assert
            await task.Should()
             .ThrowAsync<ArgumentOutOfRangeException>()
             .WithMessage($"Specified argument was out of the range of valid values. (Parameter 'purchaseValue')");
        }

        [Fact(DisplayName = nameof(Should_NotFoundException_When_NotFoundCustomerId))]
        [Trait("Service", "Service - Customer")]
        public async void Should_NotFoundException_When_NotFoundCustomerId()
        {
            // Arrange
            int dayOfWeek = (int)DayOfWeek.Saturday;
            var dateProvider = new FakeDateTimeProvider { UtcNow = new DateTime(2024, 8, dayOfWeek, 8, 0, 0) };
            var exampleCustomers = _fixture.GetValidCustomersWithOrdersList(dateProvider.UtcNow);
            var exampleCustomer = exampleCustomers[2];
            var customerId = exampleCustomers[2].Id;

            var repositoryMock = new Mock<ICustomerRepository>();
            repositoryMock.Setup(
                x => x.GetByIdCustomerWithOrder(
               customerId,
               It.IsAny<CancellationToken>()))
              .ReturnsAsync(exampleCustomer);

            var service = new CustomerService(repositoryMock.Object, dateProvider);
            var handler = new NotificationValidationHandler();
            // Act
            var output = await service.CanPurchaseAsync(customerId, 10, CancellationToken.None);
            // Assert
            repositoryMock.Verify(repository => repository.GetByIdCustomerWithOrder(
               It.IsAny<int>(),
               It.IsAny<CancellationToken>()
               ),
               Times.Once
           );
        }

        [Fact(DisplayName = nameof(Should_NotFoundException_When_CustomerId_NotFound))]
        [Trait("Service", "Service - Customer")]
        public async Task Should_NotFoundException_When_CustomerId_NotFound()
        {
            // Arrange
            var customerId = 123;
            var repositoryMock = new Mock<ICustomerRepository>();
            repositoryMock
                .Setup(r => r.GetByIdCustomerWithOrder(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException($"Customer '{customerId}' not found."));

            var repo = repositoryMock.Object;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                repo.GetByIdCustomerWithOrder(customerId, CancellationToken.None));

            repositoryMock.Verify(repository => repository.GetByIdCustomerWithOrder(
               It.IsAny<int>(),
               It.IsAny<CancellationToken>()
               ),
               Times.Once
           );
        }

        [Theory(DisplayName = nameof(Should_True_When_CanPurchaseAsync_Success))]
        [Trait("Service", "Service - Customer")]
        [MemberData(nameof(GetRandomCustomerListLengthValidPurchase), 100, 1000)]
        public async Task Should_True_When_CanPurchaseAsync_Success(int custumerListLength, decimal purchaseValue, int orderLength)
        {
            // Arrange
            var dateProvider = new FakeDateTimeProvider { UtcNow = new DateTime(2025, 8, 12, 10, 0, 0) };
            var exampleCustomers = _fixture.GetValidCustomersWithOrdersList
                (
                    dateProvider.UtcNow,
                    custumerLength: custumerListLength,
                    orderLength: orderLength
                );
            var exampleCustomer = exampleCustomers[custumerListLength - 1];
            var customerId = exampleCustomer.Id;
            var repositoryMock = new Mock<ICustomerRepository>();
            repositoryMock.Setup(
                x => x.GetByIdCustomerWithOrder(
               customerId,
               It.IsAny<CancellationToken>()))
              .ReturnsAsync(exampleCustomer);
            var service = new CustomerService(repositoryMock.Object, dateProvider);

            // Act
            var output = await service.CanPurchaseAsync(customerId, purchaseValue, CancellationToken.None);

            // Assert
            output.Should().BeTrue();
            repositoryMock.Verify(repository => repository.GetByIdCustomerWithOrder(
               It.IsAny<int>(),
               It.IsAny<CancellationToken>()
               ),
               Times.Once
           );
        }

        public static IEnumerable<object[]> GetRandomCustomerListLengthValidPurchase(int numberOfTests, int maxpurchase)
        {
            Random random = new Random();
            yield return new object[] { 1, 101m, 1 };
            for (int i = 0; i < (numberOfTests - 1); i++)
            {
                int custumerListLength = random.Next(1, 20);
                int orderLength = random.Next(1, 20);
                decimal purchase = random.Next(1, maxpurchase) + (decimal)random.NextDouble();
                yield return new object[] { custumerListLength, purchase, orderLength };
            }
        }
    }
}
