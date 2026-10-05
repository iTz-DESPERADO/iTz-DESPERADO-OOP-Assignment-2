namespace SrpLab.WarehousePickList;

public sealed class WarehousePickListState
{
    private readonly List<PickNeed> _lines = new();

    public IReadOnlyList<PickNeed> Lines => _lines;

    public void AddNeed(
        string sku,
        string aisle,
        int bin,
        int qtyNeeded,
        int qtyOnHand)
    {
        _lines.Add(
            new PickNeed(
                sku,
                aisle,
                bin,
                qtyNeeded,
                qtyOnHand));
    }
}
