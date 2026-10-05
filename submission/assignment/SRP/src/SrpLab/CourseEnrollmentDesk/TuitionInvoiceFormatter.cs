namespace SrpLab.CourseEnrollmentDesk;

public sealed class TuitionInvoiceFormatter
{
    public string Format(
        string courseCode,
        TuitionInvoice invoice)
    {
        if (!invoice.IsSeated)
            return $"{courseCode},WAITLIST,0.00";

        return $"{courseCode},TUITION,{invoice.Tuition:0.00}," +
               $"VAT,{invoice.Vat:0.00},TOTAL,{invoice.Total:0.00}";
    }
}
