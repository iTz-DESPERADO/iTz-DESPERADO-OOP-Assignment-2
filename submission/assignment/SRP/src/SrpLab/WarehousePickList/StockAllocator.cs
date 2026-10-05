namespace SrpLab.WarehousePickList;

public sealed class StockAllocator
{
    public IReadOnlyList<StockAllocation> Allocate(
        IReadOnlyList<PickNeed> lines)
    {
        var result = new List<StockAllocation>();

        foreach (var line in lines)
        {
            var allocated =
                Math.Min(line.QtyNeeded, line.QtyOnHand);

            result.Add(
                new StockAllocation(
                    line.Sku,
                    line.Aisle,
                    line.Bin,
                    line.QtyNeeded,
                    allocated));
        }

        return result;
    }
}
