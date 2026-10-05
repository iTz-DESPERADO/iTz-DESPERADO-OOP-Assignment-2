namespace SrpLab.LoanDesk;

public sealed class RequiredDocumentPolicy
{
    public IReadOnlyList<string> GetRequiredDocuments(
        LoanApplication application,
        bool isEligible)
    {
        var documents = new List<string>
        {
            "National ID",
            "Proof of income (3 months)"
        };

        if (application.RequestedAmount > 40_000m)
            documents.Add("Bank statements (6 months)");

        if (application.HasCollateral)
            documents.Add("Collateral ownership deed");

        if (application.EmploymentMonths < 12)
            documents.Add("Employer letter");

        if (!isEligible)
            documents.Add("Manual underwriter referral form");

        return documents;
    }
}
