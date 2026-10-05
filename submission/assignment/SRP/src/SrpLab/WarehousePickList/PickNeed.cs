namespace SrpLab.WarehousePickList;

public sealed record PickNeed(
    string Sku,
    string Aisle,
    int Bin,
    int QtyNeeded,
    int QtyOnHand);
