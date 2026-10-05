namespace SrpLab.KitchenTicket;

public sealed class KitchenOrder
{
    private readonly List<KitchenItem> _items = new();

    public IReadOnlyList<KitchenItem> Items => _items;

    public void AddItem(
        string item,
        IEnumerable<string> ingredients,
        int prepMinutes)
    {
        var normalizedIngredients = ingredients
            .Select(ingredient => ingredient.Trim().ToLowerInvariant())
            .ToList();

        _items.Add(new KitchenItem(
            item,
            normalizedIngredients,
            prepMinutes));
    }
}
