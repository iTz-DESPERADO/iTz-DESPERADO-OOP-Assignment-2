namespace SrpLab.WarehousePickList;

public sealed record PickStep(
    string Aisle,
    int Bin,
    string Sku,
    int Qty);
