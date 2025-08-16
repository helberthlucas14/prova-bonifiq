using ProvaPub.Models;
using ProvaPub.Models.Enum;
using ProvaPub.Services.Strategy;

namespace ProvaPub.Services
{
    public class OrderPaymentService : IOrderPaymentService
    {
        private readonly IPaymentStrategyFactory _factory;

        public OrderPaymentService(IPaymentStrategyFactory factory)
        {
            _factory = factory;
        }

        public async Task<Order> PayOrderAsync(Order order, PaymentMethod method)
        {
            var strategy = _factory.Create(method);
            return await strategy.ProcessPaymentAsync(order, method);
        }
    }
}
