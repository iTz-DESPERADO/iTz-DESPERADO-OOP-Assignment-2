# Prototype: Notes and References

## Problem and prototype

Prototype creates a new object from an existing configured object. This avoids
repeating expensive initialization and lets the caller request a copy without
selecting a concrete class. The existing object used as the copying source is
the prototype. See [Refactoring Guru's pattern explanation](https://refactoring.guru/design-patterns/prototype).

## Clone in this lab

The client stores an `Enemy` and calls `Clone()`. Each concrete enemy creates the
same enemy type through a constructor that accepts the existing model data, so
the slow model-loading constructor does not run again. The clone has the same
`ModelId` and copied name and health. It gets its own `Weapon` and `Abilities`
list. The registry stores named prototypes and returns clones.

## Shallow and deep copies

A shallow copy copies reference values, so both objects can refer to the same
mutable nested object. A deep copy also copies the mutable objects that must be
independent. In this lab, sharing `Weapon` would make a damage change affect both
enemies; sharing `Abilities` would make an added ability appear in both lists.
The implementation clones the weapon and creates a new list. Strings can be
shared because they are immutable.

[Microsoft's MemberwiseClone documentation](https://learn.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone?view=net-8.0)
explains shallow copying and demonstrates how to copy nested reference data.
The lab uses explicit cloning constructors instead of `MemberwiseClone()`.
