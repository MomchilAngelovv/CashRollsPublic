namespace CashRolls.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CashRolls.Data.Models;

    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.Name).HasMaxLength(50).IsRequired();
            entity.Property(entity => entity.Symbol).HasMaxLength(5).IsRequired();
        }
    }
}
