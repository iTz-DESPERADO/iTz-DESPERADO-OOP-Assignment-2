namespace SrpLab.WardBoard;

public sealed class WardBoard
{
    private readonly WardRegistry _registry = new();
    private readonly AcuityScorer _acuityScorer = new();
    private readonly AcuityStatusPolicy _statusPolicy = new();
    private readonly PagerPolicy _pagerPolicy = new();
    private readonly PagerMessageFormatter _pagerFormatter = new();
    private readonly PagerLog _pagerLog = new();
    private readonly HandoffNoteFormatter _handoffFormatter = new();
    private readonly CensusCsvExporter _csvExporter = new();

    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        var acuity = _acuityScorer.Calculate(heartRate, spo2);
        var patient = _registry.Assign(bed, patientId, acuity);

        var code = _pagerPolicy.GetCode(acuity);
        if (code is not null)
            _pagerLog.Add(_pagerFormatter.Format(code.Value, bed, DateTime.UtcNow));
    }

    public int ScoreAcuity(int heartRate, int spo2)
        => _acuityScorer.Calculate(heartRate, spo2);

    public string BuildHandoffNote(int bed)
    {
        if (!_registry.TryGet(bed, out var patient))
            return $"Bed {bed}: empty";

        var status = _statusPolicy.GetStatus(patient.Acuity);
        return _handoffFormatter.Format(patient, status, DateTime.UtcNow);
    }

    public IReadOnlyList<string> DrainPagerLog()
        => _pagerLog.Drain();

    public string ExportCensusCsv()
        => _csvExporter.Export(_registry.GetCensus());
}
