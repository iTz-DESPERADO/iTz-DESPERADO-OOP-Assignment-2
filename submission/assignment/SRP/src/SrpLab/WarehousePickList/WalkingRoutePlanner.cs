namespace SrpLab.WarehousePickList;

public sealed class WalkingRoutePlanner
{
    public IReadOnlyList<PickStep> Plan(
        IReadOnlyList<StockAllocation> allocations)
    {
        return allocations
            .Where(allocation => allocation.Allocated > 0)
            .OrderBy(allocation => allocation.Aisle)
            .ThenBy(allocation => allocation.Bin)
            .Select(
                allocation =>
                    new PickStep(
                        allocation.Aisle,
                        allocation.Bin,
                        allocation.Sku,
                        allocation.Allocated))
            .ToList();
    }
}
