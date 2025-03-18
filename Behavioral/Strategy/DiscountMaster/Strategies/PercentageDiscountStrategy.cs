namespace DiscountMaster.Strategies;

public class PercentageDiscountStrategy : IDiscountStrategy
{
    private readonly decimal _percentage;

    public PercentageDiscountStrategy(decimal percentage)
    {
        _percentage = percentage;
    }

    public decimal ApplyDiscount(decimal amount)
    {
        return amount - (amount * _percentage / 100);
    }
}
