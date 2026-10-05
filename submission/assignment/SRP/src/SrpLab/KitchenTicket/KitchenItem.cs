namespace SrpLab.KitchenTicket;

public sealed record KitchenItem(
    string Item,
    IReadOnlyList<string> Ingredients,
    int PrepMinutes);
