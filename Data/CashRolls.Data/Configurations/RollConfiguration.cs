namespace CashRolls.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CashRolls.Data.Models;

    public class RollConfiguration : IEntityTypeConfiguration<Roll>
    {
        public void Configure(EntityTypeBuilder<Roll> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.EntryFee).HasColumnType("decimal(18,4)");
            entity.Property(entity => entity.CutPercent).HasColumnType("decimal(18,4)");
            entity.Property(entity => entity.MaxSum).HasColumnType("decimal(18,4)");
        }
    }
}
