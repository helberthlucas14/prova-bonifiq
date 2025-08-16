using ProvaPub.Dtos;
using ProvaPub.Models;
using ProvaPub.Models.Enum;

namespace ProvaPub.Services.Interfaces
{
    public interface IOrderService : IDomainService<Order>
    {
        Task<OrderDto> PayOrderAsync(PaymentMethod paymentMethod, decimal paymentValue, int customerId, CancellationToken cancellationToken);
        Task<OrderDto> InsertOrderAsync(Order order, CancellationToken cancellationToken);
    }
}
