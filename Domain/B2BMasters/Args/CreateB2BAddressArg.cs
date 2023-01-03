using System;

namespace Domain.B2BMasters.Args
{
    public record CreateB2BAddressArg(
        B2BAddressType AddressType,
        string? Address,
        string? FirstName,
        string? LastName,
        string? FullName,
        string? Email,
        string? City,
        string? District,
        string? CountryName,
        string? Phone,
        bool? FreeTradeZone
    );
}