using CurrencyExchangeRate.Domain.Interfaces.Repositories;

namespace CurrencyExchangeRate.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICurrencyRepository currencyRepository { get; }
        IExchangeRateRepository exchangeRateRepository { get; }

        Task<bool> CommitAsync();
    }
}