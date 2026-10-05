namespace SrpLab.CheckoutBasket;

public sealed class GiftWrapPricingPolicy
{
    public decimal Fee(bool enabled)
        => enabled ? 4.99m : 0m;
}
