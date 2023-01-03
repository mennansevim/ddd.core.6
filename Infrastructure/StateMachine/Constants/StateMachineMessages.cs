namespace Infrastructure.StateMachine.Constants
{
    public static class StateMachineMessages
    {
        public const string CantMoveNextFromInvoiceCreatedErrorMessage = "InvoiceCreated durumundaki bir b2b için tekrar işlem yapılamaz !";
        public const string StatusMustBeMoveLinearErrorF = "{0} statüsündeki b2b kaydı {1} statüsüne geçemez !";
        public const string UnKnownStatus = "Tanımsız statü !";
        public const string FirstStatusShouldBePickingWaitingMessage = "İlk statü Toplama Bekliyor olmalı !";
    }
}