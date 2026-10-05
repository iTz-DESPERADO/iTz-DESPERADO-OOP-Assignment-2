using System;
using System.Collections.Generic;

namespace LibrarySystem;

public class LibraryItem
{
    private static readonly List<string> _registeredCatalogNumbers = new List<string>();
    private readonly decimal _lateFeeMultiplier;

    public string CatalogNumber { get; }
    public string Title { get; }
    public int LoanPeriodDays { get; }
    public decimal BaseLateFee { get; private set; }
    public bool IsWithdrawn { get; private set; }
    public bool IsOnLoan { get; private set; }

    protected LibraryItem(string catalogNumber, string title, decimal baseLateFee,
        int loanPeriodDays, decimal multiplier)
    {
        if (string.IsNullOrWhiteSpace(catalogNumber))
            throw new ArgumentException("Catalog number must not be empty.");
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Item title must not be empty.");
        if (baseLateFee <= 0)
            throw new ArgumentException("Base late fee must be greater than zero.");
        if (loanPeriodDays <= 0)
            throw new ArgumentException("Loan period must be positive.");
        if (multiplier <= 0)
            throw new ArgumentException("Late-fee multiplier must be positive.");
        if (_registeredCatalogNumbers.Contains(catalogNumber))
            throw new ArgumentException("Catalog number '" + catalogNumber + "' is already registered.");

        CatalogNumber = catalogNumber;
        Title = title;
        BaseLateFee = baseLateFee;
        LoanPeriodDays = loanPeriodDays;
        _lateFeeMultiplier = multiplier;
        _registeredCatalogNumbers.Add(catalogNumber);
    }

    public decimal GetDailyLateFee()
    {
        return BaseLateFee * _lateFeeMultiplier;
    }

    internal void ChangeLateFee(decimal newFee)
    {
        if (newFee <= 0)
            throw new ArgumentException("New base late fee must be greater than zero.");
        BaseLateFee = newFee;
    }

    internal void Withdraw()
    {
        IsWithdrawn = true;
    }

    internal void Restore()
    {
        IsWithdrawn = false;
    }

    internal void StartLoan()
    {
        if (IsWithdrawn)
            throw new InvalidOperationException("Item '" + Title + "' is withdrawn and cannot be borrowed.");
        if (IsOnLoan)
            throw new InvalidOperationException("Item '" + Title + "' is already on loan.");
        IsOnLoan = true;
    }

    internal void EndLoan()
    {
        IsOnLoan = false;
    }
}
