namespace Domain.B2BMasters
{
    public enum B2BMasterStatus
    {
        New,
        WaitingForApproval,
        Approved,
        Shipped,
        Invoicing,
        InvoiceCreateError,
        Invoiced,
        Cancelled,
    }
}