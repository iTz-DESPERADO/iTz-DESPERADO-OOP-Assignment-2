using System;
using System.Collections.Generic;

namespace LibrarySystem;

public class Person
{
    private static readonly List<string> _registeredPersonIds = new List<string>();

    public string PersonId { get; }
    public string FullName { get; }
    public string Phone { get; }

    protected Person(string personId, string fullName, string phone)
    {
        if (string.IsNullOrWhiteSpace(personId))
            throw new ArgumentException("Person ID must not be empty.");
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name must not be empty.");
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone number must not be empty.");

        PersonId = personId;
        FullName = fullName;
        Phone = phone;
    }

    protected void RegisterPersonId()
    {
        if (_registeredPersonIds.Contains(PersonId))
            throw new ArgumentException("Person ID '" + PersonId + "' is already registered.");
        _registeredPersonIds.Add(PersonId);
    }
}
