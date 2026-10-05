namespace SrpLab.SubscriptionBilling;

public sealed class SubscriptionBilling
{
    private readonly SubscriptionAccount _account;
    private readonly ProrationCalculator _prorationCalculator = new();
    private readonly InvoiceNumberGenerator _invoiceNumberGenerator = new();
    private readonly DunningPolicy _dunningPolicy = new();
    private readonly DunningEmailFormatter _dunningEmailFormatter = new();
    private readonly LedgerJournalLineExporter _journalExporter = new();

    public string CustomerId => _account.CustomerId;
    public decimal MonthlyPrice => _account.MonthlyPrice;
    public DateOnly PeriodStart => _account.PeriodStart;
    public DateOnly PeriodEnd => _account.PeriodEnd;
    public int FailedPayments => _account.FailedPayments;

    public SubscriptionBilling(
        string customerId,
        decimal monthlyPrice,
        DateOnly periodStart,
        DateOnly periodEnd)
    {
        _account = new SubscriptionAccount(
            customerId,
            monthlyPrice,
            periodStart,
            periodEnd);
    }

    public decimal Prorate(DateOnly activeFrom)
        => _prorationCalculator.Calculate(_account, activeFrom);

    public string NextInvoiceNumber()
        => _invoiceNumberGenerator.Next(PeriodStart);

    public void RegisterFailedPayment()
        => _account.RegisterFailedPayment();

    public string DunningEmail(
        string customerName,
        DateOnly asOf)
    {
        var amount = Prorate(PeriodStart);
        var invoiceNumber = NextInvoiceNumber();

        return _dunningEmailFormatter.Format(
            customerName,
            asOf,
            amount,
            invoiceNumber,
            FailedPayments,
            _dunningPolicy.GetLevel(FailedPayments));
    }

    public string LedgerJournalLine(DateOnly activeFrom)
    {
        var invoiceNumber = NextInvoiceNumber();
        var amount = Prorate(activeFrom);

        return _journalExporter.Export(
            CustomerId,
            invoiceNumber,
            amount);
    }
}
