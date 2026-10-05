namespace SrpLab.CheckoutBasket;

public sealed class GiftMessageCardFormatter
{
    public string Format(
        string fromName,
        IReadOnlyList<BasketLine> lines,
        decimal grandTotal)
    {
        var items = string.Join(", ", lines.Select(line => line.Sku));

        return $"Dear friend,\nA gift from {fromName} awaits ({items}).\n" +
               $"Total surprise value: {grandTotal:C}\n";
    }
}
