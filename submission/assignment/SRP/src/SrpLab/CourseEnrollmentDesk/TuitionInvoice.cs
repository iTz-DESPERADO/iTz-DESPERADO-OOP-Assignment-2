namespace SrpLab.CourseEnrollmentDesk;

public sealed record TuitionInvoice(
    bool IsSeated,
    decimal Tuition,
    decimal Vat,
    decimal Total);
