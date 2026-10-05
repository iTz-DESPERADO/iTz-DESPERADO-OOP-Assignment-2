namespace SrpLab.CheckoutBasket;

public sealed class BasketPricingCalculator
{
    private readonly CouponDiscountPolicy _couponPolicy = new();
    private readonly GiftWrapPricingPolicy _giftWrapPolicy = new();

    public decimal SubTotal(BasketState basket)
        => basket.Lines.Sum(line => line.Price * line.Qty);

    public decimal DiscountAmount(BasketState basket)
        => _couponPolicy.Calculate(basket.CouponText, SubTotal(basket));

    public decimal GrandTotal(BasketState basket)
    {
        var total =
            SubTotal(basket) -
            DiscountAmount(basket) +
            _giftWrapPolicy.Fee(basket.GiftWrapEnabled);

        return Math.Max(0m, total);
    }
}
