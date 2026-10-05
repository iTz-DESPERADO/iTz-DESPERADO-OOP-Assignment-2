namespace SrpLab.WardBoard;

public sealed class WardRegistry
{
    private readonly Dictionary<int, BedPatient> _beds = new();

    public BedPatient Assign(int bed, string patientId, int acuity)
    {
        if (bed <= 0)
            throw new ArgumentOutOfRangeException(nameof(bed));

        if (string.IsNullOrWhiteSpace(patientId))
            throw new ArgumentException("patient required", nameof(patientId));

        var patient = new BedPatient(
            bed,
            patientId.Trim().ToUpperInvariant(),
            acuity);

        _beds[bed] = patient;
        return patient;
    }

    public bool TryGet(int bed, out BedPatient patient)
        => _beds.TryGetValue(bed, out patient!);

    public IEnumerable<WardCensusRow> GetCensus()
    {
        foreach (var patient in _beds.Values.OrderBy(x => x.Bed))
        {
            yield return new WardCensusRow(
                patient.Bed,
                patient.PatientId,
                patient.Acuity);
        }
    }
}
