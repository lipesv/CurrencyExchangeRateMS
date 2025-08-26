using CurrencyExchangeRate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CurrencyExchangeRate.Infrastructure.Persistence.Configurations;

public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasKey(er => er.ExchangeRateId);

        builder.Property(er => er.BaseCurrencyCode)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(er => er.TargetCurrencyCode)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(er => er.Rate)
            .IsRequired();
            

        builder.Property(er => er.LastUpdated)
            .IsRequired();
            
        builder.HasIndex(er => new { er.BaseCurrencyCode, er.TargetCurrencyCode })
            .IsUnique();
    }
}