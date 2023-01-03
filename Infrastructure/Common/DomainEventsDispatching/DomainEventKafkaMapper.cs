using Application.Common.Exceptions;
using Domain.B2BMasters.Events;
using Domain.Common;
using Domain.PreReservations.Events;

namespace Infrastructure.Common.DomainEventsDispatching;

public class DomainEventKafkaMapper
{
    public string GetName(IDomainEvent domainEvent) => domainEvent switch
    {
        GenerateOrderNumberEvent => "my.sample.b2b.order-number-created.0",
        _ => throw new NotFoundException($"{domainEvent} can not found")
    };
}