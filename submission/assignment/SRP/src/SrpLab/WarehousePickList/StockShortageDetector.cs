namespace SrpLab.WarehousePickList;

public sealed class StockShortageDetector
{
    public IReadOnlyList<string> Find(IReadOnlyList<StockAllocation> allocations)
    {
        // Preserve the lab's first-request rule when a SKU occurs more than once.
        return allocations
            .Where(allocation => allocation.Allocated <
                allocations.First(line => line.Sku == allocation.Sku).QtyNeeded)
            .Select(allocation => allocation.Sku)
            .ToList();
    }
}
