namespace Domain.B2BMasters.Args
{
    public record CreateB2BMasterItemArg(
        B2BProduct Product,
        B2BProductPrice ProductPrice,
        int RequestedQuantity,
        int OrderedQuantity,
        string StockId,
        string ReservationId
    );
}