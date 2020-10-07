namespace CashRolls.Web.Controllers
{
    using System.Text;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using CashRolls.Data;
    using CashRolls.Web.Common;
    using CashRolls.Data.Models;

    public class SeederController : Controller
    {
        private readonly CashRollDbContext db;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public SeederController(
            CashRollDbContext db,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.db = db;
        }

        public async Task<IActionResult> Seed()
        {
            if (db.Users.Any())
            {
                return Ok("Database is not empty");
            }

            var stringBuilder = new StringBuilder();

            var user = new User
            {
                UserName = AdministratorUserValues.UserName,
                Email = AdministratorUserValues.Email,
                PhoneNumber = AdministratorUserValues.PhoneNumber,
            };
            var roles = new List<Role>
            {
                new Role
                {
                    Name = "User",
                    Information = "Basic role."
                },
                new Role
                {
                    Name = "Administrator",
                    Information = "Administrator role gives full access to the platoform."
                },
            };
            var currencies = new List<Currency>
            {
                new Currency
                {
                    Name = "USD",
                    IsoCode = "USD",
                    Symbol = "$",
                },
                new Currency
                {
                    Name = "EUR",
                    IsoCode = "EUR",
                    Symbol = "€"
                }
            };

            await userManager.CreateAsync(user, AdministratorUserValues.Passowrd);

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var curency in currencies)
            {
                await db.Currencies.AddAsync(curency);
            }

            await userManager.AddToRoleAsync(user, Roles.Administrator);
            await db.SaveChangesAsync();

            stringBuilder.AppendLine($"Registered user: {user.Email}");
            stringBuilder.AppendLine($"Registered roles: {string.Join(" ", roles.Select(role => role.Name))}");
            stringBuilder.AppendLine($"Registered currencies: {string.Join(" ", currencies.Select(currency => currency.Name))}");

            var response = stringBuilder.ToString();
            return Ok(response);
        }
    }
}
