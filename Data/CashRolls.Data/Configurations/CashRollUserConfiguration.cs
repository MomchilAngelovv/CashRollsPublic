namespace CashRolls.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CashRolls.Data.Models;

    public class CashRollUserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.PhoneNumber).IsRequired();
        }
    }
}
