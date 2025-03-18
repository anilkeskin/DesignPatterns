using DiscountMaster.Managers;
using DiscountMaster.Strategies;

namespace DiscountMaster.Models;

public class Cart
{
    private readonly List<Product> _products;
    private IDiscountStrategy _discountStrategy;
    private PromoCodeManager _promoCodeManager;

    public Cart(PromoCodeManager promoCodeManager)
    {
        _products = new List<Product>();
        _promoCodeManager = promoCodeManager;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public void ApplyPromoCode(string promoCode)
    {
        var discountStrategy = _promoCodeManager.GetDiscountStrategy(promoCode);
        if (discountStrategy != null)
        {
            _discountStrategy = discountStrategy;
        }
    }

    public decimal GetTotalAmount()
    {
        decimal totalAmount = 0;
        foreach (var product in _products)
        {
            totalAmount += product.Price;
        }

        if (_discountStrategy != null)
        {
            totalAmount = _discountStrategy.ApplyDiscount(totalAmount);
        }

        return totalAmount;
    }
}