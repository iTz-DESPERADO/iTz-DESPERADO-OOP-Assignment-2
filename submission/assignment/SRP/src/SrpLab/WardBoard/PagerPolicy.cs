namespace SrpLab.WardBoard;

public sealed class PagerPolicy
{
    public PagerCode? GetCode(int acuity)
        => acuity >= 8 ? PagerCode.Yellow : null;
}
