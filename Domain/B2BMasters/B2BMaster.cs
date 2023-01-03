using System;
using System.Collections.Generic;
using System.Linq;
using Domain.B2BMasters.Args;
using Domain.B2BMasters.Events;
using Domain.B2BMasters.Rules;
using Domain.Common;

namespace Domain.B2BMasters
{
    public class B2BMaster : AggregateRoot<long>
    {
        protected B2BMaster()
        {
            //For ORM
        }

        private B2BMaster(
            long? orderParentId,
            Supplier supplier,
            B2BType type,
            int? warehouseId,
            string? creatorEmail,
            string? invoiceTypeName,
            CarrierType carrierType,
            bool isOpenOrder,
            int? createdBy,
            B2BMasterStatus status
        )
        {
            OrderParentId = orderParentId;
            Supplier = supplier;
            Type = type;
            WarehouseId = warehouseId;
            CreatorEmail = creatorEmail;
            InvoiceTypeName = invoiceTypeName;
            CarrierType = carrierType;
            IsOpenOrder = isOpenOrder;
            CreatedBy = createdBy;
            Status = status;
            IsInvoiced = false;
            ShippedDate = null;
            CreationDate = DateTime.Now;
            IsDeleted = false;
            Items = new List<B2BMasterItem>();
            Addresses = new List<B2BAddress>();

            AddDomainEvent(() => B2BMasterCreatedEvent.Of(this));
        }

        public long? OrderParentId { get; }
        public virtual Supplier Supplier { get; }
        public B2BType Type { get; }
        public int? WarehouseId { get; }
        public string? CreatorEmail { get; }
        public string? InvoiceTypeName { get; }
        public bool? IsInvoiced { get; }
        public DateTime? ShippedDate { get; private set; }
        public CarrierType CarrierType { get; }
        public bool IsOpenOrder { get; }
        public int? CreatedBy { get; }
        public B2BMasterStatus Status { get; private set; }
        public virtual ICollection<B2BAddress> Addresses { get; private set; }
        public virtual ICollection<B2BMasterItem> Items { get; private set; }

        public static B2BMaster Create(CreateB2BMasterArg createB2BMasterArg) => new B2BMaster(
            orderParentId: createB2BMasterArg.OrderParentId,
            supplier: createB2BMasterArg.Supplier,
            type: createB2BMasterArg.Type,
            warehouseId: createB2BMasterArg.WarehouseId,
            creatorEmail: createB2BMasterArg.CreatorEmail,
            invoiceTypeName: createB2BMasterArg.InvoiceTypeName,
            carrierType: createB2BMasterArg.CarrierType,
            isOpenOrder: createB2BMasterArg.IsOpenOrder,
            createdBy: createB2BMasterArg.CreatedBy,
            status: B2BMasterStatus.New
        );

        public bool IsShipped() => Status == B2BMasterStatus.Shipped;

        public void Ship(UpdateShipArg updateShipArg)
        {
            if (IsShipped())
                return;

            CheckRule(new MasterStatusCanNotBeShippedIfNotApproved(this));

            updateShipArg.UnSuppliedItems.Select(x => new
            {
                Item = Items.First(item => item.Product.VariantId == x.VariantId),
                UnSuppliedQuantity = x.Quantity
            }).ToList().ForEach(x =>
                x.Item.UnSupplied(x.UnSuppliedQuantity)
            );

            Status = B2BMasterStatus.Shipped;
            ShippedDate = updateShipArg.ShippedDate;

            AddDomainEvent(() => B2BMasterShippedEvent.Of(this));
        }

        public void SetShipmentAddress(B2BAddress address)
        {
            Addresses.Add(address);
        }

        public void SetInvoiceAddress(B2BAddress address)
        {
            Addresses.Add(address);
        }

        public void AddItem(B2BMasterItem item)
        {
            CheckRule(new ListingIdMustBeUniqueForEachB2BMasterRule(this, item));

            Items.Add(item);
        }
    }
}