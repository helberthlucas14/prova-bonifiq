using Bogus;
using FluentAssertions;
using ProvaPub.Models;
using ProvaPub.Validators.DomainValidations;
using System;
using Xunit;

namespace ProvaPub.Tests
{
    [Collection(nameof(PucrchaseValidatorTestFixture))]
    public class PurchaseValidatorTests
    {
        private readonly PucrchaseValidatorTestFixture _fixture;
        public PurchaseValidatorTests(PucrchaseValidatorTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory(DisplayName = nameof(Should_AddError_When_PurchaseOutsideBusinessHours))]
        [Trait("Validator", "Validator - Purchase")]
        [MemberData(nameof(GetRandomDayAndHourInvalidValid), 50)]
        public void Should_AddError_When_PurchaseOutsideBusinessHours(DayOfWeek dayOfWeek, int hour)
        {
            // Arrange
            var purchaseDate = new DateTime(2024, 8, 1, hour, 0, 0);
            while (purchaseDate.DayOfWeek != dayOfWeek)
                purchaseDate = purchaseDate.AddDays(1);

            var customer = new Customer { Orders = new List<Order>() };
            var handler = new NotificationValidationHandler();
            var validator = new PurchaseValidator(customer, handler, 50, purchaseDate);

            // Act
            validator.Validate();

            // Assert
            handler.HasErrors().Should().BeTrue();

            handler.Errors.Should()
              .BeEquivalentTo(new List<ValidationError>()
              {
                new ValidationError("Purchases can only be made during business hours (Mon–Fri, 8am–6pm)."),
              });
        }

        [Theory(DisplayName = nameof(Should_AddError_When_CustomerAlreadyPurchasedThisMonth))]
        [Trait("Validator", "Validator - Purchase")]
        [InlineData(1, 10, 2)]
        [InlineData(2, 25, 6)]
        [InlineData(3, 80, 10)]
        public void Should_AddError_When_CustomerAlreadyPurchasedThisMonth(int customerId, decimal purchaseValue, int orderListLength)
        {
            // Arrange
            var dateProvider = new FakeDateTimeProvider { UtcNow = new DateTime(2024, 8, 1, 8, 0, 0) };
            var ordersInDateRange = _fixture.GetRandomOrders(customerId, orderListLength, dateProvider.UtcNow);
            var customer = new Customer();

            for (int i = 0; i < ordersInDateRange.Count; i++)
                customer.Orders.Add(ordersInDateRange[i]);

            var handler = new NotificationValidationHandler();
            var validator = new PurchaseValidator(customer, handler, purchaseValue, dateProvider.UtcNow);

            // Act
            Action action = () => validator.Validate();

            validator.Validate();

            // Assert
            handler.HasErrors().Should().BeTrue();

            handler.Errors.Should()
              .BeEquivalentTo(new List<ValidationError>()
              {
                new ValidationError("A customer can only make one purchase per month."),
              });
        }

        [Theory(DisplayName = nameof(Should_AddError_When_FirstPurchaseAboveLimit))]
        [Trait("Validator", "Validator - Purchase")]
        [MemberData(nameof(GetPurchaseOver100), 10, 500)]

        public void Should_AddError_When_FirstPurchaseAboveLimit(decimal purchaseValue)
        {
            // Arrange
            var dateProvider = new FakeDateTimeProvider { UtcNow = new DateTime(2025, 8, 12, 10, 0, 0) };
            var customer = new Customer();
            var handler = new NotificationValidationHandler();
            var validator = new PurchaseValidator(customer, handler, purchaseValue, dateProvider.UtcNow);

            // Act
            Action action = () => validator.Validate();

            validator.Validate();

            // Assert
            handler.HasErrors().Should().BeTrue();

            handler.Errors.Should()
              .BeEquivalentTo(new List<ValidationError>()
              {
                new ValidationError($"First purchase cannot exceed {100}"),
              });
        }

        [Theory(DisplayName = nameof(Should_NotAddErrors_When_AllRulesPass))]
        [Trait("Validator", "Validator - Purchase")]
        [MemberData(nameof(GetRandomHourHourValid), 50)]
        public void Should_NotAddErrors_When_AllRulesPass(DayOfWeek dayOfWeek, int hour)
        {
            // Arrange
            var purchaseDate = new DateTime(2024, 8, (int)dayOfWeek, hour, 0, 0);
            while (purchaseDate.DayOfWeek == DayOfWeek.Sunday ||
                purchaseDate.DayOfWeek == DayOfWeek.Saturday)
                purchaseDate = purchaseDate.AddDays(1);

            var dateProvider = new FakeDateTimeProvider { UtcNow = purchaseDate };

            var customer = new Customer { Orders = new List<Order>() };
            var handler = new NotificationValidationHandler();
            var validator = new PurchaseValidator(customer, handler, 80, dateProvider.UtcNow);

            // Act
            validator.Validate();

            // Assert
            Assert.False(handler.HasErrors());
        }

        public static IEnumerable<object[]> GetRandomDayAndHourInvalidValid(int numberOfTests)
        {
            yield return new object[] { DayOfWeek.Monday, 7 };
            for (int i = 0; i < (numberOfTests - 1); i++)
            {
                var dayWeek = (DayOfWeek)(new Random().Next(0, 6));
                var hour = i % 2 == 0 ? new Random().Next(0, 7) : new Random().Next(19, 23);
                yield return new object[] { dayWeek, hour };
            }
        }

        public static IEnumerable<object[]> GetRandomHourHourValid(int numberOfTests)
        {
            yield return new object[] { DayOfWeek.Monday, 8 };
            for (int i = 0; i < (numberOfTests - 1); i++)
            {
                var dayWeek = (DayOfWeek)(new Random().Next(1, 6));
                var hour = new Random().Next(9, 17);
                yield return new object[] { dayWeek, hour };
            }
        }
        public static IEnumerable<object[]> GetPurchaseOver100(int numberOfTests, int maxRange)
        {
            Random random = new Random();
            yield return new object[] { 101m };
            for (int i = 0; i < (numberOfTests - 1); i++)
            {
                decimal purchase = random.Next(101, maxRange) + (decimal)random.NextDouble();
                yield return new object[] { purchase };
            }
        }


    }
}

