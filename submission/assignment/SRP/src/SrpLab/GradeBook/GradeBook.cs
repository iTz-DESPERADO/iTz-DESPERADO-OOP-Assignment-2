namespace SrpLab.GradeBook;

public sealed class GradeBook
{
    private readonly GradeBookState _state = new();
    private readonly GradePolicy _gradePolicy = new();
    private readonly HonorRollPolicy _honorRollPolicy = new();
    private readonly GradeSummaryBuilder _summaryBuilder = new();
    private readonly TranscriptFormatter _transcriptFormatter = new();
    private readonly GradeBookCsvExporter _csvExporter = new();

    public void Record(string studentId, decimal score)
        => _state.Record(studentId, score);

    public decimal Average(string studentId)
        => _state.Average(studentId);

    public string Letter(string studentId)
        => _gradePolicy.GetLetter(Average(studentId));

    public bool MeetsHonorRoll(string studentId)
        => _honorRollPolicy.MeetsHonorRoll(
            Average(studentId),
            Letter(studentId));

    public string TranscriptPlain(
        string studentId,
        string fullName)
    {
        var summary = _summaryBuilder.BuildOne(
            _state,
            studentId,
            _gradePolicy,
            _honorRollPolicy);

        return _transcriptFormatter.Format(
            fullName,
            summary);
    }

    public string ExportCsv()
    {
        var rows = _summaryBuilder.Build(
            _state,
            _gradePolicy,
            _honorRollPolicy);

        return _csvExporter.Export(rows);
    }
}
