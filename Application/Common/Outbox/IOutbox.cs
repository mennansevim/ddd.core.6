using System.Threading.Tasks;

namespace Application.Common.Outbox;

public interface IOutbox
{
    Task Add(OutboxMessage outbox);
}