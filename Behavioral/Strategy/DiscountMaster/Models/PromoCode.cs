using DiscountMaster.Strategies;

namespace DiscountMaster.Models;

public class PromoCode
{
    public string Code { get; set; }
    public IDiscountStrategy DiscountStrategy { get; set; }

    public PromoCode(string code, IDiscountStrategy discountStrategy)
    {
        Code = code;
        DiscountStrategy = discountStrategy;
    }
}
