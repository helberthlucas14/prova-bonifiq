using ProvaPub.Models;
using ProvaPub.Models.Enum;

namespace ProvaPub.Services.Strategy
{
    public interface IPaymentStrategy
    {
        Task<Order> ProcessPaymentAsync(Order order, PaymentMethod paymentMethod);
    }
}
