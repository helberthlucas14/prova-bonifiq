using ProvaPub.Dtos;
using ProvaPub.Extensions;
using ProvaPub.Models;
using ProvaPub.Models.Enum;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services.Interfaces;
using ProvaPub.Services.Strategy;

namespace ProvaPub.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderPaymentService _orderPayment;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        public OrderService(
            IOrderPaymentService paymentFactory,
            IOrderRepository repository,
            ICustomerRepository customerRepository)
        {
            _orderPayment = paymentFactory;
            _orderRepository = repository;
            _customerRepository = customerRepository;
        }

        public async Task<OrderDto> PayOrderAsync(PaymentMethod paymentMethod, decimal paymentValue, int customerId, CancellationToken cancellationToken)
        {
            var newOrder = new Order(paymentValue, customerId);

            var payment = _orderPayment.PayOrderAsync(newOrder, paymentMethod)
                ?? throw new NotSupportedException($"Payment method {paymentMethod} is not supported.");

            return await InsertOrderAsync(newOrder, cancellationToken);
        }

        public async Task<OrderDto> InsertOrderAsync(Order order, CancellationToken cancellation)
        {
            var customer = await _customerRepository.GetByIdCustomerWithOrder(order.CustomerId, cancellation);

            await _orderRepository.AddAsync(order, cancellation);
            var orderSaved = await _orderRepository.GetByIdAsync(order.Id, cancellation)
                ?? throw new InvalidOperationException($"Order with Id {order.Id} could not be found after insertion.");

            return new OrderDto
            {
                Id = orderSaved.Id,
                OrderDate = orderSaved.OrderDate.ToBrasiliaTime(),
                CustomerId = orderSaved.CustomerId
            };
        }
    }
}
