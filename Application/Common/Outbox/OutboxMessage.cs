using System;

namespace Application.Common.Outbox;

public record OutboxMessage
{
    public OutboxMessage(string aggregateId, string topic, string payload, DateTime createdDate)
    {
        AggregateId = aggregateId;
        Topic = topic;
        Payload = payload;
        CreatedDate = createdDate;
    }

    public long Id { get; set; }
    public string AggregateId { get; set; }
    public string Topic { get; set; }
    public string Payload { get; set; }
    public DateTime CreatedDate { get; set; }
}