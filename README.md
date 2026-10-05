# OOP Assignment 2

- Name: Gamal Khaled Mohamed
- ID:

## Submission layout

This repository follows the submission paths displayed on the Simulation Academy
website. The assignment projects sit inside `submission/assignment/`; their
internal folder names follow the assignment PDF. LeetCode evidence is kept in
`submission/leetcode/`, beside the account file required by the website.

```text
submission/
  assignment/
    SRP/
      Responsibilities.md
      SrpLab.slnx
      src/SrpLab/
      src/SrpLab.Runner/
    DesignPatterns/
      linked.md
      PrototypeNotes.md
      PatternsLab.slnx
      src/PatternsLab/
      src/PatternsLab.Runner/
    Inheritance/
      ClassDiagram.png
      LibrarySystem.slnx
      src/
  leetcode/
    account.md
    README.md
    1456_MaxVowelsInSubstring/
      Solution.cs
      accepted_screenshot.png
      leetcode.md
  linkedin/
    posts.md
```

## Run the console projects

Requirements: .NET 10 SDK and the .NET 8 runtime/targeting pack, because the
projects currently target both `net10.0` and `net8.0`. Run these commands from
the repository root:

```powershell
dotnet run --project submission/assignment/SRP/src/SrpLab.Runner
dotnet run --project submission/assignment/DesignPatterns/src/PatternsLab.Runner
dotnet run --project submission/assignment/Inheritance/src/LibrarySystem.csproj
```

## Parts and evidence

- **SRP:** all ten examples are refactored into named responsibilities.
  [Responsibility analysis](submission/assignment/SRP/Responsibilities.md).
- **Design patterns:** shared, thread-safe Singleton initialization; Prototype
  deep copies for mutable enemy data and a prototype registry; fluent Builder
  with validation in `Build()`.
  [Prototype notes and references](submission/assignment/DesignPatterns/PrototypeNotes.md).
  [Three LinkedIn posts](submission/assignment/DesignPatterns/linked.md).
- **Inheritance:** [class diagram](submission/assignment/Inheritance/ClassDiagram.png)
  and a console demonstration of valid actions, rejected actions, and commented
  examples that must not compile.
- **LeetCode:** [solution and acceptance evidence](submission/leetcode/README.md).