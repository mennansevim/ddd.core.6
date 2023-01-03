using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.PreReservations.Events
{
    public record GenerateOrderNumberEvent(
        long Id
    ) : IDomainEvent
    {
        public string AggregateId => Convert.ToString(Id);

        public static GenerateOrderNumberEvent Of(int Id) => new GenerateOrderNumberEvent(
            Id: Id
        );
    }
}