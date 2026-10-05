namespace SrpLab.SupportTicket;

public sealed class TicketPriorityClassifier
{
    public string Classify(string subject, string body)
    {
        var text = (subject + " " + body).ToLowerInvariant();

        if (text.Contains("down") ||
            text.Contains("outage") ||
            text.Contains("cannot login"))
        {
            return "P1";
        }

        if (text.Contains("urgent") ||
            text.Contains("asap") ||
            text.Contains("blocked"))
        {
            return "P2";
        }

        return "P3";
    }
}
