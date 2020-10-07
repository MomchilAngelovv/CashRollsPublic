namespace CashRolls.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CashRolls.Data.Models;

    public class CashRollRoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.HasKey(entity => entity.Id);
        }
    }
}
