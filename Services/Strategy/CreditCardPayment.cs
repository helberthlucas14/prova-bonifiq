using ProvaPub.Models;
using ProvaPub.Models.Enum;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services.Strategy
{
    public class CreditCardPayment : IPaymentStrategy
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreditCardPayment(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public Task<Order> ProcessPaymentAsync(Order order, PaymentMethod paymentMethod)
        {
            if (paymentMethod != PaymentMethod.CreditCard)
                throw new NotSupportedException($"Payment method {paymentMethod} is not supported by CreditCardPayment strategy.");

            return Task.FromResult(new Order(order.Value, order.CustomerId, _dateTimeProvider.UtcNow));
        }
    }
}
