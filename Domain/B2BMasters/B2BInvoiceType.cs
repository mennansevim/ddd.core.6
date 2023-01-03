using System.Runtime.Serialization;

namespace Domain.B2BMasters
{
    public enum B2BInvoiceType
    {
        [EnumMember(Value = "Satış Faturası")]
        SaleInvoice,

        [EnumMember(Value = "İade Faturası")]
        ReturnInvoice,

        [EnumMember(Value = "İstisna Faturası")]
        SpecialInvoice,
        
        [EnumMember(Value = "Konsinye Faturası")]
        ConsigneeInvoice,
        
        [EnumMember(Value = "İhracat Faturası")]
        ExportInvoice,
        
        [EnumMember(Value = "Satış Faturası FC")]
        SaleInvoiceFC,
        
        [EnumMember(Value = "Bağış Faturası")]
        DonateInvoice
    }
}
