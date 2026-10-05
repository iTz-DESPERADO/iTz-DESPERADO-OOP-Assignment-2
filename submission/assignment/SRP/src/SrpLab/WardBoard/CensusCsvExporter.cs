namespace SrpLab.WardBoard;

public sealed class CensusCsvExporter
{
    public string Export(IEnumerable<WardCensusRow> rows)
    {
        var lines = new List<string> { "bed,patient,acuity" };

        foreach (var row in rows)
            lines.Add($"{row.Bed},{row.Patient},{row.Acuity}");

        return string.Join('\n', lines);
    }
}
