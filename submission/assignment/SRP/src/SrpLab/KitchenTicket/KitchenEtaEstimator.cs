namespace SrpLab.KitchenTicket;

public sealed class KitchenEtaEstimator
{
    public int Estimate(
        IReadOnlyList<KitchenItem> items,
        int openStations,
        bool hasAllergens)
    {
        if (openStations <= 0)
            openStations = 1;

        var sequential = items.Sum(item => item.PrepMinutes);
        var parallel =
            (int)Math.Ceiling(sequential / (double)openStations);

        if (hasAllergens)
            parallel += 3;

        var longest = items.Count == 0
            ? 0
            : items.Max(item => item.PrepMinutes);

        return Math.Max(parallel, longest);
    }
}
