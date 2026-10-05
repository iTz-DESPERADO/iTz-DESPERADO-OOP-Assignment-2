namespace SrpLab.WarehousePickList;

public sealed class PickerScriptFormatter
{
    public string Format(
        IReadOnlyList<PickStep> steps,
        IReadOnlyList<string> shortages)
    {
        var instructions = steps
            .Select(
                (step, index) =>
                    $"{index + 1}. Go aisle {step.Aisle} " +
                    $"bin {step.Bin}: pick {step.Qty} × {step.Sku}");

        var warning = shortages.Count > 0
            ? "SHORTAGES: " + string.Join(", ", shortages)
            : "SHORTAGES: none";

        return string.Join('\n', instructions) +
               "\n" +
               warning;
    }
}
