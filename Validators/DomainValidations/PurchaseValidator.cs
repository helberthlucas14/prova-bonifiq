using ProvaPub.Models;
public class PurchaseValidator : Validator
{
    private readonly Customer _customer;
    private readonly decimal _firstMaximumPurchaseValue = 100m;
    private decimal PurchaseValue { get; }
    private readonly DateTime _now;

    public PurchaseValidator(
                            Customer customer,
                            ValidationHandler handler,
                            decimal purchaseValue,
                            DateTime now
        )
        : base(handler)
    {
        _customer = customer;
        PurchaseValue = purchaseValue;
        _now = now;
    }

    public override void Validate()
    {
        if (IsOutsideBusinessHoursOrDays())
            _handler.HandleError("Purchases can only be made during business hours (Mon–Fri, 8am–6pm).");

        if (HasAlreadyPurchasedThisMonth())
            _handler.HandleError("A customer can only make one purchase per month.");

        if (IsFirstPurchaseAboveLimit())
            _handler.HandleError($"First purchase cannot exceed {_firstMaximumPurchaseValue}");
    }

    private bool IsOutsideBusinessHoursOrDays()
    {
        var now = _now;
        return now.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday || now.Hour < 8 || now.Hour > 18;
    }

    private bool HasAlreadyPurchasedThisMonth()
    {
        var baseDate = _now.AddMonths(-1);
        return _customer.Orders.Any(o => o.OrderDate >= baseDate);
    }

    private bool IsFirstPurchaseAboveLimit()
    {
        var haveBoughtBefore = _customer.Orders.Count();
        return haveBoughtBefore == 0 && PurchaseValue > _firstMaximumPurchaseValue;
    }
}
