namespace CurrencyExchangeRate.Application.DTOs
{
    public record CurrencyDto(Guid CurrencyId,
                              string CurrencyCode,
                              string CurrencyName)
    {
        public CurrencyDto() : this(default, default, default) { }
    }

    public record CurrencyCreateDto(string CurrencyCode, string CurrencyName)
    {
        public CurrencyCreateDto() : this(default, default) { }
    }

    public record CurrencyUpdateDto(Guid CurrencyId,
                                    string CurrencyCode,
                                    string CurrencyName)
    {
        public CurrencyUpdateDto() : this(default, default, default) { }
    }
}