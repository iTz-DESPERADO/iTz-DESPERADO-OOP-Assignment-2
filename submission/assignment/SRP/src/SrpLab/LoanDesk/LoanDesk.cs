namespace SrpLab.LoanDesk;

public sealed class LoanDesk
{
    private readonly LoanApplication _application;
    private readonly LoanRiskCalculator _riskCalculator = new();
    private readonly LoanEligibilityPolicy _eligibilityPolicy = new();
    private readonly RequiredDocumentPolicy _documentPolicy = new();
    private readonly LoanDecisionLetterFormatter _letterFormatter = new();
    private readonly UnderwriterCsvExporter _csvExporter = new();

    public decimal RequestedAmount => _application.RequestedAmount;
    public int CreditScore => _application.CreditScore;
    public int EmploymentMonths => _application.EmploymentMonths;
    public bool HasCollateral => _application.HasCollateral;

    public LoanDesk(
        decimal requestedAmount,
        int creditScore,
        int employmentMonths,
        bool hasCollateral)
    {
        _application = new LoanApplication(
            requestedAmount,
            creditScore,
            employmentMonths,
            hasCollateral);
    }

    public decimal RiskScore()
        => _riskCalculator.Calculate(_application);

    public bool IsEligible()
        => _eligibilityPolicy.IsEligible(_application, RiskScore());

    public IReadOnlyList<string> RequiredDocuments()
        => _documentPolicy.GetRequiredDocuments(_application, IsEligible());

    public string DecisionLetter(string applicantName)
        => _letterFormatter.Format(
            applicantName,
            _application,
            RiskScore(),
            IsEligible(),
            RequiredDocuments());

    public string UnderwriterCsvRow(string applicationId)
        => _csvExporter.Export(
            applicationId,
            _application,
            RiskScore(),
            IsEligible());
}
