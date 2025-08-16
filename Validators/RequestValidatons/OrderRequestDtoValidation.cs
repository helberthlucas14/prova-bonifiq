using FluentValidation;
using ProvaPub.Dtos;
using ProvaPub.Models.Enum;

namespace ProvaPub.Validators.RequestValidatons
{
    public class OrderRequestDtoValidation : AbstractValidator<OrderRequestDto>
    {
        public OrderRequestDtoValidation()
        {
            RuleFor(x => x.PaymentMethod)
                    .IsInEnum()
                    .WithMessage("Método de pagamento inválido. Use: " +
                     string.Join(", ", Enum.GetNames(typeof(PaymentMethod))));

            RuleFor(x => x.Value)
                .GreaterThan(0).WithMessage("O valor do pedido deve ser maior que zero.");

            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("O Id do cliente deve ser maior que zero.");
        }
    }
}
