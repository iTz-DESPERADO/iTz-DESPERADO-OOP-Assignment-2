namespace SrpLab.GradeBook;

public sealed class TranscriptFormatter
{
    public string Format(
        string fullName,
        StudentGradeSummary summary)
    {
        return $"TRANSCRIPT\n" +
               $"Student: {fullName} ({summary.StudentId})\n" +
               $"Average: {summary.Average}\n" +
               $"Letter: {summary.Letter}\n" +
               $"Honor: {summary.Honor}\n";
    }
}
