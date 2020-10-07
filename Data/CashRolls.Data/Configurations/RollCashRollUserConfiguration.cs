namespace CashRolls.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CashRolls.Data.Models;

    public class RollCashRollUserConfiguration : IEntityTypeConfiguration<RollUser>
    {
        public void Configure(EntityTypeBuilder<RollUser> entity)
        {
            entity.HasKey(rollCashRollUser => rollCashRollUser.Id);
            entity.Property(entity => entity.RollId).HasMaxLength(450).IsRequired();
            entity.Property(entity => entity.UserId).HasMaxLength(450).IsRequired();
        }
    }
}
