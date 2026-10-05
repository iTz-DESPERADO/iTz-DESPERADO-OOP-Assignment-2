namespace SrpLab.LoanDesk;

public sealed class LoanRiskCalculator
{
    public decimal Calculate(LoanApplication application)
    {
        decimal score = 100m;

        score -= Math.Max(0, 700 - application.CreditScore) * 0.15m;

        if (application.EmploymentMonths < 6)
            score -= 20m;

        if (application.RequestedAmount > 50_000m &&
            !application.HasCollateral)
        {
            score -= 25m;
        }

        if (application.RequestedAmount > 150_000m)
            score -= 10m;

        return Math.Clamp(score, 0m, 100m);
    }
}
