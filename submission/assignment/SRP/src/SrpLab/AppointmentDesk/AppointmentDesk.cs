namespace SrpLab.AppointmentDesk;

public sealed class AppointmentDesk
{
    private readonly AppointmentBook _book = new();
    private readonly BusinessHoursPolicy _businessHours;
    private readonly AppointmentSlotFinder _slotFinder = new();
    private readonly AppointmentBookingService _bookingService = new();
    private readonly IcsCalendarExporter _icsExporter = new();
    private readonly SmsReminderFormatter _smsReminderFormatter = new();

    public TimeOnly Open => _businessHours.Open;
    public TimeOnly Close => _businessHours.Close;
    public int SlotMinutes => _businessHours.SlotMinutes;

    public AppointmentDesk(
        TimeOnly open,
        TimeOnly close,
        int slotMinutes)
    {
        _businessHours = new BusinessHoursPolicy(
            open,
            close,
            slotMinutes);
    }

    public bool IsWithinBusinessHours(DateTimeOffset when)
        => _businessHours.IsWithinBusinessHours(when);

    public DateTimeOffset? FindNextSlot(
        DateTimeOffset from,
        int searchHours)
    {
        return _slotFinder.FindNextSlot(
            from,
            searchHours,
            _businessHours,
            _book);
    }

    public bool TryBook(DateTimeOffset slot)
        => _bookingService.TryBook(
            slot,
            _businessHours,
            _book);

    public string ToIcs(
        DateTimeOffset slot,
        string patientName,
        string clinician)
    {
        return _icsExporter.Export(
            slot,
            SlotMinutes,
            patientName,
            clinician);
    }

    public string SmsReminder(
        DateTimeOffset slot,
        string clinicPhone)
    {
        return _smsReminderFormatter.Format(
            slot,
            clinicPhone);
    }
}
