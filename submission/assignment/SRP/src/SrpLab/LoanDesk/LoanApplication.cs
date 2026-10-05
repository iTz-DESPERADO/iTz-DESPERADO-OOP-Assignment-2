namespace SrpLab.LoanDesk;

public sealed record LoanApplication(
    decimal RequestedAmount,
    int CreditScore,
    int EmploymentMonths,
    bool HasCollateral);
