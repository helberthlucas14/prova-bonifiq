using ProvaPub.Models;
using ProvaPub.Models.Enum;

namespace ProvaPub.Services.Strategy
{
    public class PayPalPayment : IPaymentStrategy
    {
        public Task<Order> ProcessPaymentAsync(Order order, PaymentMethod paymentMethod)
        {
            if (paymentMethod != PaymentMethod.PayPal)
            {
                throw new NotSupportedException($"Payment method {paymentMethod} is not supported by PayPalPayment strategy.");
            }
            return Task.FromResult(new Order(order.Value, order.CustomerId));
        }
    }
}
