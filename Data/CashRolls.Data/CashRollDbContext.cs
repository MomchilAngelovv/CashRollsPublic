namespace CashRolls.Data
{
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using CashRolls.Data.Models;

    public class CashRollDbContext : IdentityDbContext<User, Role, string>
    {
        public CashRollDbContext(
            DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Roll> Rolls { get; set; }
        public DbSet<RollUser> RollsUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
