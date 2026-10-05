namespace SrpLab.WarehousePickList;

public sealed class WarehousePickList
{
    private readonly WarehousePickListState _state = new();
    private readonly StockAllocator _allocator = new();
    private readonly WalkingRoutePlanner _routePlanner = new();
    private readonly StockShortageDetector _shortageDetector = new();
    private readonly PickerScriptFormatter _pickerScriptFormatter = new();
    private readonly WmsXmlExporter _wmsXmlExporter = new();

    public void AddNeed(
        string sku,
        string aisle,
        int bin,
        int qtyNeeded,
        int qtyOnHand)
    {
        _state.AddNeed(
            sku,
            aisle,
            bin,
            qtyNeeded,
            qtyOnHand);
    }

    public IReadOnlyList<(string Sku, int Allocated)> Allocate()
    {
        return _allocator
            .Allocate(_state.Lines)
            .Select(
                allocation =>
                    (allocation.Sku, allocation.Allocated))
            .ToList();
    }

    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)>
        WalkingOrder()
    {
        return _routePlanner
            .Plan(_allocator.Allocate(_state.Lines))
            .Select(
                step =>
                    (step.Aisle, step.Bin, step.Sku, step.Qty))
            .ToList();
    }

    public string PickerScript()
    {
        var allocations = _allocator.Allocate(_state.Lines);
        var steps = _routePlanner.Plan(allocations);

        return _pickerScriptFormatter.Format(
            steps,
            _shortageDetector.Find(allocations));
    }

    public string WmsXmlBatch(string batchId)
    {
        var allocations = _allocator.Allocate(_state.Lines);

        return _wmsXmlExporter.Export(
            batchId,
            allocations);
    }
}
