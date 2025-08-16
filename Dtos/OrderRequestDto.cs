using FluentValidation;
using ProvaPub.Models.Enum;

namespace ProvaPub.Dtos
{
    public class OrderRequestDto
    {
        public decimal Value { get; set; }
        public int CustomerId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderRequestDto()
        {
        }
        public OrderRequestDto(decimal value, int customerId, PaymentMethod paymentMethod)
        {
            Value = value;
            CustomerId = customerId;
            PaymentMethod = paymentMethod;
        }
    }
}
