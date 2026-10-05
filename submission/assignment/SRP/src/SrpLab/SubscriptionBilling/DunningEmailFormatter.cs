namespace SrpLab.SubscriptionBilling;

public sealed class DunningEmailFormatter
{
    public string Format(
        string customerName,
        DateOnly asOf,
        decimal amount,
        string invoiceNumber,
        int failedPayments,
        DunningLevel level)
    {
        var severity = level switch
        {
            DunningLevel.FriendlyReminder => "friendly reminder",
            DunningLevel.SecondNotice => "second notice",
            DunningLevel.FinalNotice => "final notice before suspension",
            _ => throw new ArgumentOutOfRangeException(nameof(level))
        };

        return $"Subject: {severity} {invoiceNumber}\n" +
               $"Hi {customerName},\n" +
               $"Balance {amount:C} as of {asOf:o} " +
               $"({failedPayments} failures).\n";
    }
}
