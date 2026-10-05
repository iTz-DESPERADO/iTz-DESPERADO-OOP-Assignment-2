namespace SrpLab.CourseEnrollmentDesk;

public sealed class WelcomePacketFormatter
{
    public string Format(
        string courseCode,
        string studentEmail,
        string studentName,
        bool isSeated,
        int waitlistPosition)
    {
        var status = isSeated
            ? "confirmed seat"
            : $"waitlist #{waitlistPosition}";

        return $"# Welcome to {courseCode}\n" +
               $"Hi {studentName},\n" +
               $"Your status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: " +
               $"https://example.invalid/{courseCode.ToLowerInvariant()}\n";
    }
}
