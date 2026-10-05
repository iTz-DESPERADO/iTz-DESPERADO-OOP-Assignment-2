namespace SrpLab.GradeBook;

public sealed record StudentGradeSummary(
    string StudentId,
    decimal Average,
    string Letter,
    bool Honor);
