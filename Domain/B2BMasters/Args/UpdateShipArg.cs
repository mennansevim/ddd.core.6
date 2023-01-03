using System;
using System.Collections.Generic;

namespace Domain.B2BMasters.Args;

public record UpdateShipArg(
    DateTime ShippedDate,
    IEnumerable<UpdateShipArg.UnSuppliedItem> UnSuppliedItems
)
{
    public record UnSuppliedItem(
        long VariantId,
        int Quantity
    );
}

