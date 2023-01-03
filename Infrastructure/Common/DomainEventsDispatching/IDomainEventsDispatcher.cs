using System.Threading.Tasks;

namespace Infrastructure.Common.DomainEventsDispatching
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}