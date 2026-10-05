namespace SrpLab.SubscriptionBilling;

public sealed class ProrationCalculator
{
    public decimal Calculate(
        SubscriptionAccount account,
        DateOnly activeFrom)
    {
        if (activeFrom <= account.PeriodStart)
            return account.MonthlyPrice;

        if (activeFrom >= account.PeriodEnd)
            return 0m;

        var totalDays =
            account.PeriodEnd.DayNumber -
            account.PeriodStart.DayNumber;

        if (totalDays <= 0)
            return account.MonthlyPrice;

        var used =
            account.PeriodEnd.DayNumber -
            activeFrom.DayNumber;

        return Math.Round(
            account.MonthlyPrice * used / totalDays,
            2);
    }
}
