namespace SrpLab.GradeBook;

public sealed class GradePolicy
{
    public string GetLetter(decimal average)
    {
        if (average >= 90)
            return "A";

        if (average >= 80)
            return "B";

        if (average >= 70)
            return "C";

        if (average >= 60)
            return "D";

        return "F";
    }
}
