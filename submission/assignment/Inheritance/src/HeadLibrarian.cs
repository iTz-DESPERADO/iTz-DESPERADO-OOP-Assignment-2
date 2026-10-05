using System;

namespace LibrarySystem;

public class HeadLibrarian : Staff
{
    public HeadLibrarian(string id, string name, string phone, DateTime hireDate, decimal monthlySalary)
        : base(id, name, phone, hireDate, monthlySalary, 400m)
    {
        RegisterPersonId();
    }

    public void ChangeLateFee(LibraryItem item, decimal newFee)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "An item is required to change its fee.");
        item.ChangeLateFee(newFee);
    }

    public void WithdrawItem(LibraryItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "An item is required to withdraw it.");
        item.Withdraw();
    }

    public void RestoreItem(LibraryItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "An item is required to restore it.");
        item.Restore();
    }
}
