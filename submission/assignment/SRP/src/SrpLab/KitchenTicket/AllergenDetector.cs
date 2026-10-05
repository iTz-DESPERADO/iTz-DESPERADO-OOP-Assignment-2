namespace SrpLab.KitchenTicket;

public sealed class AllergenDetector
{
    public IReadOnlyList<string> Detect(
        IReadOnlyList<KitchenItem> items)
    {
        var allergens =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var item in items)
        {
            foreach (var ingredient in item.Ingredients)
            {
                if (ingredient.Contains("milk") ||
                    ingredient.Contains("cheese") ||
                    ingredient.Contains("butter"))
                {
                    allergens.Add("dairy");
                }

                if (ingredient.Contains("wheat") ||
                    ingredient.Contains("flour") ||
                    ingredient.Contains("bread"))
                {
                    allergens.Add("gluten");
                }

                if (ingredient.Contains("peanut") ||
                    ingredient.Contains("almond") ||
                    ingredient.Contains("cashew"))
                {
                    allergens.Add("nuts");
                }

                if (ingredient.Contains("shrimp") ||
                    ingredient.Contains("prawn") ||
                    ingredient.Contains("crab"))
                {
                    allergens.Add("shellfish");
                }
            }
        }

        return allergens.OrderBy(x => x).ToList();
    }
}
