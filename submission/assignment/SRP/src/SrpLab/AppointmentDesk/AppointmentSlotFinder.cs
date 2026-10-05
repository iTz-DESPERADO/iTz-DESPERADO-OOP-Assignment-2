namespace SrpLab.AppointmentDesk;

public sealed class AppointmentSlotFinder
{
    public DateTimeOffset? FindNextSlot(
        DateTimeOffset from,
        int searchHours,
        BusinessHoursPolicy businessHours,
        AppointmentBook book)
    {
        var cursor = Align(
            from,
            businessHours.SlotMinutes);

        var end = from.AddHours(searchHours);

        while (cursor < end)
        {
            if (businessHours.IsWithinBusinessHours(cursor) &&
                !book.IsBooked(cursor))
            {
                return cursor;
            }

            cursor =
                cursor.AddMinutes(
                    businessHours.SlotMinutes);
        }

        return null;
    }

    private static DateTimeOffset Align(
        DateTimeOffset from,
        int slotMinutes)
    {
        var minutes =
            from.Minute -
            (from.Minute % slotMinutes);

        return new DateTimeOffset(
            from.Year,
            from.Month,
            from.Day,
            from.Hour,
            minutes,
            0,
            from.Offset);
    }
}
