using System;

namespace Domain.B2BMasters.Args
{
    public record CreateB2BMasterArg(
        long OrderParentId,
        B2BType Type,
        string InvoiceTypeName,
        Supplier Supplier,
        int? WarehouseId,
        CarrierType CarrierType,
        bool IsOpenOrder=false,
        string? CreatorEmail = null,
        int? CreatedBy = null
    );
}