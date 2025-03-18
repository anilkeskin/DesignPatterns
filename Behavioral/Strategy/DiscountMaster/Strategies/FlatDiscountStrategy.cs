namespace DiscountMaster.Strategies;

public class FlatDiscountStrategy : IDiscountStrategy
{
    private readonly decimal _discountAmount;

    public FlatDiscountStrategy(decimal discountAmount)
    {
        _discountAmount = discountAmount;
    }

    public decimal ApplyDiscount(decimal amount)
    {
        return amount - _discountAmount;
    }
}