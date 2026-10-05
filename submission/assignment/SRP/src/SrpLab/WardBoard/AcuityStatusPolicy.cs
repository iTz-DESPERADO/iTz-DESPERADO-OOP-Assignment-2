namespace SrpLab.WardBoard;

public sealed class AcuityStatusPolicy
{
    public AcuityStatus GetStatus(int acuity)
    {
        if (acuity >= 8)
            return AcuityStatus.Escalate;

        if (acuity >= 4)
            return AcuityStatus.Watch;

        return AcuityStatus.Stable;
    }
}
