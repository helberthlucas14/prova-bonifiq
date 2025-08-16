namespace ProvaPub.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
    }
    public class Customer : Entity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public void CanPurchase(ValidationHandler handler, decimal purchaseValue, DateTime now)
            => (new PurchaseValidator(this, handler, purchaseValue, now)).Validate();
    }
}
