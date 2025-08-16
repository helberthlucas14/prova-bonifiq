using ProvaPub.Extensions;
using ProvaPub.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    public class OrderReponseDto
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentMethod { get; set; }
        public OrderReponseDto()
        {

        }
        public OrderReponseDto(int id, decimal value, int customerId, DateTime orderDate, string paymentMethod)
        {
            Id = id;
            Value = value;
            CustomerId = customerId;
            OrderDate = orderDate.ToBrasiliaTime();
            PaymentMethod = paymentMethod;
        }
    }
}
