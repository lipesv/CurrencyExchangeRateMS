namespace CurrencyExchangeRate.Domain.Entities
{
    public class ExchangeRate
    {
        public Guid ExchangeRateId { get; private set; }
        public string BaseCurrencyCode { get; private set; }
        public string TargetCurrencyCode { get; private set; }
        public decimal Rate { get; private set; }
        public DateTime LastUpdated { get; private set; }

        public ExchangeRate() { }

        public ExchangeRate(string baseCurrencyCode, string targetCurrencyCode, decimal rate)
        {
            ExchangeRateId = Guid.NewGuid();
            BaseCurrencyCode = baseCurrencyCode;
            TargetCurrencyCode = targetCurrencyCode;
            Rate = rate;
            LastUpdated = DateTime.Now;
        }

        public void UpdateRate(decimal newRate)
        {
            Rate = newRate;
            LastUpdated = DateTime.Now;
        }

        public static ExchangeRate Create(string baseCurrencyCode,
                                          string targetCurrencyCode,
                                          decimal rate)
        {
            return new ExchangeRate(baseCurrencyCode, targetCurrencyCode, rate);
        }
    }
}