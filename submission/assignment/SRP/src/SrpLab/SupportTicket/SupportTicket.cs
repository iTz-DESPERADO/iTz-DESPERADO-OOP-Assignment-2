namespace SrpLab.SupportTicket;

public sealed class SupportTicket
{
    private readonly TicketPriorityClassifier _priorityClassifier = new();
    private readonly SlaPolicy _slaPolicy = new();
    private readonly PublicReplyFormatter _publicReplyFormatter = new();
    private readonly InternalEscalationFormatter _internalEscalationFormatter = new();

    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; } = "P3";

    public SupportTicket(
        string id,
        string subject,
        string body,
        DateTimeOffset openedAt)
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;

        RecalculatePriorityFromText();
    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
        RecalculatePriorityFromText();
    }

    public void RecalculatePriorityFromText()
        => Priority = _priorityClassifier.Classify(Subject, Body);

    public DateTimeOffset SlaDeadline()
        => _slaPolicy.GetDeadline(OpenedAt, Priority);

    public bool IsBreached(DateTimeOffset now)
        => _slaPolicy.IsBreached(now, SlaDeadline());

    public string DraftPublicReply(string agentName)
        => _publicReplyFormatter.Format(
            Id,
            Priority,
            agentName,
            SlaDeadline());

    public string InternalEscalationBlurb()
        => _internalEscalationFormatter.Format(
            Id,
            Priority,
            SlaDeadline());
}
