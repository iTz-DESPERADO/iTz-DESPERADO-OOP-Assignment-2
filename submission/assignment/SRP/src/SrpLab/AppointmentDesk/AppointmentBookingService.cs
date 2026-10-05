namespace SrpLab.AppointmentDesk;

public sealed class AppointmentBookingService
{
    public bool TryBook(
        DateTimeOffset slot,
        BusinessHoursPolicy businessHours,
        AppointmentBook book)
    {
        if (!businessHours.IsWithinBusinessHours(slot) ||
            book.IsBooked(slot))
        {
            return false;
        }

        return book.Add(slot);
    }
}
