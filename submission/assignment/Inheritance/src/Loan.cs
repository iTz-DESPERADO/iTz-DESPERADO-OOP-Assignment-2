using System;
using System.Collections.Generic;

namespace LibrarySystem;

public class Loan
{
    private static readonly List<string> _registeredLoanIds = new List<string>();

    public string LoanId { get; }
    public DateTime BorrowDate { get; }
    public Member Member { get; }
    public LibraryItem Item { get; }
    public LoanStatus Status { get; private set; }
    public DateTime? ReturnDate { get; private set; }

    public DateTime DueDate
    {
        get { return BorrowDate.AddDays(Item.LoanPeriodDays); }
    }

    public decimal LateFee
    {
        get
        {
            if (Status != LoanStatus.Returned || ReturnDate == null)
                return 0m;
            int lateDays = (ReturnDate.Value - DueDate).Days;
            if (lateDays <= 0)
                return 0m;
            return lateDays * Item.GetDailyLateFee() * (1m - Member.DiscountPercentage / 100m);
        }
    }

    internal Loan(string loanId, Member member, LibraryItem item, DateTime borrowDate)
    {
        if (string.IsNullOrWhiteSpace(loanId))
            throw new ArgumentException("Loan ID must not be empty.");
        if (member == null)
            throw new ArgumentNullException(nameof(member), "A loan must belong to a member.");
        if (item == null)
            throw new ArgumentNullException(nameof(item), "A loan must refer to an item.");
        if (_registeredLoanIds.Contains(loanId))
            throw new ArgumentException("Loan ID '" + loanId + "' is already registered.");

        // Ensure the computed due date can exist before changing availability.
        if (borrowDate.Date > DateTime.MaxValue.Date.AddDays(-item.LoanPeriodDays))
            throw new ArgumentException("Borrow date is too late to calculate a valid due date.");

        LoanId = loanId;
        Member = member;
        Item = item;
        BorrowDate = borrowDate.Date;
        Status = LoanStatus.Borrowed;
        Item.StartLoan();
        _registeredLoanIds.Add(loanId);
    }

    internal void Return(DateTime returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Only a borrowed loan can be returned. Current status: " + Status + ".");
        if (returnDate.Date < BorrowDate)
            throw new ArgumentException("Return date cannot be earlier than borrow date.");

        ReturnDate = returnDate.Date;
        Status = LoanStatus.Returned;
        Item.EndLoan();
    }

    internal void MarkAsLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Only a borrowed loan can be marked as lost. Current status: " + Status + ".");
        Status = LoanStatus.Lost;

    }
}
