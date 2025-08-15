using ProvaPub.Models.Enum;

namespace ProvaPub.Services.Strategy
{

    public class PaymentStrategyFactory : IPaymentStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentStrategy Create(PaymentMethod method)
        {
            return method switch
            {
                PaymentMethod.Pix => _serviceProvider.GetRequiredService<PixPayment>(),
                PaymentMethod.CreditCard => _serviceProvider.GetRequiredService<CreditCardPayment>(),
                PaymentMethod.PayPal => _serviceProvider.GetRequiredService<PayPalPayment>(),
                _ => throw new NotSupportedException($"Método {method} não suportado")
            };
        }
    }
}
