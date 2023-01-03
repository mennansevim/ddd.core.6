using System.Text.Json.Serialization;

namespace Domain.Common
{
    public interface IDomainEvent
    {
        [JsonIgnore]
        string AggregateId { get; }
    }
}