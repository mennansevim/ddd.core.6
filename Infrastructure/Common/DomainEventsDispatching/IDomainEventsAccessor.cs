
using Domain.Common;
using System.Collections.Generic;

namespace Infrastructure.Common.DomainEventsDispatching
{
    public interface IDomainEventsAccessor
    {
        List<IDomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}