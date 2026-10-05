using System;

namespace LibrarySystem;

public class Staff : Person
{
    public DateTime HireDate { get; }
    public decimal MonthlySalary { get; private set; }
    public decimal ResponsibilityAllowance { get; }

    protected Staff(string id, string name, string phone, DateTime hireDate,
        decimal monthlySalary, decimal responsibilityAllowance)
        : base(id, name, phone)
    {
        if (monthlySalary <= 0)
            throw new ArgumentException("Monthly salary must be positive.");
        if (responsibilityAllowance < 0)
            throw new ArgumentException("Responsibility allowance cannot be negative.");

        HireDate = hireDate.Date;
        MonthlySalary = monthlySalary;
        ResponsibilityAllowance = responsibilityAllowance;
    }

    public void GiveRaise(decimal percentage)
    {
        if (percentage <= 0)
            throw new ArgumentException("Raise percentage must be greater than zero.");
        MonthlySalary = MonthlySalary + MonthlySalary * (percentage / 100m);
    }

    public decimal GetMonthlyPay()
    {
        return MonthlySalary + ResponsibilityAllowance;
    }
}
