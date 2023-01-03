namespace Domain.B2BMasters
{
    public class B2BProductPrice
    {
        protected B2BProductPrice()
        {
            //For ORM
        }

        public decimal Price { get; private set; }
        public decimal PriceVatIncluded { get; private set; }
        public decimal Vat { get; private set; }
        public int VatRate { get; private set; }
        public string Currency { get; private set; }

        public static B2BProductPrice Create(decimal price, int vatRate, string currency) => new B2BProductPrice()
        {
            Price = price,
            PriceVatIncluded = price * ((vatRate + 100.0m) / 100.0m),
            Vat = price * (vatRate / 100.0m),
            VatRate = vatRate,
            Currency = currency
        };
    }
}