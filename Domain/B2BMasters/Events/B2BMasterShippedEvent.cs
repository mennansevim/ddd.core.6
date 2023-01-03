using System;
using Domain.Common;

namespace Domain.B2BMasters.Events;

public record B2BMasterShippedEvent(
    long Id,
    long? OrderParentId,
    Supplier Supplier,
    B2BMasterStatus Status,
    B2BType Type,
    int? WarehouseId,
    string? InvoiceTypeName,
    DateTime? ShippedDate,
    CarrierType CarrierType,
    bool IsOpenOrder
) : IDomainEvent
{
    public string AggregateId => Convert.ToString(Id);

    public static B2BMasterShippedEvent Of(B2BMaster master) => new B2BMasterShippedEvent(
        Id: master.Id,
        OrderParentId: master.OrderParentId,
        Supplier: master.Supplier,
        Status: master.Status,
        Type: master.Type,
        WarehouseId: master.WarehouseId,
        InvoiceTypeName: master.InvoiceTypeName,
        ShippedDate: master.ShippedDate,
        CarrierType: master.CarrierType,
        IsOpenOrder: master.IsOpenOrder
    );
}