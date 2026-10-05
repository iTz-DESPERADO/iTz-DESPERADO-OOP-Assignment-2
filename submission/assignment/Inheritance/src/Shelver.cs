using System;

namespace LibrarySystem;

public class Shelver : Staff
{
    public string Section { get; private set; }

    public Shelver(string id, string name, string phone, DateTime hireDate,
        decimal monthlySalary, string section)
        : base(id, name, phone, hireDate, monthlySalary, 0m)
    {
        if (string.IsNullOrWhiteSpace(section))
            throw new ArgumentException("Section must not be empty.");
        Section = section;
        RegisterPersonId();
    }

    public void Reassign(string section)
    {
        if (string.IsNullOrWhiteSpace(section))
            throw new ArgumentException("Section must not be empty.");
        Section = section;
    }
}
