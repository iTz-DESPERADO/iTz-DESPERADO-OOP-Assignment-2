using System;

namespace LibrarySystem;

public class Librarian : Staff
{
    public Librarian(string id, string name, string phone, DateTime hireDate, decimal monthlySalary)
        : base(id, name, phone, hireDate, monthlySalary, 0m)
    {
        RegisterPersonId();
    }

    public void ProcessReturn(Loan loan, DateTime returnDate)
    {
        if (loan == null)
            throw new ArgumentNullException(nameof(loan), "A loan is required to process a return.");
        loan.Return(returnDate);
    }

    public void MarkAsLost(Loan loan)
    {
        if (loan == null)
            throw new ArgumentNullException(nameof(loan), "A loan is required to mark it as lost.");
        loan.MarkAsLost();
    }
}
