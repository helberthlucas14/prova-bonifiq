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

        public async Task<OrderReponseDto> PayOrderAsync(OrderRequestDto orderRequest, CancellationToken cancellationToken)
        {
            var orderCustomer = new Order(orderRequest.Value, orderRequest.CustomerId);

            var newOrder = await _orderPayment.PayOrderAsync(orderCustomer, orderRequest.PaymentMethod);

            var result = await InsertOrderAsync(newOrder, cancellationToken);

            var response = new OrderReponseDto
            {
                Id = result.Id,
                OrderDate = result.OrderDate.ToBrasiliaTime(),
                CustomerId = result.CustomerId,
                Value = result.Value,
                PaymentMethod = orderRequest.PaymentMethod.ToString(),
            };

            return response;
        }

        public async Task<Order> InsertOrderAsync(Order order, CancellationToken cancellation)
        {
            var customer = await _customerRepository.GetByIdCustomerWithOrder(order.CustomerId, cancellation);

            await _orderRepository.AddAsync(order, cancellation);

            var orderSaved = await _orderRepository.GetByIdAsync(order.Id, cancellation)
                ?? throw new InvalidOperationException($"Order with Id {order.Id} could not be found after insertion.");

            return orderSaved;
        }
    }
}
