using CurrencyExchangeRate.Domain.Interfaces;
using CurrencyExchangeRate.Domain.Interfaces.Repositories;
using CurrencyExchangeRate.Infrastructure.Persistence.DataContext;
using CurrencyExchangeRate.Infrastructure.Persistence.Repositories;

namespace CurrencyExchangeRate.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private CurrencyRepository _produtoRepository;
        private ExchangeRateRepository _categoriaRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICurrencyRepository currencyRepository => _produtoRepository ??= new CurrencyRepository(_context);

        public IExchangeRateRepository exchangeRateRepository => _categoriaRepository ??= new ExchangeRateRepository(_context);

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here if needed
                    if (currencyRepository is IDisposable disposableCurrencyRepo)
                    {
                        disposableCurrencyRepo.Dispose();
                    }
                    if (exchangeRateRepository is IDisposable disposableExchangeRateRepo)
                    {
                        disposableExchangeRateRepo.Dispose();
                    }
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}