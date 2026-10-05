namespace SrpLab.CheckoutBasket;

public sealed class CouponDiscountPolicy
{
    public decimal Calculate(string? couponText, decimal subTotal)
    {
        if (string.IsNullOrWhiteSpace(couponText))
            return 0m;

        var text = couponText.Trim().ToUpperInvariant();

        if (text.StartsWith("SAVE") &&
            int.TryParse(text[4..], out var pct) &&
            pct is > 0 and <= 50)
        {
            return Math.Round(subTotal * pct / 100m, 2);
        }

        if (text.Contains("FREESHIP"))
            return 0m;

        if (text == "WELCOME10")
            return Math.Min(10m, subTotal);

        return 0m;
    }
}
