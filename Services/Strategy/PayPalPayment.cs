using ProvaPub.Models;
using ProvaPub.Models.Enum;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services.Strategy
{
    public class PayPalPayment : IPaymentStrategy
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public PayPalPayment(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public Task<Order> ProcessPaymentAsync(Order order, PaymentMethod paymentMethod)
        {
            if (paymentMethod != PaymentMethod.PayPal)
            {
                throw new NotSupportedException($"Payment method {paymentMethod} is not supported by PayPalPayment strategy.");
            }
            return Task.FromResult(new Order(order.Value, order.CustomerId, _dateTimeProvider.UtcNow));
        }
    }
}
