using ProvaPub.Models;
using ProvaPub.Models.Enum;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services.Strategy
{
    public class PixPayment : IPaymentStrategy
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public PixPayment(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public Task<Order> ProcessPaymentAsync(Order order, PaymentMethod paymentMethod)
        {
            if (paymentMethod != PaymentMethod.Pix)
            {
                throw new NotSupportedException($"Payment method {paymentMethod} is not supported by PixPayment strategy.");
            }

            return Task.FromResult(new Order(order.Value, order.CustomerId, _dateTimeProvider.UtcNow));
        }
    }
}
