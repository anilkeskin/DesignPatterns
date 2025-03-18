namespace DiscountMaster.Strategies;

public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal price);
}
