namespace CashRolls.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CashRolls.Data.Models;

    public class ErrorConfiguration : IEntityTypeConfiguration<Error>
    {
        public void Configure(EntityTypeBuilder<Error> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.Method).HasMaxLength(50).IsRequired(false);
            entity.Property(entity => entity.Path).HasMaxLength(250).IsRequired(false);
            entity.Property(entity => entity.Message).IsRequired(false);
            entity.Property(entity => entity.StackTrace).IsRequired(false);
            entity.Property(entity => entity.UserId).IsRequired(false);
        }
    }
}
