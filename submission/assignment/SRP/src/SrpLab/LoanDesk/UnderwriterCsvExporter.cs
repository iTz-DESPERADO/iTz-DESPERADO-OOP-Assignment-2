namespace SrpLab.LoanDesk;

public sealed class UnderwriterCsvExporter
{
    public string Export(
        string applicationId,
        LoanApplication application,
        decimal riskScore,
        bool isEligible)
    {
        return $"{applicationId}," +
               $"{application.CreditScore}," +
               $"{application.EmploymentMonths}," +
               $"{(application.HasCollateral ? 1 : 0)}," +
               $"{riskScore:0.00}," +
               $"{(isEligible ? "Y" : "N")}";
    }
}
