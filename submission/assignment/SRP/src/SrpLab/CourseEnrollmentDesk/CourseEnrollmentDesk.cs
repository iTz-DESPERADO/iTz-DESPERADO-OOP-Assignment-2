namespace SrpLab.CourseEnrollmentDesk;

public sealed class CourseEnrollmentDesk
{
    private readonly CourseEnrollmentRegistry _registry;
    private readonly WelcomePacketFormatter _welcomePacketFormatter = new();
    private readonly TuitionInvoiceCalculator _tuitionInvoiceCalculator = new();
    private readonly TuitionInvoiceFormatter _tuitionInvoiceFormatter = new();

    public int Capacity => _registry.Capacity;
    public decimal Tuition => _registry.Tuition;
    public string CourseCode => _registry.CourseCode;

    public CourseEnrollmentDesk(
        string courseCode,
        int capacity,
        decimal tuition)
    {
        _registry = new CourseEnrollmentRegistry(
            courseCode,
            capacity,
            tuition);
    }

    public string Register(string studentEmail)
        => _registry.Register(studentEmail);

    public int WaitlistPosition(string studentEmail)
        => _registry.WaitlistPosition(studentEmail);

    public string WelcomePacketMarkdown(
        string studentEmail,
        string studentName)
    {
        return _welcomePacketFormatter.Format(
            CourseCode,
            studentEmail,
            studentName,
            _registry.IsSeated(studentEmail),
            WaitlistPosition(studentEmail));
    }

    public string TuitionInvoiceLine(string studentEmail)
    {
        var invoice = _tuitionInvoiceCalculator.Calculate(
            Tuition,
            _registry.IsSeated(studentEmail));

        return _tuitionInvoiceFormatter.Format(CourseCode, invoice);
    }

    public void PromoteFromWaitlist(int seats)
        => _registry.PromoteFromWaitlist(seats);
}
