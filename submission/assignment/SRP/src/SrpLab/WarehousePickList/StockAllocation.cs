namespace SrpLab.WarehousePickList;

public sealed record StockAllocation(
    string Sku,
    string Aisle,
    int Bin,
    int QtyNeeded,
    int Allocated);
