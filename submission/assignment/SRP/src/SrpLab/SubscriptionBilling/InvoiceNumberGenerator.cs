namespace SrpLab.SubscriptionBilling;

public sealed class InvoiceNumberGenerator
{
    private static int _invoiceSequence = 1000;

    public string Next(DateOnly periodStart)
    {
        var number = ++_invoiceSequence;

        return $"INV-{periodStart:yyyyMM}-{number:D5}";
    }
}
