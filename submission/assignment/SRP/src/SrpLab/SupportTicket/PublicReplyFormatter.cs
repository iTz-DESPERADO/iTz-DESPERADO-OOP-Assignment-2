namespace SrpLab.SupportTicket;

public sealed class PublicReplyFormatter
{
    public string Format(
        string ticketId,
        string priority,
        string agentName,
        DateTimeOffset slaDeadline)
    {
        var opening = priority == "P1"
            ? "We are treating this as a critical incident."
            : "Thanks for reaching out.";

        return $"Hi,\n{opening}\nTicket {ticketId} is with {agentName}. " +
               $"Next update before {slaDeadline:u}.\n";
    }
}
