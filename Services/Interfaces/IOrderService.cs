using ProvaPub.Dtos;
using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IOrderService : IDomainService<Order>
    {
        Task<OrderReponseDto> PayOrderAsync(OrderRequestDto orderRequest, CancellationToken cancellationToken);
        Task<Order> InsertOrderAsync(Order order, CancellationToken cancellationToken);
    }
}
