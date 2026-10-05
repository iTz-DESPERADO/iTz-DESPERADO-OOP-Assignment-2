namespace SrpLab.WardBoard;

public sealed class HandoffNoteFormatter
{
    public string Format(
        BedPatient patient,
        AcuityStatus status,
        DateTime timestamp)
    {
        return $"[HANDOFF {timestamp:yyyy-MM-dd}] Bed {patient.Bed} · {patient.PatientId} · " +
               $"acuity={patient.Acuity} · {status.ToString().ToUpperInvariant()}";
    }
}
