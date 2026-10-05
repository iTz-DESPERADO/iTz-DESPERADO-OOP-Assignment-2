namespace SrpLab.CheckoutBasket;

public sealed class BasketState
{
    private readonly List<BasketLine> _lines = new();

    public IReadOnlyList<BasketLine> Lines => _lines;
    public string? CouponText { get; private set; }
    public bool GiftWrapEnabled { get; private set; }

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0)
            throw new ArgumentOutOfRangeException(nameof(qty));

        _lines.Add(new BasketLine(sku, price, qty));
    }

    public void ApplyCoupon(string? couponText)
        => CouponText = couponText;

    public void EnableGiftWrap()
        => GiftWrapEnabled = true;
}
