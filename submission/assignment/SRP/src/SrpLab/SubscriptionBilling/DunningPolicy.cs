namespace SrpLab.SubscriptionBilling;

public sealed class DunningPolicy
{
    public DunningLevel GetLevel(int failedPayments)
    {
        return failedPayments switch
        {
            <= 1 => DunningLevel.FriendlyReminder,
            2 => DunningLevel.SecondNotice,
            _ => DunningLevel.FinalNotice
        };
    }
}
