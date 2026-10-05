namespace SrpLab.KitchenTicket;

public sealed class ExpoLanePolicy
{
    public string GetLane(
        bool hasAllergens,
        int estimatedReadyMinutes)
    {
        if (hasAllergens)
            return "LANE-ALLERGY";

        return estimatedReadyMinutes > 20
            ? "LANE-SLOW"
            : "LANE-FAST";
    }
}
