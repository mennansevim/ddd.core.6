using System;
using Domain.Common;

namespace Domain.B2BMasters.Events
{
    public record B2BMasterItemUnSuppliedEvent(
        long Id,
        long B2BMasterId,
        B2BProduct Product,
        int? OrderedQuantity,
        int? UnSuppliedQuantity
    ) : IDomainEvent
    {
        public string AggregateId => Convert.ToString(B2BMasterId);
        
        public static B2BMasterItemUnSuppliedEvent Of(B2BMasterItem masterItem) => new B2BMasterItemUnSuppliedEvent(
            Id: masterItem.Id,
            B2BMasterId: masterItem.B2BMasterId,
            Product: masterItem.Product,
            OrderedQuantity: masterItem.OrderedQuantity,
            UnSuppliedQuantity: masterItem.UnSuppliedQuantity
        );
    }
}