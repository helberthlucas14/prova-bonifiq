namespace ProvaPub.Models
{
    public class Order : Entity
    {
        public decimal Value { get; private set; }
        public int CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public Customer Customer { get; private set; }
        public Order()
        {
        }
        public Order(decimal value, int customerId, DateTime orderDate)
        {
            Value = value;
            CustomerId = customerId;
            OrderDate = orderDate;
        }

        public Order(decimal value, int customerId)
        {
            Value = value;
            CustomerId = customerId;
        }
    }
}
