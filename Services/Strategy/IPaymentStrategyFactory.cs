using ProvaPub.Models;
using ProvaPub.Models.Enum;

namespace ProvaPub.Services.Strategy
{
    public interface IPaymentStrategyFactory
    {
        IPaymentStrategy Create(PaymentMethod method);
    }

    public interface IOrderPaymentService
    {
        Task<Order> PayOrderAsync(Order order, PaymentMethod method);
    }
}
