using ProvaPub.Extensions;

namespace ProvaPub.Dtos
{
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
