namespace SrpLab.WardBoard;

public sealed class PagerLog
{
    private readonly List<string> _entries = new();

    public void Add(string message)
        => _entries.Add(message);

    public IReadOnlyList<string> Drain()
    {
        var copy = _entries.ToList();
        _entries.Clear();
        return copy;
    }
}
