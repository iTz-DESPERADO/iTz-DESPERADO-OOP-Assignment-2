namespace SrpLab.GradeBook;

public sealed class GradeSummaryBuilder
{
    public IEnumerable<StudentGradeSummary> Build(
        GradeBookState gradeBook,
        GradePolicy gradePolicy,
        HonorRollPolicy honorRollPolicy)
    {
        foreach (var studentId in gradeBook.StudentIds)
        {
            yield return BuildOne(
                gradeBook,
                studentId,
                gradePolicy,
                honorRollPolicy);
        }
    }

    public StudentGradeSummary BuildOne(
        GradeBookState gradeBook,
        string studentId,
        GradePolicy gradePolicy,
        HonorRollPolicy honorRollPolicy)
    {
        var average = gradeBook.Average(studentId);
        var letter = gradePolicy.GetLetter(average);
        var honor = honorRollPolicy.MeetsHonorRoll(
            average,
            letter);

        return new StudentGradeSummary(
            studentId,
            average,
            letter,
            honor);
    }
}
