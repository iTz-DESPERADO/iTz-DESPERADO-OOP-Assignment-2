namespace SrpLab.WarehousePickList;

public sealed class WmsXmlExporter
{
    public string Export(
        string batchId,
        IReadOnlyList<StockAllocation> allocations)
    {
        var parts = allocations.Select(
            allocation =>
                $"<line sku=\"{allocation.Sku}\" " +
                $"qty=\"{allocation.Allocated}\" />");

        return $"<batch id=\"{batchId}\">" +
               $"{string.Join("", parts)}" +
               "</batch>";
    }
}
