using DiscountMaster.Models;
using DiscountMaster.Strategies;

namespace DiscountMaster.Managers;

public class PromoCodeManager
{
    private readonly Dictionary<string, PromoCode> _promoCodes;

    public PromoCodeManager()
    {
        _promoCodes = new Dictionary<string, PromoCode>();
    }
    public void AddPromoCode(PromoCode promoCode)
    {
        _promoCodes[promoCode.Code] = promoCode;
    }

    public IDiscountStrategy GetDiscountStrategy(string code)
    {
        if (_promoCodes.ContainsKey(code))
        {
            return _promoCodes[code].DiscountStrategy;
        }

        return null;
    }
}