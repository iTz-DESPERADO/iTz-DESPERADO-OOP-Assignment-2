namespace SrpLab.SubscriptionBilling;

public sealed class LedgerJournalLineExporter
{
    public string Export(
        string customerId,
        string invoiceNumber,
        decimal amount)
    {
        return $"{customerId}," +
               $"{invoiceNumber}," +
               $"{amount:0.00}," +
               "AR-SUB";
    }
}
