namespace CurrencyExchangeRate.Domain.Entities
{
    public class Currency
    {
        public Guid CurrencyId { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Currency() { }

        public Currency(string code, string name)
        {
            CurrencyId = Guid.NewGuid();
            Code = code;
            Name = name;
        }

        public void UpdateCurrency(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public static Currency Create(string code, string name)
        {
            return new Currency(code, name);
        }
    }
}