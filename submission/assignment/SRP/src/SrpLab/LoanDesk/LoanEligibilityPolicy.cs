namespace SrpLab.LoanDesk;

public sealed class LoanEligibilityPolicy
{
    public bool IsEligible(
        LoanApplication application,
        decimal riskScore)
    {
        return riskScore >= 55m &&
               application.CreditScore >= 580;
    }
}
