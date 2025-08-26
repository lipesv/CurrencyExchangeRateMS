namespace CurrencyExchangeRate.Application.DTOs
{
    public record ExchangeRateDto(Guid ExchangeRateId,
                                  string BaseCurrencyCode,
                                  string TargetCurrencyCode,
                                  decimal Rate,
                                  DateTime LastUpdated)
    {
        public ExchangeRateDto() : this(default, default, default, default, default) { }
    }

    public record ExchangeRateCreateDto(string BaseCurrencyCode,
                                        string TargetCurrencyCode,
                                        decimal Rate)
    {
        public ExchangeRateCreateDto() : this(default, default, default) { }
    }

    public record ExchangeRateUpdateDto(Guid ExchangeRateId,
                                        decimal Rate)
    {
        public ExchangeRateUpdateDto() : this(default, default) { }
    }
}