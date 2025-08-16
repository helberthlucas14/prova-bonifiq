using ProvaPub.Extensions;
using ProvaPub.Models;

namespace ProvaPub.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        //public Customer Customer { get; set; }
        public OrderDto()
        {

        }
        public OrderDto(int id, decimal value, int customerId, DateTime orderDate)
        {
            Id = id;
            Value = value;
            CustomerId = customerId;
            OrderDate = orderDate.ToBrasiliaTime();
        }
    }
}
