using DiscountMaster.Managers;
using DiscountMaster.Models;
using DiscountMaster.Strategies;

namespace Strategy.Test;

public class DiscountMasterTests
{
    [Fact]
    public void TestFlatDiscountStrategy()
    {
        var flatDiscount = new FlatDiscountStrategy(50);
        var amountAfterDiscount = flatDiscount.ApplyDiscount(200);

        Assert.Equal(150, amountAfterDiscount); // 200 - 50 = 150
    }

    [Fact]
    public void TestPercentageDiscountStrategy()
    {
        var percentageDiscount = new PercentageDiscountStrategy(10);
        var amountAfterDiscount = percentageDiscount.ApplyDiscount(200);

        Assert.Equal(180, amountAfterDiscount); // 200 - 10% = 180
    }

    [Fact]
    public void TestFlatDiscountWithPromoCode()
    {
        var promoCodeManager = new PromoCodeManager();
        promoCodeManager.AddPromoCode(new PromoCode("FLAT30", new FlatDiscountStrategy(30)));

        var cart = new Cart(promoCodeManager);
        cart.AddProduct(new Product("Product 1", 100));
        cart.AddProduct(new Product("Product 2", 150));

        cart.ApplyPromoCode("FLAT30");

        var totalAmount = cart.GetTotalAmount();

        Assert.Equal(220, totalAmount); // 100 + 150 - 30 = 220
    }

    [Fact]
    public void TestPercentageDiscountWithPromoCode()
    {
        var promoCodeManager = new PromoCodeManager();
        promoCodeManager.AddPromoCode(new PromoCode("SAVE10", new PercentageDiscountStrategy(10)));

        var cart = new Cart(promoCodeManager);
        cart.AddProduct(new Product("Product 1", 200));
        cart.AddProduct(new Product("Product 2", 300));

        cart.ApplyPromoCode("SAVE10");

        var totalAmount = cart.GetTotalAmount();

        Assert.Equal(450, totalAmount); // 200 + 300 - 10% = 450
    }

    [Fact]
    public void TestInvalidPromoCode()
    {
        var promoCodeManager = new PromoCodeManager();
        promoCodeManager.AddPromoCode(new PromoCode("FLAT30", new FlatDiscountStrategy(30)));

        var cart = new Cart(promoCodeManager);
        cart.AddProduct(new Product("Product 1", 100));
        cart.AddProduct(new Product("Product 2", 150));

        cart.ApplyPromoCode("INVALID");

        var totalAmount = cart.GetTotalAmount();

        Assert.Equal(250, totalAmount); // 100 + 150 = 250
    }
}