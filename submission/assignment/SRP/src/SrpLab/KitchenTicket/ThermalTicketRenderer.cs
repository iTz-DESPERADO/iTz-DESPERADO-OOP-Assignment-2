namespace SrpLab.KitchenTicket;

public sealed class ThermalTicketRenderer
{
    public string Render(
        int orderNumber,
        IReadOnlyList<KitchenItem> items,
        int estimatedReadyMinutes,
        IReadOnlyList<string> allergens)
    {
        const int width = 32;

        var line = new string('=', width);

        var body = string.Join(
            '\n',
            items.Select(
                item =>
                    $"* {item.Item.ToUpperInvariant()} ({item.PrepMinutes}m)"));

        var allergyLine = allergens.Count == 0
            ? "ALLERGENS: none"
            : "ALLERGENS: " + string.Join(",", allergens);

        return $"{line}\n" +
               $"ORDER #{orderNumber}\n" +
               $"ETA {estimatedReadyMinutes} MIN\n" +
               $"{body}\n" +
               $"{allergyLine}\n" +
               $"{line}\n";
    }
}
