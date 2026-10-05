namespace SrpLab.WardBoard;

public sealed class PagerMessageFormatter
{
    public string Format(PagerCode code, int bed, DateTime timestamp)
        => $"CODE-{code.ToString().ToUpperInvariant()} bed={bed} at {timestamp:HH:mm}";
}
