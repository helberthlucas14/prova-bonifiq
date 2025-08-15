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
        public OrderService(IOrderPaymentService paymentFactory, IOrderRepository repository, ICustomerRepository customerRepository)
        {
            _orderPayment = paymentFactory;
            _orderRepository = repository;
            _customerRepository = customerRepository;
        }

        public async Task<OrderDto> PayOrderAsync(PaymentMethod paymentMethod, decimal paymentValue, int customerId)
        {
            var newOrder = new Order(paymentValue, customerId);

            var payment = _orderPayment.PayOrderAsync(newOrder, paymentMethod)
                ?? throw new NotSupportedException($"Payment method {paymentMethod} is not supported.");

            var order = await InsertOrderAsync(newOrder);

            return new OrderDto(order.Id, order.Value, order.CustomerId, order.OrderDate, order.Customer);
        }

        public async Task<Order> InsertOrderAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
            var orderSaved = await _orderRepository.GetByIdAsync(order.Id, true);
            return orderSaved ?? throw new InvalidOperationException($"Order with Id {order.Id} could not be found after insertion.");
        }

        public Task<PagedList<Order>> PaginedListAsync(int page, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return PagedList<Order>.ToPagedListAsync(
                 _orderRepository.Query(cancellationToken),
                 page,
                 pageSize,
                 cancellationToken);
        }
    }
}
