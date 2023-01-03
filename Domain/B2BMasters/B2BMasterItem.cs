using System;
using Domain.B2BMasters.Args;
using Domain.B2BMasters.Events;
using Domain.B2BMasters.Rules;
using Domain.Common;

namespace Domain.B2BMasters
{
    public class B2BMasterItem : Entity<long>
    {
        protected B2BMasterItem()
        {
            //For ORM
        }

        private B2BMasterItem(
            B2BProduct product,
            B2BProductPrice productPrice,
            int requestedQuantity,
            int orderedQuantity,
            int canceledQuantity,
            string stockId,
            string reservationId)
        {
            Product = product;
            ProductPrice = productPrice;
            RequestedQuantity = requestedQuantity;
            OrderedQuantity = orderedQuantity;
            CanceledQuantity = canceledQuantity;
            UnSuppliedQuantity = 0;
            StockId = stockId;
            ReservationId = reservationId;
            IsRowDeleted = false;
            IsDeleted = false;
            CreationDate = DateTime.Now;
            
            AddDomainEvent(() => B2BMasterItemCreatedEvent.Of(this));
        }

        public long B2BMasterId { get; protected set; }
        public B2BProduct Product { get; }
        public B2BProductPrice ProductPrice { get; }
        public int? RequestedQuantity { get; }
        public int? OrderedQuantity { get; }
        public int? CanceledQuantity { get; }
        public int? UnSuppliedQuantity { get; private set; }
        public string? StockId { get; }
        public string? ReservationId { get; }
        public bool IsRowDeleted { get; }

        public static B2BMasterItem Create(CreateB2BMasterItemArg arg) => new B2BMasterItem(
            product: arg.Product,
            productPrice: arg.ProductPrice,
            requestedQuantity: arg.RequestedQuantity,
            orderedQuantity: arg.OrderedQuantity,
            canceledQuantity: arg.RequestedQuantity - arg.OrderedQuantity,
            stockId: arg.StockId,
            reservationId: arg.ReservationId
        );

        public void UnSupplied(int quantity)
        {
            CheckRule(new UnsuppliedQuantityCanNotBeGreaterThanOrderedQuantity(this, quantity));

            UnSuppliedQuantity = quantity;
            
            AddDomainEvent(() => B2BMasterItemUnSuppliedEvent.Of(this));
        }
    }
}