namespace Domain.B2BMasters
{
    public enum B2BStatusTypeNames
    {
        PickingWaiting = 10,
        PickingStarted = 20,
        WeighingCompleted = 30,
        Canceling = 40,
        Cancelled = 50,
        WaitingShipment = 60,
        Shipped = 70,
        InvoiceReady = 80,
        Invoicing = 90,
        InvoiceCreated = 100
    }
}