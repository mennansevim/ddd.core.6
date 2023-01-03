using System;
using Domain.Common;

namespace Domain.B2BMasters.Events
{
    public record B2BMasterCreatedEvent(
        long Id,
        long? OrderParentId,
        B2BType Type,
        Supplier Supplier,
        int? WarehouseId,
        B2BMasterStatus Status,
        CarrierType CarrierType
    ) : IDomainEvent
    {
        public string AggregateId => Convert.ToString(Id);

        public static B2BMasterCreatedEvent Of(B2BMaster b2BMaster) => new B2BMasterCreatedEvent(
            Id: b2BMaster.Id,
            OrderParentId: b2BMaster.OrderParentId,
            Type: b2BMaster.Type,
            Supplier: b2BMaster.Supplier,
            WarehouseId: b2BMaster.WarehouseId,
            Status: b2BMaster.Status,
            CarrierType: b2BMaster.CarrierType
        );
    }
}