namespace SrpLab.KitchenTicket;

public sealed class KitchenTicket
{
    private readonly KitchenOrder _order = new();
    private readonly AllergenDetector _allergenDetector = new();
    private readonly KitchenEtaEstimator _etaEstimator = new();
    private readonly ThermalTicketRenderer _thermalTicketRenderer = new();
    private readonly ExpoLanePolicy _expoLanePolicy = new();

    public void AddItem(
        string item,
        IEnumerable<string> ingredients,
        int prepMinutes)
    {
        _order.AddItem(item, ingredients, prepMinutes);
    }

    public IReadOnlyList<string> DetectAllergens()
        => _allergenDetector.Detect(_order.Items);

    public int EstimatedReadyMinutes(int openStations)
        => _etaEstimator.Estimate(
            _order.Items,
            openStations,
            DetectAllergens().Count > 0);

    public string RenderThermalTicket(int orderNumber)
    {
        var allergens = DetectAllergens();
        var eta = _etaEstimator.Estimate(
            _order.Items,
            openStations: 2,
            hasAllergens: allergens.Count > 0);

        return _thermalTicketRenderer.Render(
            orderNumber,
            _order.Items,
            eta,
            allergens);
    }

    public string ExpoLaneHint()
    {
        var hasAllergens = DetectAllergens().Count > 0;
        var eta = EstimatedReadyMinutes(2);

        return _expoLanePolicy.GetLane(hasAllergens, eta);
    }
}
