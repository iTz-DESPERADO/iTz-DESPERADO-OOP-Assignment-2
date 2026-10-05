namespace SrpLab.GradeBook;

public sealed class GradeBookCsvExporter
{
    public string Export(
        IEnumerable<StudentGradeSummary> rows)
    {
        var lines = new List<string>
        {
            "studentId,average,letter,honor"
        };

        foreach (var row in rows)
        {
            lines.Add(
                $"{row.StudentId}," +
                $"{row.Average}," +
                $"{row.Letter}," +
                $"{(row.Honor ? 1 : 0)}");
        }

        return string.Join('\n', lines);
    }
}
