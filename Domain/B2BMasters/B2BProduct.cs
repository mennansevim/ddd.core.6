using Domain.Common;

namespace Domain.B2BMasters
{
    public record B2BProduct(
        string? Name,
        string? Barcode,
        int VariantId,
        string? ListingId,
        string? Composition,
        int? HsCode,
        decimal Weight,
        string? Origin
    );
}