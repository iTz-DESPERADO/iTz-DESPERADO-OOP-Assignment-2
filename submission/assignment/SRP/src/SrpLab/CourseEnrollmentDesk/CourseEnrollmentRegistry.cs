namespace SrpLab.CourseEnrollmentDesk;

public sealed class CourseEnrollmentRegistry
{
    private readonly HashSet<string> _seated =
        new(StringComparer.OrdinalIgnoreCase);

    private readonly List<string> _waitlist = new();

    public string CourseCode { get; }
    public int Capacity { get; }
    public decimal Tuition { get; }

    public CourseEnrollmentRegistry(
        string courseCode,
        int capacity,
        decimal tuition)
    {
        CourseCode = courseCode;
        Capacity = capacity;
        Tuition = tuition;
    }

    public string Register(string studentEmail)
    {
        if (string.IsNullOrWhiteSpace(studentEmail))
            throw new ArgumentException("email", nameof(studentEmail));

        var email = studentEmail.Trim();

        if (_seated.Contains(email) || _waitlist.Contains(email))
            return "ALREADY_REGISTERED";

        if (_seated.Count < Capacity)
        {
            _seated.Add(email);
            return "SEATED";
        }

        _waitlist.Add(email);
        return $"WAITLIST:{_waitlist.Count}";
    }

    public int WaitlistPosition(string studentEmail)
    {
        var index = _waitlist.FindIndex(
            email => email.Equals(
                studentEmail,
                StringComparison.OrdinalIgnoreCase));

        return index < 0 ? -1 : index + 1;
    }

    public bool IsSeated(string studentEmail)
        => _seated.Contains(studentEmail);

    public void PromoteFromWaitlist(int seats)
    {
        while (seats > 0 &&
               _waitlist.Count > 0 &&
               _seated.Count < Capacity)
        {
            var next = _waitlist[0];
            _waitlist.RemoveAt(0);
            _seated.Add(next);
            seats--;
        }
    }
}
