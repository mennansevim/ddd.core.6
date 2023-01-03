using System;
using Domain.Common;

namespace Domain.B2BMasters.Events
{
    public record B2BMasterItemCreatedEvent(
        long Id,
        long? B2BMasterId,
        B2BProduct Product,
        B2BProductPrice ProductPrice,
        int? OrderedQuantity,
        int? CancelledQuantity
    ) : IDomainEvent
    {
        public string AggregateId => Convert.ToString(Id);
        
        public static B2BMasterItemCreatedEvent Of(B2BMasterItem masterItem) => new B2BMasterItemCreatedEvent(
            Id: masterItem.Id,
            B2BMasterId: masterItem.B2BMasterId,
            Product: masterItem.Product,
            ProductPrice: masterItem.ProductPrice,
            OrderedQuantity: masterItem.OrderedQuantity,
            CancelledQuantity: masterItem.CanceledQuantity
        );
    }
}