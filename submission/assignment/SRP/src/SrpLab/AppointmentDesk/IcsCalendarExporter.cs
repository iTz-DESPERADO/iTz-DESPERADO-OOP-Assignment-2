namespace SrpLab.AppointmentDesk;

public sealed class IcsCalendarExporter
{
    public string Export(
        DateTimeOffset slot,
        int slotMinutes,
        string patientName,
        string clinician)
    {
        var uid = Guid.NewGuid();
        var end = slot.AddMinutes(slotMinutes);

        return "BEGIN:VCALENDAR\n" +
               "VERSION:2.0\n" +
               "BEGIN:VEVENT\n" +
               $"UID:{uid}\n" +
               $"DTSTART:{slot:yyyyMMdd'T'HHmmss'Z'}\n" +
               $"DTEND:{end:yyyyMMdd'T'HHmmss'Z'}\n" +
               $"SUMMARY:Visit {patientName} / {clinician}\n" +
               "END:VEVENT\n" +
               "END:VCALENDAR\n";
    }
}
