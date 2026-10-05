namespace SrpLab.CheckoutBasket;

public sealed class CheckoutBasket
{
    private readonly BasketState _state = new();
    private readonly BasketPricingCalculator _pricing = new();
    private readonly GiftMessageCardFormatter _giftFormatter = new();
    private readonly PaymentAuthorizationStub _paymentAuthorization = new();

    public void AddLine(string sku, decimal price, int qty)
        => _state.AddLine(sku, price, qty);

    public void ApplyCouponText(string? couponText)
        => _state.ApplyCoupon(couponText);

    public void EnableGiftWrap()
        => _state.EnableGiftWrap();

    public decimal SubTotal()
        => _pricing.SubTotal(_state);

    public decimal DiscountAmount()
        => _pricing.DiscountAmount(_state);

    public decimal GrandTotal()
        => _pricing.GrandTotal(_state);

    public string GiftMessageCard(string fromName)
        => _giftFormatter.Format(fromName, _state.Lines, GrandTotal());

    public string AuthorizePaymentStub(string cardLast4)
        => _paymentAuthorization.Authorize(
            GrandTotal(),
            cardLast4,
            _state.Lines.Count);
}
