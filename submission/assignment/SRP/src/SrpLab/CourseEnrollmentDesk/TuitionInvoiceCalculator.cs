namespace SrpLab.CourseEnrollmentDesk;

public sealed class TuitionInvoiceCalculator
{
    public TuitionInvoice Calculate(decimal tuition, bool isSeated)
    {
        if (!isSeated)
            return new TuitionInvoice(false, 0m, 0m, 0m);

        var vat = Math.Round(tuition * 0.14m, 2);
        return new TuitionInvoice(true, tuition, vat, tuition + vat);
    }
}
