using System;
using System.Collections.Generic;

namespace LibrarySystem;

public class Member : Person
{
    private readonly List<Loan> _loans = new List<Loan>();

    public int MaxLoans { get; }
    public decimal DiscountPercentage { get; }

    public IReadOnlyList<Loan> Loans
    {
        get { return _loans.AsReadOnly(); }
    }

    public int ActiveLoanCount
    {
        get
        {
            int count = 0;
            foreach (Loan loan in _loans)
            {
                if (loan.Status == LoanStatus.Borrowed)
                    count++;
            }
            return count;
        }
    }

    protected Member(string id, string name, string phone, int maxLoans, decimal discountPercentage)
        : base(id, name, phone)
    {
        if (maxLoans <= 0)
            throw new ArgumentException("Maximum loans must be positive.");
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentException("Discount percentage must be between 0 and 100.");
        MaxLoans = maxLoans;
        DiscountPercentage = discountPercentage;
    }

    public Loan Borrow(string loanId, LibraryItem item, DateTime date)
    {
        if (ActiveLoanCount >= MaxLoans)
            throw new InvalidOperationException(FullName + " has reached the loan limit of " + MaxLoans + ".");

        // Loan validates its data and asks the item to enforce availability
        // before this member's history is changed.
        Loan loan = new Loan(loanId, this, item, date);
        _loans.Add(loan);
        return loan;
    }
}
